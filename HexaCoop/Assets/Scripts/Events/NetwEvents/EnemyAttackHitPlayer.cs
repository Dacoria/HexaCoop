using Photon.Pun;
using UnityEngine;

public partial class NetworkActionEvents : MonoBehaviour
{    
    public void EnemyAttackHit(EnemyScript enemy, Hex hexTile, int damage)
    {
        photonView.RPC("RPC_AE_EnemyAttackHit", RpcTarget.All, enemy.Id, (Vector3)hexTile.HexCoordinates, damage);
    }

    [PunRPC]
    public void RPC_AE_EnemyAttackHit(int enemyId, Vector3 hextile, int damage)
    {
        ActionEvents.EnemyAttackHit?.Invoke(enemyId.GetEnemy(), hextile.GetHex(), damage);
    }
}