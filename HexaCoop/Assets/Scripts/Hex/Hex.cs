using System.Linq;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class Hex : MonoBehaviour
{
    [ComponentInject] private HexCoordinates hexCoordinates;
    [ComponentInject] private HighlightHexScript highlightMove;

    private FogOnHex fogOnHex;

    public HighlightColorType? GetHighlight() => highlightMove.CurrentColorHighlight;

    public Vector3Int HexCoordinates => hexCoordinates.OffSetCoordinates;

    public HexStructureType HexStructureType;
    public HexSurfaceType HexSurfaceType;

    public HexObjectOnTileType HexObjectOnTileType; // voor debug purposes
    public Vector3 OrigPosition;

    private HexSurfaceScript hexSurfaceScript;
    private HexStructureScript hexStructureScript;

    public bool FogIsActive() => fogOnHex?.FogIsActive() == true;

    void Awake()
    {
        this.hexSurfaceScript = gameObject.AddComponent<HexSurfaceScript>();
        this.hexStructureScript = gameObject.AddComponent<HexStructureScript>();

        this.ComponentInject();
        OrigPosition = this.transform.position;
    }

    private IEnumerator Start()
    {
        while(fogOnHex == null)
        {
            yield return Wait4Seconds.Get(0.2f);
            fogOnHex = GetComponentInChildren<FogOnHex>();
        }
    }


    public void EnableHighlight(HighlightColorType type) => highlightMove.CurrentColorHighlight = type;
    public void DisableHighlight() => highlightMove.CurrentColorHighlight = HighlightColorType.None;
    public void DisableHighlight(HighlightColorType type)
    {
        if(highlightMove.CurrentColorHighlight == type)
        {
            highlightMove.CurrentColorHighlight = HighlightColorType.None;
        }
    }

    public void SetFogOnHex(bool fogEnabled)
    {
        fogOnHex.SetFog(fogEnabled);
        if(GetPlayerOnHex() != null)
        {
            GetPlayerOnHex().PlayerModel.gameObject.SetActive(!fogEnabled);
        }
    }   
   
    public bool IsObstacle() => HexSurfaceType.IsObstacle() || HexStructureType.IsObstacle();

    public bool HasUnitOnHex() => GetUnitOnHex() != null;
    public IUnit GetUnitOnHex() => GetPlayerOnHex() != null ? GetPlayerOnHex() : GetEnemyOnHex();

    public EnemyScript GetEnemyOnHex()
    {
        var enemies = EnemyManager.instance.GetEnemies();
        return enemies.FirstOrDefault(x => x.CurrentHexTile.HexCoordinates == HexCoordinates);
    }

    public PlayerScript GetPlayerOnHex()
    {
        var players = NetworkHelper.instance.GetAllPlayers(isAlive: true);
        return players.FirstOrDefault(x => x.CurrentHexTile.HexCoordinates == HexCoordinates);
    }

    public void ChangeHexSurfaceType(HexSurfaceType changeToType, bool alsoChangeType = true)
    {        
        hexSurfaceScript.HexSurfaceTypeChanged(changeToType);

        if (alsoChangeType)
        {
            HexSurfaceType = changeToType;
        }
    }

    public void ChangeHexStructureType(HexStructureType changeToType, bool alsoChangeType = true)
    {
        if (alsoChangeType)
        {
            // daadwekelijk verwijderen
            hexStructureScript.HexStructureTypeChanged(changeToType);
            HexStructureType = changeToType;
        }
        else
        {
            // alleen hiden/showen
            var structureGoModel = Utils.GetStructureGoFromHex(this)?.GetComponentInChildren<StructureModel>(true)?.gameObject;
            structureGoModel?.SetActive(changeToType != HexStructureType.None);
        }
    }
}
