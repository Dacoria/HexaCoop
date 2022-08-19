using Photon.Pun;
using System.Linq;

public class PlayerScript : HexaEventCallback, IPunInstantiateMagicCallback
{
    public Hex CurrentHexTile;
    public bool IsAi;
    public int PlayerId;
    public string PlayerName;

    private int baseVisionRange = 1;

    public bool PlayerIsAlive => CurrentHP > 0;
    public int TurnCount => playerTurnCount?.TurnCount ?? 0;
    public int CurrentAP => playerActionPoints?.CurrentPlayerAP ?? 0;
    public int CurrentHP => playerHealth?.CurrentHitPoints ?? 0;
    public int GetVision() => this.baseVisionRange + GetComponents<PlayerExtraVisionRangeScript>().Sum(x => x.AdditionalRange);


    [ComponentInject (SearchDirection=SearchDirection.CHILDREN)] public PlayerModel PlayerModel;

    [ComponentInject] private PlayerTurnCount playerTurnCount;
    [ComponentInject] private PlayerActionPoints playerActionPoints;
    [ComponentInject] private PlayerHealth playerHealth;
    
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        var name = instantiationData[0].ToString();
        IsAi = bool.Parse(instantiationData[1].ToString());

        var hosterCounterId = int.Parse(instantiationData[2].ToString());

        if (PhotonNetwork.OfflineMode || IsAi)
        {
            PlayerId = hosterCounterId + 1000; // forceert dat het anders is dat het photonId
        }
        else
        {
            PlayerId = info.photonView.OwnerActorNr;
        }
        if(IsAi && PhotonNetwork.IsMasterClient)
        {
            //gameObject.AddComponent<PlayerAi>();
            IsAi = false;
        }

        NetworkHelper.instance.RefreshPlayerGos();
        PlayerName = name;
        PlayerModel.gameObject.SetActive(false); // begin met onzichtbaar model
    }
}
