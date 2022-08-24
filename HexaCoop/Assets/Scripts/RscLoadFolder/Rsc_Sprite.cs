using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static partial class Rsc
{
    private static List<string> spriteRscList = new List<string>
    {
        Statics.RESOURCE_PATH_SPRITE_ABILITY
    };

    private static Dictionary<string, Sprite> __spriteMap;
    public static Dictionary<string, Sprite> SpriteMap
    {
        get
        {
            if (__spriteMap == null)            
                __spriteMap = RscHelper.CreateSpriteDict(spriteRscList);            

            return __spriteMap;
        }
    }

    public static Sprite Get(this Dictionary<string, Sprite> dict, string key) => 
        dict.Single(x => key == x.Key).Value;
}