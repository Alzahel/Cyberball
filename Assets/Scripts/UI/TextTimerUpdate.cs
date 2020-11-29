using TMPro;
using UnityEngine;

namespace UI
{
    public class TextTimerUpdate : MonoBehaviour
    {
        private TextMeshProUGUI timerText;

        private void Awake()
        {
            timerText = GetComponent<TextMeshProUGUI>();
        }

        public void UpdateTimerText(float value)
        {
            timerText.text = value.ToString();
        }
    
    }
}
