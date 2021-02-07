using Managers;
using Mirror;
using UnityEngine;

namespace Utils
{
    public class DisableIfLocalPlayer : MonoBehaviour
    {
        private void OnEnable()
        {

            if (transform.root.GetComponent<NetworkIdentity>().hasAuthority)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
