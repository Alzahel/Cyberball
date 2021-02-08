using System;
using System.Collections;
using Cinemachine;
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
    public bool HasEnergy { get; set; } = true;
    private bool sprintFirstTap;
    private const float SprintTimeBeforeSecondPress = 0.2f;

    //Movement input
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; } 
        
    //Zoom
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject camZoom;

    //States
    public bool IsCrouching{ get; private set; }
    public bool IsJumping{ get; private set; }
    public bool IsSprinting{ get; private set; }
    public bool IsAiming{ get; private set; }
    
    //Events
    public event EventHandler<OnMoveEventArgs> OnMove;

    public class OnMoveEventArgs : EventArgs
    {
        public float X;
        public float Y;
    }
    public event EventHandler<OnSprintEventArgs> OnSprint;

    public class OnSprintEventArgs : EventArgs
    {
        public bool IsSprinting;
    }
    public event EventHandler<OnCrouchEventArgs> OnCrouch;
    public class OnCrouchEventArgs : EventArgs
    {
        public bool IsCrouching;
    }
    
    public event EventHandler<OnAimEventArgs> OnAim;
    public class OnAimEventArgs : EventArgs
    {
        public bool IsAiming;
    }
    
    // Start is called before the first frame update
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        camZoom.SetActive(false);
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        
        moveSpeed = walkSpeed;
        //playerShoot.Shot += StopSpecialMovements;

        //to see the physic equations look at :https://youtu.be/PlT44xr0iW0?list=PLFt_AvWsXl0f0hqURlhyIoAabKPgRsqjz&t=351
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpPeak, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpPeak;
    }
    
    //Cancel all special Movemennts : Sprint / Dodge / WallJump / Dash / Slide / wallrun...
    public void CancelAllMovements()
    {
        Horizontal = 0;
        Vertical = 0;
        movementDirection = Vector3.zero;
        CancelSprint();
        if (IsCrouching) CancelCrouching();
    }
    
    //Cancel all special Movemennts : Sprint / Dodge / WallJump / Dash / Slide / wallrun...
    public void CancelAllSpecialMovements()
    {
        CancelSprint();
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
        
        OnMove?.Invoke(this, new OnMoveEventArgs{X=movementDirection.x, Y = movementDirection.y});
    }
    
    private void Sprint()
    {
        if (!HasEnergy) return;
        
        //Si on détecte le firstTap et que dans la durée autorisée (SprintWaitForSecondPress) on détecte un appui de la touche "avancer" alors on sprint
        //Le second press après le tap (OnSprint) est géré avec une coroutine qui donne au joueur une certaines durée pour réappuyer sur la touche "avancer" 
        //qui sera détectée  dans le OnMove car l'input manager ne gère pas des séries d'action qui permettrait de détecter le tap puis le press
        if (sprintFirstTap)
        {
            if (Vertical > 0 && !IsSprinting)
            {
                IsSprinting = true;
                moveSpeed = sprintSpeed;

                OnSprint?.Invoke(this, new OnSprintEventArgs {IsSprinting = IsSprinting});
            }
            //Si la touche avancé est relaché ou que l'énergie est vide on arrête de sprinter
        }
        else if (((Vertical <= 0)) && IsSprinting)
        {
            CancelSprint();
        }
    }

    //Cancel sprint movement
    private void CancelSprint()
    {
        if (IsSprinting)
        {
            IsSprinting = false;
            moveSpeed = walkSpeed;
            
            OnSprint?.Invoke(this, new OnSprintEventArgs{IsSprinting = IsSprinting});
        }
    }

    private void Crouch()
    {
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
        
        OnCrouch?.Invoke(this, new OnCrouchEventArgs{IsCrouching = IsCrouching});
    }

    private void CancelCrouching()
    {
        Debug.Log("Stop crouching");
        
        IsCrouching = false;
        characterController.height = 1.5f;
        var center = characterController.center;
        center.y = 0.8f;
        characterController.center = center;
        
        OnCrouch?.Invoke(this, new OnCrouchEventArgs{IsCrouching = IsCrouching});
    }

    private void Aim()
    {
        IsAiming = true;
        cam.SetActive(false);
        camZoom.SetActive(true);
    }

    private void CancelAim()
    {
        IsAiming = false;
        camZoom.SetActive(false);
        cam.SetActive(true);
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

    #region Input Reading

    [UsedImplicitly]
    private void OnMoveInput(InputValue value)
    {
        if (!hasAuthority) return;
        
        Horizontal = value.Get<Vector2>().x;
        Vertical = value.Get<Vector2>().y;
    }

    [UsedImplicitly]
    private void OnSprintInput()
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
    private void OnJumpInput(InputValue value)
    {
        if (!hasAuthority) return;

        var isKeyPushed = Math.Abs(value.Get<float>()) >= 1;
        
        isPressingJumpKey = isKeyPushed;
    }

    [UsedImplicitly]
    private void OnCrouchInput(InputValue value)
    {
        if (!hasAuthority) return;
        
        var isKeyPushed = Math.Abs(value.Get<float>()) >= 1;
        
        if (isKeyPushed) Crouch();
        else CancelCrouching();
    }
    
    [UsedImplicitly]
    private void OnAimInput(InputValue value)
    {
        if (!hasAuthority) return;
        
        var isKeyPushed = Math.Abs(value.Get<float>()) >= 1;
        
        if (isKeyPushed) Aim();
        else CancelAim();
    }
    
    #endregion
    
}