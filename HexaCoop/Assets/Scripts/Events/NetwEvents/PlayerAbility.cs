using Photon.Pun;
using UnityEngine;

public partial class NetworkActionEvents : MonoBehaviour
{
    public void PlayerAbility(PlayerScript player, Hex hexTile, AbilityType abilityType)
    {
        photonView.RPC("RPC_AE_PlayerAbility", RpcTarget.All, player.PlayerId, (Vector3)hexTile.HexCoordinates, abilityType);
    }

    [PunRPC]
    public void RPC_AE_PlayerAbility(int pId, Vector3 hexTile, AbilityType abilityType)
    {
        ActionEvents.PlayerAbility?.Invoke(pId.GetPlayer(), hexTile.GetHex(), abilityType);
    }
}