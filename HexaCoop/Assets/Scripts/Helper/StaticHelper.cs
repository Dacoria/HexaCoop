
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public static class StaticHelper
{
    private static System.Random rng = new System.Random();

    public static bool IsWideScreen => Screen.height * 1.2f < Screen.width;
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
        var x = Mathf.CeilToInt(position.x / xOffset);
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

    public static bool In<T>(this T val, params T[] values) where T : struct
    {
        return values.Contains(val);
    }

    public static PlayerScript GetPlayer(this int id)
    {
        return NetworkHelper.instance.GetAllPlayers().FirstOrDefault(p => p.PlayerId == id);
    }

    public static EnemyScript GetEnemy(this int id)
    {
        return ObjectNetworkInit.instance.SpawnedNetworkObjects[id].GetComponent<EnemyScript>();
    }

    public static Hex GetHex(this Vector3Int coordinates)
    {
        return HexGrid.instance.GetTileAt(coordinates);
    }

    public static Hex GetHex(this Vector3 coordinates)
    {
        return HexGrid.instance.GetTileAt(new Vector3Int((int)coordinates.x, (int)coordinates.y, (int)coordinates.z));
    }

    public static int GetPunOwnerActorNr(this GameObject go)
    {
        var photonView = go.GetComponent<PhotonView>();
        if (photonView != null)
        {
            return photonView.OwnerActorNr;
        }
        return -1;
    }

    public static bool IsOnEdgeOfGrid(this Vector3Int vector) => HexGrid.instance.IsOnEdgeOfGrid(vector);
    public static bool IsOnCornerOfGrid(this Vector3Int vector) => HexGrid.instance.IsOnCornerOfGrid(vector);

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

}