using Managers;
using UnityEngine;

namespace UI.Killfeed
{
    public class Killfeed : MonoBehaviour
    {

        [SerializeField] GameObject killfeedItemPrefab= null;
    

        // Start is called before the first frame update
        void Start()
        {
            GameManager.Instance.OnPlayerKilled += OnKill;
        }

        private void OnKill(object sender, GameManager.PlayerKilledEventArgs e)
        {
            GameObject go = Instantiate(killfeedItemPrefab, this.transform);
            go.GetComponent<KillfeedItem>().setup(e.PlayerKilled, e.Source);
            go.transform.SetAsFirstSibling();
            Destroy(go, 5f);
        }
    }
}
