
using System;
using UnityEngine;

public class FallingDamageObjectScript : MonoBehaviour
{
    public DamageObjectType DamageObjectType;
    public float Speed = 4;

    private Vector3 endposition;
    public GameObject Explosion;
    public PlayerScript Player;
    public Hex HexTarget;

    private void Start()
    {
        endposition = transform.position + new Vector3(0, -100, 0);
    }
   
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endposition, Speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var explosion = Instantiate(Explosion, collision.transform.position, Quaternion.identity);
        PlayerObjectHitTile(Player, HexTarget);
        Destroy(gameObject);
    }

    private void PlayerObjectHitTile(PlayerScript playerWhoShotRocket, Hex hexTileHit)
    {
        if(!Netw.IsMyNetwTurn())
        {
            // logica bij curr player
            return;
        }

        var allPlayers = NetworkHelper.instance.GetAllPlayers();
        PlayerScript playerHit = null;

        foreach (var player in allPlayers)
        {
            if (player.CurrentHexTile == hexTileHit)
            {                
                playerHit = player;
                break;
            }
        }

        NetworkAE.instance.PlayerDamageObjectHitTile(playerWhoShotRocket, hexTileHit, DamageObjectType);       
    }
}
