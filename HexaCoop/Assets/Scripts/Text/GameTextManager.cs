using System.Collections.Generic;
using UnityEngine;

public class GameTextManager : HexaEventCallback
{
    protected override void OnEnemyFaseStarted()
    {
        Textt.GameLocal("Enemy fase started");
    }

    protected override void OnPlayerAttackHit(PlayerScript player, Hex hexWithTargetHit, int damage)
    {
        var enemyOnHex = hexWithTargetHit.GetEnemy();
        var playerOnHex = hexWithTargetHit.GetPlayer();

        if (enemyOnHex != null)
        {
            if(enemyOnHex.GetComponent<EnemyHealth>().CurrentHitPoints > damage)
            {
                Textt.GameLocal(player.PlayerName + " hit enemy " + enemyOnHex.gameObject.name + " (1 dmg)");
            }
            else
            {
                Textt.GameLocal(player.PlayerName + " killed enemy " + enemyOnHex.gameObject.name);
            }
        }
        else if (playerOnHex != null)
        {
            if(playerOnHex.IsOnMyNetwork() || player.IsOnMyNetwork())
            {
                Textt.GameLocal(player.PlayerName + " attacks " + playerOnHex.PlayerName + " (1 dmg)");
            }

            if (playerOnHex.GetComponent<PlayerHealth>()?.CurrentHitPoints <= damage)
            {
                Textt.GameLocal(player.PlayerName + " killed another player! " + playerOnHex.gameObject.name);
            }            
        }
    }

    protected override void OnPlayerDamageObjectHitTile(PlayerScript playerOwner, Hex hex, DamageObjectType doType)
    {
        if (hex.HasUnit())
        {
            var playerOnHex = hex.GetPlayer();
            var enemyOnHex = hex.GetEnemy();

            if (playerOnHex != null)
            {
                if(playerOnHex.IsOnMyNetwork() || playerOwner.IsOnMyNetwork())
                {
                    Textt.GameLocal("Projectile hit " + hex.GetPlayer().PlayerName + "! (1 dmg)");
                }
            }
            else if (enemyOnHex != null)
            {
                Textt.GameLocal("Projectile hit " + enemyOnHex.gameObject.name + "! (1 dmg)");
            }
        }        
    }

    protected override void OnPlayerBeartrapHitPlayer(PlayerScript pOwnsTrap, Hex hex, PlayerScript playerHit)
    {
        if (playerHit.GetComponent<PlayerHealth>()?.CurrentHitPoints <= 1)
        {
            Textt.GameLocal(playerHit.PlayerName + " died by stepping on a beartrap. RIP");
        }
        else if (playerHit.IsOnMyNetwork()) // let op AI - daar wordt dit genegeerd
        {
            Textt.GameLocal("Beartrap hit " + playerHit.PlayerName + "!");
        }
        else if(pOwnsTrap.IsOnMyNetwork())
        {
            Textt.GameLocal("Beartrap of " + pOwnsTrap.PlayerName + " damaged " + playerHit.PlayerName + "!");
        }       
    }


    protected override void OnNewRoundStarted(List<PlayerScript> players, PlayerScript currentPlayer)
    {      
        if(GameHandler.instance.GameStatus != GameStatus.ActiveRound)
        {
            return;
        }

        if(Netw.MyPlayer() == currentPlayer)
        {
            Textt.GameLocal("New round started! Your Turn");
        }
        else
        {
            Textt.GameLocal("New round started! Turn: " + currentPlayer.PlayerName);
        }
    }

    protected override void OnEnemyAttack(EnemyScript enemy, PlayerScript player)
    {
        if (player.IsOnMyNetwork())
        {
            Textt.GameLocal("Enemy attacks " + player.PlayerName);
        }
    }

    protected override void OnEnemyMove(EnemyScript enemy, Hex tile)
    {
        //Textt.GameLocal("Enemy moves to new tile");
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        if (GameHandler.instance.GameStatus != GameStatus.ActiveRound)
        {
            return;
        }

        if (Netw.MyPlayer() == player)
        {
            Textt.GameLocal("New turn, your move!");
        }
        else
        {
            Textt.GameLocal("Turn: " + player.PlayerName);
        }
    }

    protected override void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType type)
    {
        if (GameHandler.instance.GameStatus != GameStatus.ActiveRound) { return; }
        
        else if (type == AbilityType.Vision)
        {
            Textt.GameLocal(player.PlayerName + " is watching a tile...");
        }
        else if (type == AbilityType.BearTrap)
        {
            Textt.GameLocal(player.PlayerName + " has placed a bear trap!");
        }        
        else if (type == AbilityType.Meditate)
        {
            Textt.GameLocal(player.PlayerName + " sacrifices his location for extra 2 AP next turn");
        }        
        else if (type == AbilityType.Binocular)
        {
            // verder niet moves updaten; moet lastig te zijn te bepalen zijn of je extra range hebt
            if (player.IsOnMyNetwork())
            {
                Textt.GameLocal(player.PlayerName + " has one extra vision till end of next turn");
            }
        }
    }
}
