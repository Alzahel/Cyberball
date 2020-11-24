using UnityEngine;

namespace Utils
{
    public class BarFill : MonoBehaviour
    {
        [SerializeField] private RectTransform barFill;

        protected void SetBarFillAmount(float yAmount = 1, float xAmount = 1)
        {
            barFill.localScale = new Vector3(xAmount, yAmount, 1f);
        }
    }
}