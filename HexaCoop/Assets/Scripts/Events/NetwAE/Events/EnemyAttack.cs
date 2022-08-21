using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{    
    public void EnemyAttack(EnemyScript enemy, PlayerScript player)
    {
        photonView.RPC("RPC_AE_EnemyAttack", RpcTarget.All, enemy.Id, player.Id);
    }

    [PunRPC]
    public void RPC_AE_EnemyAttack(int enemyId, int pId)
    {
        ActionEvents.EnemyAttack?.Invoke(enemyId.GetEnemy(), pId.GetPlayer());
    }
}