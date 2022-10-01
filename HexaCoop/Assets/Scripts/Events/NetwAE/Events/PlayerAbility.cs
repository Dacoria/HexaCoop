using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void Invoker_PlayerAbility(PlayerScript player, Hex hexTile, Hex hexTile2, AbilityType abilityType, int queueId = -1)
    {
        var hexTile2V3 = hexTile2 != null ? (Vector3)hexTile2.HexCoordinates : Utils.DefaultEmptyV3;
        photonView.RPC("RPC_AE_PlayerAbility", RpcTarget.All, player.Id, (Vector3)hexTile.HexCoordinates, hexTile2V3, abilityType, queueId);
    }

    [PunRPC]
    public void RPC_AE_PlayerAbility(int pId, Vector3 hexTile, Vector3 hexTile2, AbilityType abilityType, int queueId)
    {
        var hex2Tile = hexTile2.IsDefaultEmptyVector() ? null : hexTile2.GetHex();
        ActionEvents.PlayerAbility?.Invoke(pId.GetPlayer(), hexTile.GetHex(), hex2Tile, abilityType, queueId);
    }
}