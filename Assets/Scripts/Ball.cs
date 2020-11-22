using Cyberball;
using Cyberball.Managers;
using Cyberball.Network;
using JetBrains.Annotations;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
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
        if (player != null && player.IsDead) DropBall();
    }
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Player " + col.name + "took the ball !");
        //take the Ball
        if (col.CompareTag("Player"))
        {
            CatchBall(col);
        }

        if (col.CompareTag($"Goal") && col.GetComponent<Goal>().GoalTeamID != player.TeamID) 
        { 
            GameManager.instance.ScoreGoal(player.TeamID);
            ResetBall();
        }
    }

    private void CatchBall(Collider col)
    {
        if (isBallCarried) return;
        else isBallCarried = true;
            
        Debug.Log("Player " + col.name + "took the ball !");

        player = col.GetComponent<NetworkGamePlayer>();

        var colTransform = col.transform;
        var colPosition = colTransform.position;

        ballPosition = new Vector3(colPosition.x, colPosition.y + onPlayerPosition, colPosition.z);
            
        ballTransform.SetParent(colTransform);
        ballTransform.position = ballPosition;
    }

    private void DropBall()
    {
        isBallCarried = false;
        Debug.Log("The ball has been dropped !");
        ballPosition = new Vector3(ballPosition.x, ballPosition.y - onPlayerPosition, ballPosition.z + 1);
           
        ballTransform.parent = null;
        ballTransform.position = ballPosition;

        player = null;
    }      

    private void ResetBall()
    {
        DropBall();
        transform.position = spawnPosition.position;
    }
        
    [UsedImplicitly]
    private void OnDropBall()
    {
        if (Input.GetButtonDown($"dropBall")) DropBall();
    }
}