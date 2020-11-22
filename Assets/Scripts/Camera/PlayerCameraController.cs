using Cinemachine;
using Mirror;
using UnityEngine;

namespace Cyberball.Camera
{
    public class PlayerCameraController : NetworkBehaviour
    {
        [SerializeField]  Cinemachine3rdPersonAim playerCamera = null;

        public AxisState xAxis;
        public AxisState yAxis;

        [SerializeField] private Transform cameraLookAt = null;

        [SerializeField] private float turnSpeed = 15;

        //zoom
        private Animator animator;
        int isAimingParam = Animator.StringToHash("isAiming");

        private void Awake()
        {
            playerCamera.gameObject.SetActive(false);
        }

        private void setXAxis()
        {
            if (hasAuthority) xAxis.Value = transform.rotation.eulerAngles.y;
        }

        private void OnEnable()
        {
            //Set the xaxis to be the transform forward otherwise the camera turns the player to 0 on start
            setXAxis();
        }


        public override void OnStartAuthority()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            animator = GetComponent<Animator>();

            if (playerCamera == null) return;
            playerCamera.gameObject.SetActive(true);

            //needs to be called here because on the first Enable authority isn't given yet
            setXAxis();
        }
    

        // Update is called once per frame
        void FixedUpdate()
        {//Control the follow of the camera
        
            if (hasAuthority)
            {
                xAxis.Update(Time.fixedDeltaTime);
                yAxis.Update(Time.fixedDeltaTime);
                cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);

                //Rotate the character towards the camera
                float yawCamera = UnityEngine.Camera.main.transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
            }
        
        }

        private void Update()
        {
            if (hasAuthority)
            {
                bool isAiming = Input.GetMouseButton(1);
                //animator.SetBool(isAimingParam, isAiming);
            }
        }

        /* private void OnAim(InputValue value)
     {
         if (value.Get<float>() == 1 && animator.GetBool(isAimingParam) == false) animator.SetBool(isAimingParam, true);
         else animator.SetBool(isAimingParam, false);
     }*/
    }
}
