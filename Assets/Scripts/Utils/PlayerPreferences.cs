using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utils
{
    public static class PlayerPreferences
    {

        public static event Action SettingsDatasChanged ;
        
        public struct SettingsDatas
        {
            public float MouseSensitivity;
            public float XCameraSensitivity;
            public float YCameraSensitivity;
        }
        
        public static void SaveSettings(float mouseSensitivity, float xCameraSensitivity, float yCameraSensitivity)
        {
            SettingsDatas data = new SettingsDatas();
            
            data.MouseSensitivity = mouseSensitivity;
            data.XCameraSensitivity = xCameraSensitivity;
            data.YCameraSensitivity = yCameraSensitivity;
            
            var serializedObject = JsonUtility.ToJson(data);
            PlayerPrefs.SetString("playerSettings", serializedObject);
            
            SettingsDatasChanged?.Invoke();
        }
        
        public static SettingsDatas GetSettings()
        {
            if (!PlayerPrefs.HasKey("playerSettings")) SaveSettings(10, 100, 100);
            
            string serializedObject = PlayerPrefs.GetString("playerSettings");
            SettingsDatas data = JsonUtility.FromJson<SettingsDatas>(serializedObject);
            return data;
        }
    }
}