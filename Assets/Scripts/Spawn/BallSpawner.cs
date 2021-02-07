using Managers;
using Mirror;
using UnityEngine;

namespace Spawn
{
    public class BallSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject ball;
        
        // Start is called before the first frame update
        void Start()
        {
            GameObject ball = Instantiate(this.ball);
            ball.GetComponent<Ball>().spawnPosition = transform;
        }
    }
}
