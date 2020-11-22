using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MADE WITH https://youtu.be/Fx8efi2MNz0?list=PLS6sInD7ThM1aUDj8lZrF4b4lpvejB2uB
/// </summary>

public class PlayerNameInput : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button continueButton;

    public static string DisplayName { get; private set; }

    private const string PlayerPrefNameKey = "PlayerName";

    private void Start() => SetupInputField();

    private void SetupInputField()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefNameKey)) return;

        var defaultName = PlayerPrefs.GetString(PlayerPrefNameKey);

        nameInputField.text = defaultName;

        SetPlayerName(defaultName);
    }

    public void SetPlayerName(string defaultName)
    {
        continueButton.interactable = !string.IsNullOrEmpty(name);
    }

    public void SavePlayerName()
    {
        DisplayName = nameInputField.text;

        PlayerPrefs.SetString(PlayerPrefNameKey, DisplayName);
    }

}