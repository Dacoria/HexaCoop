using Photon.Pun;
using System.Linq;

public class PlayerScript : HexaEventCallback, IPunInstantiateMagicCallback, IUnit
{
    public bool IsAi;
    public string PlayerName;

    public UnitType UnitType => UnitType.Player;


    private int baseVisionRange = 1;

    public bool IsAlive => CurrentHP > 0;
    public int TurnCount => playerTurnCount?.TurnCount ?? 0;
    public int CurrentAP => playerActionPoints?.CurrentPlayerAP ?? 0;
    public int CurrentHP => playerHealth?.CurrentHitPoints ?? 0;

    public Hex CurrentHexTile { get; private set; }
    public int Id { get; private set; }

    public int Index;
    public void SetCurrentHexTile(Hex hex) => CurrentHexTile = hex;
    public void MoveToNewDestination(Hex tile) => gameObject.GetSet<UnitMovement>().GoToDestination(tile, 2f);
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
            Id = hosterCounterId + 1000; // forceert dat het anders is dat het photonId
        }
        else
        {
            Id = info.photonView.OwnerActorNr;
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
