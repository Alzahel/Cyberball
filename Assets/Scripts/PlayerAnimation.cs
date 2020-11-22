using Mirror;
using UnityEngine;

namespace Cyberball
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerAnimation : NetworkBehaviour
    {
        private Animator anim;
        private PlayerMovement movement;
        
        private static readonly int IsSprinting = Animator.StringToHash("isSprinting");
        private static readonly int IsCrouching = Animator.StringToHash("isCrouching");
        private static readonly int Y = Animator.StringToHash("y");
        private static readonly int X = Animator.StringToHash("x");

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
                anim.SetFloat(X, movement.Horizontal, 0, Time.deltaTime);
                anim.SetFloat(Y, movement.Vertical, 0, Time.deltaTime);
                anim.SetBool(IsSprinting, movement.IsSprinting);
                anim.SetBool(IsCrouching, movement.IsCrouching);
            }
       
        }
    }
}
