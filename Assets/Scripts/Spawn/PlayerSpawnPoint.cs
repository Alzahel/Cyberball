using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [SerializeField] private uint teamID;

        public uint TeamID { get => teamID; set => teamID = value; }

        private void Awake() => PlayerSpawnSystem.AddSpawnPoint(this);
        private void OnDestroy() => PlayerSpawnSystem.RemoveSpawnPoint(this);

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.25f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
        }
    }
}