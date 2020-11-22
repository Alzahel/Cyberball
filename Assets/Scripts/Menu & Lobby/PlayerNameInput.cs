using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cyberball
{

    /// <summary>
    /// MADE WITH https://youtu.be/Fx8efi2MNz0?list=PLS6sInD7ThM1aUDj8lZrF4b4lpvejB2uB
    /// </summary>

    public class PlayerNameInput : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private TMP_InputField nameinputField = null;
        [SerializeField] private Button continueButton = null;

        public static string DisplayName { get; private set; }

        private const string PlayerPrefNameKey = "PlayerName";

        private void Start() => SetupInputField();

        private void SetupInputField()
        {
            if (!PlayerPrefs.HasKey(PlayerPrefNameKey)) return;

            string defaultName = PlayerPrefs.GetString(PlayerPrefNameKey);

            nameinputField.text = defaultName;

            SetPlayerName(defaultName);
        }

        public void SetPlayerName(string defaultName)
        {
            continueButton.interactable = !string.IsNullOrEmpty(name);
        }

        public void SavePlayerName()
        {
            DisplayName = nameinputField.text;

            PlayerPrefs.SetString(PlayerPrefNameKey, DisplayName);
        }

    }
}