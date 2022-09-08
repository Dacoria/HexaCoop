using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void UnitMovingFinished__NON_NETWORK(IUnit unit, Hex hexTile)
    {
        photonView.RPC("RPC_AE_UnitMovingFinished", RpcTarget.All, unit.Id, (Vector3)hexTile.HexCoordinates);
    }   

    [PunRPC]
    public void RPC_AE_UnitMovingFinished(int unitId, Vector3 hexTile)
    {
        ActionEvents.UnitMovingFinished?.Invoke(unitId.GetUnit(), hexTile.GetHex());
    }
}