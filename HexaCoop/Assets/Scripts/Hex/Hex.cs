using System.Linq;
using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

[SelectionBase]
public class Hex : HexaEventCallback
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

    private HexStructureType initHexStructureType;
    private HexSurfaceType initHexSurfaceType;
    private HexObjectOnTileType initHexObjectOnTileType;

    public HexStartPlayerType HexStartPlayer;

    new void Awake()
    {
        base.Awake();
        this.hexSurfaceScript = gameObject.AddComponent<HexSurfaceScript>();
        this.hexStructureScript = gameObject.AddComponent<HexStructureScript>();

        initHexStructureType = HexStructureType;
        initHexSurfaceType = HexSurfaceType;
        initHexObjectOnTileType = HexObjectOnTileType;
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
        var player = this.GetPlayer(isAlive: true);
        if (player != null && player.IsAlive)
        {
            player.PlayerModel.gameObject.SetActive(!fogEnabled);
        }

        ChangeHexSurfaceType(Settings.ShowSurfacesInFog || !fogEnabled ? HexSurfaceType : HexSurfaceType.Simple_Plain, alsoChangeType: false);
        ChangeHexStructureType(Settings.ShowStructuresInFog || !fogEnabled ? HexStructureType : HexStructureType.None, alsoChangeType: false);
    }

    public bool IsObstacle() => HexSurfaceType.IsObstacle() || HexStructureType.IsObstacle();
    public void ChangeHexSurfaceType(HexSurfaceType changeToType, bool alsoChangeType = true)
    {        
        hexSurfaceScript.HexSurfaceTypeChanged(changeToType);

        if (alsoChangeType)
        {
            HexSurfaceType = changeToType;
        }
    }

    public void DestroyStructure()
    {
        if(HexStructureType == HexStructureType.Mountain)
        {
            var structureGo = Utils.GetStructureGoFromHex(this);
            if (structureGo != null)
            {
                var mountainScript = structureGo.GetComponentInChildren<MountainScript>();
                mountainScript.Destroy();
            }
        }
        else
        {
            throw new Exception("Alleen mountain vernietigen wordt nog ondersteund!");
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
            // alleen hiden/showen van model
            var structureGo = Utils.GetStructureGoFromHex(this);
            if (structureGo != null)
            {
                var structureGoModel = structureGo.GetComponentInChildren<StructureModel>(true)?.gameObject;
                structureGoModel?.SetActive(changeToType != HexStructureType.None);
            }
        }
    }   

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {
        if(HexStructureType != initHexStructureType)
        {
            ChangeHexStructureType(initHexStructureType);
        }
        if (HexSurfaceType != initHexSurfaceType)
        {
            ChangeHexSurfaceType(initHexSurfaceType);
        }

        var structureGo = Utils.GetChildGoByName(gameObject, "Props");
        if (initHexObjectOnTileType.IsPickup() && Utils.GetChildGoByName(structureGo, initHexObjectOnTileType.ToString()) == null)      
        {
            var pickupPrefab = Rsc.GoEnemiesOrObjMap.First(x => x.Key == initHexObjectOnTileType.ToString()).Value;
            var pickupGo = Instantiate(pickupPrefab, structureGo.transform);
            pickupGo.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }
}
