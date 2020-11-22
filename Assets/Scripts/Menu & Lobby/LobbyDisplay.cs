using Mirror;
using Network;
using TMPro;
using UnityEngine;

public class LobbyDisplay : MonoBehaviour
{
    public static LobbyDisplay Instance;

    [SerializeField] private TMP_Text[] team1PlayerNameTexts = new TMP_Text[0];
    [SerializeField] private TMP_Text[] team1PlayerReadyTexts = new TMP_Text[0];
    [SerializeField] private TMP_Text[] team2PlayerNameTexts = new TMP_Text[0];
    [SerializeField] private TMP_Text[] team2PlayerReadyTexts = new TMP_Text[0];

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < team1PlayerNameTexts.Length; i++)
        {
            team1PlayerNameTexts[i].text = string.Empty;
            team1PlayerReadyTexts[i].text = string.Empty;
            team2PlayerNameTexts[i].text = string.Empty;
            team2PlayerReadyTexts[i].text = string.Empty;
        }

        int team1Players = 0;
        int team2Players = 0;
        foreach (NetworkRoomPlayer playerSlot in NetworkRoomManagerExt.Instance.roomSlots)
        {
            NetworkRoomPlayerExt player = playerSlot.GetComponent<NetworkRoomPlayerExt>();

            if (player.TeamID == 1)
            {
                team1PlayerNameTexts[team1Players].text = player.Username;
                team1PlayerReadyTexts[team1Players].text = player.readyToBegin ?
                    "<color=green>Ready</color>" :
                    "<color=red>Not Ready</color>";
                team1Players++;
            }
            else
            {
                team2PlayerNameTexts[team2Players].text = player.Username;
                team2PlayerReadyTexts[team2Players].text = player.readyToBegin ?
                    "<color=green>Ready</color>" :
                    "<color=red>Not Ready</color>";
                team2Players++;
            }
        }
    }
}