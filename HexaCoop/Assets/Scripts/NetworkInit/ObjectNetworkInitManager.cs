using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectNetworkInitManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        instance = this;
        var enemies = Rsc.GoEnemiesOrObjMap; // nodig voor inladen resource in PUN
    }

    public static ObjectNetworkInitManager instance;
    public Dictionary<int, GameObject> SpawnedNetworkObjects = new Dictionary<int, GameObject>();

    public InstantiationInfo InstantiatePunObject(GameObject go)
    {
        var hextile = go.GetComponent<IObjectOnTile>()?.CurrentHexTile;
        if(hextile != null)
        {
            return InstantiatePunObject(go.name.Replace("(Clone)", "").Trim(), hextile.OrigPosition + new Vector3(0,1,0), go.transform.rotation, currentHex: hextile);
        }
        else
        {
            return InstantiatePunObject(go.name.Replace("(Clone)", "").Trim(), go.transform.position, go.transform.rotation);
        }
    }

    private InstantiationInfo InstantiatePunObject(string resourceKey, Vector3 spawnPos, Quaternion rotation, Hex currentHex = null)
    {
        var resourceId = MonoHelper.instance.GenerateNewId();        

        var data = new object[]
        {
            resourceId,
            currentHex != null ? (Vector3)currentHex.HexCoordinates : null
        };

        var go = PhotonNetwork.Instantiate(resourceKey, spawnPos, rotation, 0, data);

        return new InstantiationInfo
        {
            GameObject = go,
            ResourceId = resourceId,
        };
    }

    private void Start() => ActionEvents.NewRoundStarted += OnNewRoundStarted;

    private void OnDestroy() => ActionEvents.NewRoundStarted -= OnNewRoundStarted;

    private void OnNewRoundStarted(List<PlayerScript> arg1, PlayerScript arg2)
    {
        foreach (var goDict in SpawnedNetworkObjects)
        {
            goDict.Value.GetComponent<SyncedMetaData>().ResetUnit();
        }
    }
}