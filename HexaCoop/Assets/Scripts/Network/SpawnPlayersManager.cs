using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;

public class SpawnPlayersManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public bool SpawnDummyPlayerOnStart;

    public static SpawnPlayersManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnPlayer("P" + GetPlayerCounter(), false);

        if(SpawnDummyPlayerOnStart)
        {
            SpawnDummyPlayer(true);
        }
    }

    private int GetPlayerCounter() => NetworkHelper.instance.GetAllPlayers().Count();

    public void SpawnDummyPlayer(bool isAi)
    {
        var aiPrefix = isAi ? "AI_" : "";
        var playerCounter = GetPlayerCounter();
        if (playerCounter == 1)
        {
            SpawnPlayer(aiPrefix + "P2", true, isAi);
        }
        else if (playerCounter == 2)
        {
            SpawnPlayer(aiPrefix + "P3", true, isAi);
        }
        else if (playerCounter == 3)
        {
            SpawnPlayer(aiPrefix + "P4", true, isAi);
        }
    }

    public void SpawnPlayer(string name, bool isDummy, bool useAi = false)
    {
        if (!PhotonNetwork.IsConnected)
        {
            Textt.GameLocal("PUN not connected");
            return;
        }

        var isAi = isDummy && useAi;

        if (!PhotonNetwork.OfflineMode && !isAi)
        {
            name = PhotonNetwork.NickName;
        }

        object[] myCustomInitData = new List<object> { name, isAi, GetPlayerCounter() }.ToArray();
        var player = PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(0, 0), Quaternion.identity, 0, myCustomInitData);
    }
}