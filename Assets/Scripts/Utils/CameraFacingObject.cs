using UnityEngine;

namespace Utils
{
    public class CameraFacingObject : MonoBehaviour
    {
        private Camera cam;
        private Quaternion rotation;
        private void Awake()
        {
            cam = Camera.main;
            
        }

        private void Update()
        {
            if (!(cam is null)) rotation = cam.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
        }
    }
}