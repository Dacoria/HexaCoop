using System.Linq;
using UnityEngine;

public partial class NetwHandleBearTrapAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target)
    {
        var beartrapPrefab = Rsc.GoEnemiesOrObjMap.Single(x => x.Key == AbilityType.BearTrap.ToString()).Value;
        Vector3 destination = target.transform.position;
        var bearTrapGo = Instantiate(beartrapPrefab, destination + new Vector3(0,1,0), Quaternion.identity);
        bearTrapGo.GetComponent<BearTrapScript>().Init(playerDoingAbility, target);
    }    
}