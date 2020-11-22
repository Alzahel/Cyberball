using UnityEngine;
using UnityEngine.UI;

class UiLobbySetup : MonoBehaviour
{
    [SerializeField] public Button startButton;
    [SerializeField] public Button changeTeam1;
    [SerializeField] public Button changeTeam2;
    [SerializeField] public Button readyButton;

    public static UiLobbySetup Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        startButton.gameObject.SetActive(false);
        startButton.interactable = false;
    }


}