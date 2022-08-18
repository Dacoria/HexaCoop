using Photon.Pun;
using UnityEngine;

public partial class NetworkActionEvents : MonoBehaviour
{    
    public void EnemyAttack(EnemyScript enemy, PlayerScript player)
    {
        photonView.RPC("RPC_AE_EnemyAttack", RpcTarget.All, enemy.Id, player.PlayerId);
    }

    [PunRPC]
    public void RPC_AE_EnemyAttack(int enemyId, int pId)
    {
        ActionEvents.EnemyAttack?.Invoke(enemyId.GetEnemy(), pId.GetPlayer());
    }
}