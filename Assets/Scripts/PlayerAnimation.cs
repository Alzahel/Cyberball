using System;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimation : NetworkBehaviour
{
    private Animator anim;
    private PlayerMovement playerMovement;
        
    private static readonly int IsSprinting = Animator.StringToHash("isSprinting");
    private static readonly int IsCrouching = Animator.StringToHash("isCrouching");
    private static readonly int Y = Animator.StringToHash("y");
    private static readonly int X = Animator.StringToHash("x");

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    #region Event Subscribing

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        playerMovement.OnMove += MoveAnimation;
        playerMovement.OnSprint += SprintAnimation;
        playerMovement.OnCrouch += OnCrouchAnimation;

    }

    public override void OnStopAuthority()
    {
        base.OnStopAuthority();

        playerMovement.OnMove -= MoveAnimation;
        playerMovement.OnSprint -= SprintAnimation;
        playerMovement.OnCrouch -= OnCrouchAnimation;

    }

    #endregion

    #region Animation

    private void MoveAnimation(object sender, EventArgs e)
    {
        anim.SetFloat(X, playerMovement.Horizontal, 0, Time.deltaTime);
        anim.SetFloat(Y, playerMovement.Vertical, 0, Time.deltaTime);
    }
        
    private void SprintAnimation(object sender, PlayerMovement.OnSprintEventArgs e)
    {
        anim.SetBool(IsSprinting, e.IsSprinting);
    }

    private void OnCrouchAnimation(object sender, EventArgs e)
    {
        anim.SetBool(IsCrouching, true);
    }

    #endregion
        
}