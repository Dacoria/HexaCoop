using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void Invoker_PlayerAbility(PlayerScript player, Hex hexTile, AbilityType abilityType, int queueId = -1)
    {
        photonView.RPC("RPC_AE_PlayerAbility", RpcTarget.All, player.Id, (Vector3)hexTile.HexCoordinates, abilityType, queueId);
    }

    [PunRPC]
    public void RPC_AE_PlayerAbility(int pId, Vector3 hexTile, AbilityType abilityType, int queueId)
    {
        ActionEvents.PlayerAbility?.Invoke(pId.GetPlayer(), hexTile.GetHex(), abilityType, queueId);
    }
}