using System;
using System.Collections;
using JetBrains.Annotations;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    private CharacterController characterController;

    //gravity 
    private float yVelocity;
    private float gravity;
    
    private Vector3 movementDirection;
        
    private float moveSpeed;
    [SerializeField]  private float walkSpeed = 5f;

    //jump 
    float jumpVelocity;
    [SerializeField] private float jumpHeight = 4;
    [SerializeField] private float timeToJumpPeak = .4f;
        
    private bool isPressingJumpKey;

    //Sprint
    [SerializeField] private float sprintSpeed = 10f;
        
    private bool sprintFirstTap;
    private const float SprintTimeBeforeSecondPress = 0.2f;

    //Energy managment
    [SerializeField] private float maxEnergyAmount = 100;
    private float energyAmount;
    [SerializeField] private float energyBurnSpeed = 10f;
    [SerializeField] private float energyRegenSpeed = 3f;
        
    //Movement input
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; } 
        
    //States
    public bool IsCrouching{ get; private set; }
    public bool IsJumping{ get; private set; }
    public bool IsSprinting{ get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        if (!hasAuthority) return;
            
        moveSpeed = walkSpeed;
        energyAmount = maxEnergyAmount;
        //playerShoot.Shot += StopSpecialMovements;

        //to see the physic equations look at :https://youtu.be/PlT44xr0iW0?list=PLFt_AvWsXl0f0hqURlhyIoAabKPgRsqjz&t=351
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpPeak, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpPeak;
    }

    //Cancel all special Movemennts : Sprint / Dodge / WallJump / Dash / Slide / wallrun...
    private void StopSpecialMovements()
    {
        CancelSprint();
        if (IsCrouching) CancelCrouching();
    }

    // Update is called once per frame
    private void Update()
    {
        if (hasAuthority)
        {

            Sprint();
            Jump();
            CharacterMovement();
        }
    }

    private void CharacterMovement()
    {
        movementDirection = new Vector3(Horizontal, 0, Vertical);
        movementDirection *= moveSpeed;
        movementDirection.y = yVelocity;
        movementDirection.y += gravity * Time.deltaTime;
        movementDirection = transform.TransformDirection(movementDirection);
        characterController.Move(movementDirection * Time.deltaTime);
    }
    
    private void Sprint()
    {
        //Si on détecte le firstTap et que dans la durée autorisée (SprintWaitForSecondPress) on détecte un appui de la touche "avancer" alors on sprint
        //Le second press après le tap (OnSprint) est géré avec une coroutine qui donne au joueur une certaines durée pour réappuyer sur la touche "avancer" 
        //qui sera détectée  dans le OnMove car l'input manager ne gère pas des séries d'action qui permettrait de détecter le tap puis le press
        if (sprintFirstTap)
        {
            if (Vertical > 0 && energyAmount > 0 && !IsSprinting)
            {
                IsSprinting = true;
                moveSpeed = sprintSpeed;
                //if (AudioManager.instance != null) AudioManager.instance.PlaySound("Sprint");
            }
            //Si la touche avancé est relaché ou que l'énergie est vide on arrête de sprinter
        }
        else if (((Vertical <= 0) || energyAmount <= 0) && IsSprinting)
        {
            CancelSprint();
        }

        //Energy management
        if (IsSprinting)
        {
            energyAmount -= energyBurnSpeed * Time.deltaTime;
        }
        else RegenEnergy();
    }

    //Cancel sprint movement
    private void CancelSprint()
    {
        if (IsSprinting)
        {
            IsSprinting = false;
            moveSpeed = walkSpeed;
            //if (AudioManager.instance != null) AudioManager.instance.PlaySound("StopSprint");
        }
    }

    private void Crouch()
    {
        //change the hitbox when crouching

        Debug.Log("Start crouching");
        IsCrouching = true;
        characterController.height = 1;
        Vector3 center = characterController.center;
        center.y = 0.5f;
        characterController.center = center;
        Vector3 direction = new Vector3(Horizontal, 0, Vertical).normalized;
            
        //if walking while crouching change the hitbox
        if (direction.magnitude == 0) return;
            
        characterController.height = 1.2f;
        center.y = 0.6f;
        characterController.center = center;

    }

    private void CancelCrouching()
    {
        Debug.Log("Stop crouching");
        IsCrouching = false;
        //if (anim != null) anim.SetBool("isCrouching", false);
        characterController.height = 1.5f;
        var center = characterController.center;
        center.y = 0.8f;
        characterController.center = center;
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            yVelocity = 0;
            if (IsJumping)
            {
                IsJumping = false;
            }
            if (isPressingJumpKey)
            {
                yVelocity = jumpVelocity;
                IsJumping = true;
            }
        }
        else
        {
            yVelocity = movementDirection.y;

        }
    }

    private void RegenEnergy()
    {
        if (energyAmount < maxEnergyAmount) energyAmount += energyRegenSpeed * Time.deltaTime;

        //Makes sure energyAmount can't go out the boundaries of 0 / max Energy
        if (energyAmount < 0 || energyAmount > 100) energyAmount = Mathf.Clamp(energyAmount, 0, maxEnergyAmount);
    }

    #region Input Reading

    [UsedImplicitly]
    private void OnMove(InputValue value)
    {
        if (!hasAuthority) return;
        
        Horizontal = value.Get<Vector2>().x;
        Vertical = value.Get<Vector2>().y;
    }

    [UsedImplicitly]
    private void OnSprint()
    {
        if (!hasAuthority) return;
        
        sprintFirstTap = true;
        StartCoroutine(SprintWaitForSecondPress());
    }
    private IEnumerator SprintWaitForSecondPress()
    {
        yield return new WaitForSeconds(SprintTimeBeforeSecondPress);
        sprintFirstTap = false;
    }

    [UsedImplicitly]
    private void OnJump(InputValue value)
    {
        if (!hasAuthority) return;

        var isKeyPushed = Math.Abs(value.Get<float>()) >= 1;
        
        isPressingJumpKey = isKeyPushed;
    }

    [UsedImplicitly]
    private void OnCrouch(InputValue value)
    {
        if (!hasAuthority) return;
        
        var isKeyPushed = Math.Abs(value.Get<float>()) >= 1;
        
        if (isKeyPushed) Crouch();
        else CancelCrouching();
    }
    
    #endregion
    
}