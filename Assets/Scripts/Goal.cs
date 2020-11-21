using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Goal :Mirror.NetworkBehaviour
{
    [SerializeField]
    private int goalTeamID;

    public int GoalTeamID { get => goalTeamID; set => goalTeamID = value; }
}
