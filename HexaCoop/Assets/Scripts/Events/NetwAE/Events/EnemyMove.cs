using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class NetworkAE : MonoBehaviour
{    
    public void EnemyMove(EnemyScript enemy, Hex hexTile)
    {
        photonView.RPC("RPC_AE_EnemyMove", RpcTarget.All, enemy.Id, (Vector3)hexTile.HexCoordinates);
    }

    [PunRPC]
    public void RPC_AE_EnemyMove(int enemyId, Vector3 hexTile)
    {
        ActionEvents.EnemyMove?.Invoke(enemyId.GetEnemy(), hexTile.GetHex());
    }
}