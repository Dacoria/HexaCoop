using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void PlayerAbilityNotExecuted(PlayerScript player, Hex hexTile, AbilityType abilityType, int queueId = -1)
    {
        photonView.RPC("RPC_AE_PlayerAbilityNotExecuted", RpcTarget.All, player.Id, (Vector3)hexTile.HexCoordinates, abilityType, queueId);
    }

    [PunRPC]
    public void RPC_AE_PlayerAbilityNotExecuted(int pId, Vector3 hexTile, AbilityType abilityType, int queueId)
    {
        ActionEvents.PlayerAbilityNotExecuted?.Invoke(pId.GetPlayer(), hexTile.GetHex(), abilityType, queueId);
    }
}