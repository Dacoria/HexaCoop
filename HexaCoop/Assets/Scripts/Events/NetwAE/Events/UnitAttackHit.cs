using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void UnitAttackHit(IUnit unit, Hex hexTile, int damage)
    {
        photonView.RPC("RPC_AE_UnitAttackHit", RpcTarget.All, unit.Id, (Vector3)hexTile.HexCoordinates, damage);
    }

    [PunRPC]
    public void RPC_AE_UnitAttackHit(int unitId, Vector3 hexTile, int damage)
    {
        ActionEvents.UnitAttackHit?.Invoke(unitId.GetUnit(), hexTile.GetHex(), damage);
    }
}