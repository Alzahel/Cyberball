using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace Assets.Scripts
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] Transform spawnPosition;
        Vector3 position;
        [SerializeField] private float onPlayerPosition = 3;

        private NetworkGamePlayer player;

        private bool isCarried;

        void Start()
        {
            position = transform.position;
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
            if (col.tag == "Player")
            {
                CatchBall(col);
            }

            if (col.tag == "Goal" && col.GetComponent<Goal>().GoalTeamID != player.TeamID) 
            { 
                GameManager.instance.ScoreGoal(player.TeamID);
                ResetBall();
            }
        }

        private void OnDropBall()
        {
            if (Input.GetButtonDown("dropBall")) DropBall();
        }

        private void CatchBall(Collider col)
        {
            if (isCarried) return;

            Debug.Log("Player " + col.name + "took the ball !");

            player = col.GetComponent<NetworkGamePlayer>();
            isCarried = true;

            transform.SetParent(col.transform);
            position = new Vector3(col.transform.position.x, col.transform.position.y + onPlayerPosition, col.transform.position.z);
            transform.position = position;
        }

        private void DropBall()
        {
            isCarried = false;
            Debug.Log("The ball has been droped !");
            position = new Vector3(transform.position.x, transform.position.y - onPlayerPosition, transform.position.z + 1);
            transform.parent = null;

            transform.position = position;

            player = null;
        }      

        private void ResetBall()
        {
            DropBall();
            transform.position = spawnPosition.position;
        }
    }
}