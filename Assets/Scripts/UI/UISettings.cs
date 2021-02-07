using System;
using JetBrains.Annotations;
using Managers;
using Mirror;
using Network;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class UISettings : Mirror.NetworkBehaviour
    {
        [SerializeField] private GameObject settings;

        [SerializeField] private Slider mouseSensitivitySlider;
        [SerializeField] private InputField mouseSensitivityInputField;
        [SerializeField] private Slider xCamSensitivitySlider;
        [SerializeField] private InputField xCamSensitivityInputField;
        [SerializeField] private Slider yCamSensitivitySlider;
        [SerializeField] private InputField yCamSensitivityInputField;

        private GameObject player;
        private PlayerPreferences.SettingsDatas settingsDatas;

        //private Player player = null;
        //private CinemachineVirtualCamera playerCam;
        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            settings.SetActive(false);
        }
        
        void Start()
        {
            settingsDatas = PlayerPreferences.GetSettings();

            mouseSensitivitySlider.maxValue = 10;
            xCamSensitivitySlider.maxValue = 100;
            yCamSensitivitySlider.maxValue = 100;

            mouseSensitivitySlider.value = settingsDatas.MouseSensitivity;
            xCamSensitivitySlider.value = settingsDatas.XCameraSensitivity;
            yCamSensitivitySlider.value = settingsDatas.YCameraSensitivity;
        }


        private void Update()
        {
            if(!mouseSensitivityInputField.isFocused) mouseSensitivityInputField.text = mouseSensitivitySlider.value.ToString();
            if(!xCamSensitivityInputField.isFocused) xCamSensitivityInputField.text = xCamSensitivitySlider.value.ToString();
            if (!yCamSensitivityInputField.isFocused) yCamSensitivityInputField.text = yCamSensitivitySlider.value.ToString();

            mouseSensitivitySlider.value = float.Parse(mouseSensitivityInputField.text);
            xCamSensitivitySlider.value = float.Parse(xCamSensitivityInputField.text);
            yCamSensitivitySlider.value = float.Parse(yCamSensitivityInputField.text);
        }
        

        [UsedImplicitly]
        private void OnSettings()
        {
            settings.SetActive(!settings.activeSelf);
            
            
            if (SceneManager.GetActiveScene().name.StartsWith("Scene_Map")) ChangeCursorState();
        }

        private void ChangeCursorState()
        {
            player = ClientScene.localPlayer.gameObject;
            
            if(settings.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                player.GetComponent<PlayerInput>().enabled = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                player.GetComponent<PlayerInput>().enabled = true;
            }
           
        }

        public void saveSettings()
        {
            PlayerPreferences.SaveSettings(mouseSensitivitySlider.value, xCamSensitivitySlider.value, yCamSensitivitySlider.value);
        }
    }
}
