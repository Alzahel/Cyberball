using UnityEngine;

namespace Utils
{
    public class BarFill : MonoBehaviour
    {
        [SerializeField] private RectTransform barFill;
        
        public void SetBarFillAmount(float amount)
        {
            barFill.localScale = new Vector3(1f, amount, 1f);
        }
    }
}