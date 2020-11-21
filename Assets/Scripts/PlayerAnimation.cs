using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimation : NetworkBehaviour
{
    private Animator anim;
    private PlayerMovement movement;

    void Awake()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {
            anim.SetFloat("x", movement.Horizontal, 0, Time.deltaTime);
            anim.SetFloat("y", movement.Vertical, 0, Time.deltaTime);
            anim.SetBool("isSprinting", movement.IsSprinting);
            anim.SetBool("isCrouching", movement.IsCrouching);
        }
       
    }
}
