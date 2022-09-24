using Photon.Pun;
using UnityEngine;

public class SyncedMetaData : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    public int Id;

    private Hex StartSpawnHex;
    private Vector3 StartPos;
    private Quaternion StartRot;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        // OnPhotonInstantiate seems to always go after awake but before start
        object[] instantiationData = info.photonView.InstantiationData;
        this.Id = (int)instantiationData[0];
        Vector3? hexCoordinates = string.IsNullOrEmpty(instantiationData[1].ToString()) ? null : instantiationData[1].ToString().ToVector3();

        if (GetComponent<IObjectOnTile>() != null  && hexCoordinates.HasValue)
        {
            GetComponent<IObjectOnTile>().SetCurrentHexTile(hexCoordinates.Value.GetHex());
            StartSpawnHex = hexCoordinates.Value.GetHex();
        }

        var resourcePool = ObjectNetworkInitManager.instance.SpawnedNetworkObjects;
        resourcePool.Add(this.Id, this.gameObject);

        StartPos = transform.position;
        StartRot = transform.rotation;
    }    

    public void ResetUnit()
    {
        gameObject.SetActive(true);
        if (GetComponent<IUnit>() != null)
        {
            GetComponent<IUnit>().SetCurrentHexTile(StartSpawnHex);
        }

        transform.position = StartPos;
        transform.rotation = StartRot;
    }
}