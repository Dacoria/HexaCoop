
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class StaticHelper
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static float xOffset = 2f, yOffset = 1f, zOffset = 1.73f;
    public static Vector3Int ConvertPositionToCoordinates(this Vector3 position)
    {
        var x = Mathf.CeilToInt (position.x / xOffset);
        var y = Mathf.RoundToInt(position.y / yOffset);
        var z = Mathf.RoundToInt(position.z / zOffset);

        return new Vector3Int(x, y, z);
    }

    public static Vector3 ConvertCoordinatesToPosition(this Vector3Int position)
    {
        if(position.z % 2 == 0)
        {
            var x = position.x * xOffset;
            var y = position.y * yOffset;
            var z = position.z * zOffset;
            return new Vector3(x, y, z);
        }
        else
        {
            var x = position.x * xOffset - 1;
            var y = position.y * yOffset;
            var z = position.z * zOffset;
            return new Vector3(x, y, z);
        }      
    }

    public static bool In<T>(this T val, params T[] values) where T : struct => values.Contains(val);
    public static PlayerScript GetPlayer(this int id) => NetworkHelper.instance.GetAllPlayers().FirstOrDefault(p => p.Id == id);    
    public static EnemyScript GetEnemy(this int id) => ObjectNetworkInitManager.instance.SpawnedNetworkObjects[id].GetComponent<EnemyScript>();
    public static Hex GetHex(this Vector3Int coordinates) => HexGrid.instance.GetTileAt(coordinates); 
    public static Hex GetHex(this Vector3 coordinates) => HexGrid.instance.GetTileAt(new Vector3Int((int)coordinates.x, (int)coordinates.y, (int)coordinates.z));

    public static bool IsEmpty(this Vector2 vector) => vector.x == 0 && vector.y == 0;
    public static Color SetAlpha(this Color color, float newAlpha) => new Color(color.r, color.g, color.b, newAlpha);

    public static Vector3 ToVector3(this string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        var result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }

    public static T GetAdd<T>(this GameObject go) where T : MonoBehaviour
    {
        var component = go.GetComponent<T>() ?? go.AddComponent<T>();
        return component;
    }    
}