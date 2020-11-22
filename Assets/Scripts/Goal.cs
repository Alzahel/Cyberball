using UnityEngine;

namespace Cyberball
{
    public class Goal :Mirror.NetworkBehaviour
    {
        [SerializeField]
        private int goalTeamID;

        public int GoalTeamID { get => goalTeamID; set => goalTeamID = value; }
    }
}
