using UnityEngine;
using UnityEngine.UI;

namespace Cyberball
{
    class UiLobbySetup : MonoBehaviour
    {
        [SerializeField] public Button startButton = null;
        [SerializeField] public Button changeTeam1 = null;
        [SerializeField] public Button changeTeam2 = null;
        [SerializeField] public Button readyButton = null;

        public static UiLobbySetup instance = null;

        private void Awake()
        {
            if (instance == null) instance = this;

            startButton.gameObject.SetActive(false);
            startButton.interactable = false;
        }


    }
}
