using Photon.Pun;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static partial class Rsc
{
    private static List<string> goEnemiesOrObjList = new List<string>
    {
        Statics.RESOURCE_PATH_GO_ENEMIES,
        Statics.RESOURCE_PATH_GO_OBJ_ON_TILE,
        Statics.RESOURCE_PATH_GO_PLAYER_UTIL,
    };

    private static Dictionary<string, GameObject> __goEnemiesOrObjMap;
    public static Dictionary<string, GameObject> GoEnemiesOrObjMap
    {
        get
        {
            if (__goEnemiesOrObjMap == null)
            {
                __goEnemiesOrObjMap = RscHelper.CreateGoDict(goEnemiesOrObjList);
                if (Application.isPlaying)
                {
                    var pool = PhotonNetwork.PrefabPool as DefaultPool;
                    pool.ResourceCache.AddRange(__goEnemiesOrObjMap);
                }
            }

            return __goEnemiesOrObjMap;
        }
    }

    private static List<string> goTileStructureList = new List<string>        
    { 
        Statics.RESOURCE_PATH_GO_TILE_STRUCTURES,
    };    

    private static Dictionary<string, GameObject> __goStructureMap;
    public static Dictionary<string, GameObject> GoStructureMap
    {
        get
        {
            if (__goStructureMap == null)
            {
                __goStructureMap = RscHelper.CreateGoDict(goTileStructureList);
            }
            
            return __goStructureMap;
        }
    }

    private static List<string> goGuiList = new List<string>
    {
        Statics.RESOURCE_PATH_GO_GUI
    };

    private static Dictionary<string, GameObject> __goGuiMap;
    public static Dictionary<string, GameObject> GoGuiMap
    {
        get
        {
            if (__goGuiMap == null)
            {
                __goGuiMap = RscHelper.CreateGoDict(goGuiList);                
            }

            return __goGuiMap;
        }
    }
}