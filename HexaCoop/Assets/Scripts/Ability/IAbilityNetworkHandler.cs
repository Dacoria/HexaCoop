using System;
using System.Collections.Generic;

public interface IAbilityNetworkHandler
{
    public bool CanDoAbility(PlayerScript playerDoingAbility, Hex target,Hex target2) => true;

    public List<Action> EventsTillAbilityIsFinished => new List<Action>();

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target, Hex target2);
}