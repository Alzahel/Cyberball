using Cyberball;
using Health;
using JetBrains.Annotations;
using Managers;
using Mirror;
using Network;
using Telepathy;
using UnityEngine;

public class Ball : NetworkBehaviour
{
    public Transform spawnPosition;
    [SerializeField] private float onPlayerPosition = 3;
        
    private NetworkGamePlayer player;
    private bool isBallCarried;

    private Transform ballTransform;
    private Vector3 ballPosition;

    private void Awake()
    {
        ballTransform = transform;
        ballPosition = ballTransform.position;
        
        
    }

    private void Start()
    {
        ResetBall();
    }

    private void Update()
    {
        if (player != null && player.GetComponent<HealthSystem>().IsDead && isBallCarried) DropBall();
    }
    void OnTriggerEnter(Collider col)
    {
        //take the Ball
        if (col.CompareTag("Player"))
        {
            Debug.Log("Player " + col.name + "took the ball !");
            CatchBall(col);
        }

        if (!col.CompareTag($"Goal")) return;
        if (col.GetComponent<Goal>().GoalTeamID == player.TeamID) return;

        GameManager.Instance.ScoreGoal(player);
        ResetBall();
    }

    private void CatchBall(Collider col)
    {
        if (isBallCarried) return;
        else isBallCarried = true;
        
        var colTransform = col.transform;
        player = colTransform.GetComponent<NetworkGamePlayer>();
            
        Debug.Log("Player " + colTransform.name + "took the ball !");

        var colPosition = colTransform.position;

        ballPosition = new Vector3(colPosition.x, colPosition.y + onPlayerPosition, colPosition.z);
            
        ballTransform.SetParent(colTransform);
        ballTransform.position = ballPosition;
    }
    
    private void DropBall()
    {
        isBallCarried = false;
        Debug.Log("The ball has been dropped !");
        ballPosition = transform.position;
        ballPosition = new Vector3(ballPosition.x, 1+ ballPosition.y - onPlayerPosition, ballPosition.z);
           
        ballTransform.parent = null;
        ballTransform.position = ballPosition;

        player = null;
    }

    private void ResetBall()
    {
        Debug.Log("reset");
        DropBall();
        transform.position = spawnPosition.position;
    }

    [UsedImplicitly]
    private void OnDropBall()
    {
        if (Input.GetButtonDown($"dropBall")) DropBall();
    }
}