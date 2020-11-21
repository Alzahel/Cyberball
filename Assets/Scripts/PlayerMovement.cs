using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;


public class PlayerMovement : NetworkBehaviour
{
    CharacterController characterController;

    //gravity 
    private float yVelocity;
    private float gravity;

    //Movement input
    private float horizontal;
    private float vertical;
    Vector3 movementDirection;
    private bool isCrouching = false;
    private float moveSpeed;
    [SerializeField]
    private float walkSpeed = 5f;

    //jump 
    float jumpVelocity;
    [SerializeField]
    private float jumpHeight = 4;
    [SerializeField]
    private float timeToJumpPeak = .4f;
    private bool isJumping = false;
    private bool isPressingJumpKey;

    //Sprint
    [SerializeField]
    private float SprintSpeed = 10f;
    private bool isSprinting;
    private bool sprintFirstTap = false;
    private float sprintTimeBeforeSecondPress = 0.2f;

    //Energy managment
    [SerializeField] private float maxEnergyAmount = 100;
    private float energyAmount;
    [SerializeField] private float EnergyBurnSpeed = 10f;
    [SerializeField] private float EnergyRegenSpeed = 3f;

    public float Horizontal { get => horizontal; set => horizontal = value; }
    public float Vertical { get => vertical; set => vertical = value; }
    public bool IsSprinting { get => isSprinting; set => isSprinting = value; }
    public bool IsJumping { get => isJumping; set => isJumping = value; }
    public bool IsCrouching { get => isCrouching; set => isCrouching = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        if (hasAuthority)
        {
            moveSpeed = walkSpeed;
            energyAmount = maxEnergyAmount;
            //playerShoot.Shot += StopSpecialMovements;

            //to see the physic equations look at :https://youtu.be/PlT44xr0iW0?list=PLFt_AvWsXl0f0hqURlhyIoAabKPgRsqjz&t=351
            gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpPeak, 2);
            jumpVelocity = Mathf.Abs(gravity) * timeToJumpPeak;
        }
    }

    //Cancel all special Movemennts : Sprint / Dodge / WallJump / Dash / Slide / wallrun...
    private void StopSpecialMovements()
    {
        CancelSprint();
        if (isCrouching) cancelCrouching();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {

            Sprint();
            Jump();
            CharacterMovement();
            walkAnimation();

        }
    }

    private void CharacterMovement()
    {
        movementDirection = new Vector3(horizontal, 0, vertical);
        movementDirection *= moveSpeed;
        movementDirection.y = yVelocity;
        movementDirection.y += gravity * Time.deltaTime;
        movementDirection = transform.TransformDirection(movementDirection);
        characterController.Move(movementDirection * Time.deltaTime);
    }


    private void walkAnimation()
    {
        float runningAnim = new Vector2(movementDirection.x, movementDirection.z).sqrMagnitude;
    }

    private void Sprint()
    {
        //Si on détecte le firstTap et que dans la durée autorisée (SprintWaitForSecondPress) on détecte un appui de la touche "avancer" alors on sprint
        //Le second press après le tap (OnSprint) est géré avec une coroutine qui donne au joueur une certaines durée pour réappuyer sur la touche "avancer" 
        //qui sera détectée  dans le OnMove car l'input manager ne gère pas des séries d'action qui permettrait de détecter le tap puis le press
        if (sprintFirstTap)
        {
            if (vertical > 0 && energyAmount > 0 && !isSprinting)
            {
                isSprinting = true;
                moveSpeed = SprintSpeed;
                //if (AudioManager.instance != null) AudioManager.instance.PlaySound("Sprint");
            }
            //Si la touche avancé est relaché ou que l'énergie est vide on arrête de sprinter
        }
        else if (((vertical <= 0) || energyAmount <= 0) && isSprinting)
        {
            CancelSprint();
        }

        //Energy management
        if (isSprinting)
        {
            energyAmount -= EnergyBurnSpeed * Time.deltaTime;
        }
        else RegenEnergy();
    }

    //Cancel sprint movement
    private void CancelSprint()
    {
        if (isSprinting)
        {
            isSprinting = false;
            moveSpeed = walkSpeed;
            //if (AudioManager.instance != null) AudioManager.instance.PlaySound("StopSprint");
        }
    }

    private void Crouch()
    {
        //change the hitbox when crouching

        Debug.Log("Start crouching");
        isCrouching = true;
        characterController.height = 1;
        Vector3 center = characterController.center;
        center.y = 0.5f;
        characterController.center = center;
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        //if walking while crouching change the hitbox
        if (direction.magnitude != 0)
        {
            characterController.height = 1.2f;
            center.y = 0.6f;
            characterController.center = center;
        }

    }

    private void cancelCrouching()
    {
        Debug.Log("Stop crouching");
        isCrouching = false;
        //if (anim != null) anim.SetBool("isCrouching", false);
        characterController.height = 1.5f;
        Vector3 center = characterController.center;
        center.y = 0.8f;
        characterController.center = center;
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            yVelocity = 0;
            if (isJumping)
            {
                isJumping = false;
            }
            if (isPressingJumpKey)
            {
                yVelocity = jumpVelocity;
                isJumping = true;
            }
        }
        else
        {
            yVelocity = movementDirection.y;

        }
    }

    private void RegenEnergy()
    {
        if (energyAmount < maxEnergyAmount) energyAmount += EnergyRegenSpeed * Time.deltaTime;

        //Makes sure energyAmount can't go out the boundaries of 0 / max Energy
        if (energyAmount < 0 || energyAmount > 100) energyAmount = Mathf.Clamp(energyAmount, 0, maxEnergyAmount);
    }

    private void OnMove(InputValue value)
    {
        if (hasAuthority)
        {
            horizontal = value.Get<Vector2>().x;
            vertical = value.Get<Vector2>().y;
        }
    }

    private void OnSprint()
    {
        if (hasAuthority)
        {
            sprintFirstTap = true;
            StartCoroutine(SprintWaitForSecondPress());
        }
    }
    private IEnumerator SprintWaitForSecondPress()
    {
        yield return new WaitForSeconds(sprintTimeBeforeSecondPress);
        sprintFirstTap = false;
    }

    private void OnJump(InputValue value)
    {
        if (hasAuthority)
        {
            if (value.Get<float>() == 1) isPressingJumpKey = true;
            else isPressingJumpKey = false;
        }
    }

    private void OnCrouch(InputValue value)
    {
        if (hasAuthority)
        {
            //
            if (value.Get<float>() == 1) Crouch();//&characterController.isGrounded && 
            else cancelCrouching();
        }
    }
}
