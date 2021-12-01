using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CDType
{
    none, turn, fight, floor
}

public enum TargetType
{
    unit, team, all
}
public abstract class Action
{
    public string name { get; }
    public TargetType targetType { get; }
    public CDType CDType { get; }
    public int MaxUses { get; }
    public int MaxCooldown { get; }
    public int usesLeft { get; set; }
    public int cooldown { get; set; }
    public Action()
    {
        usesLeft = MaxUses;
        cooldown = 0;
    }
    public abstract Unit[] GetValidTargets(Unit User);
    public abstract void invoke(Unit User, Unit[] targets);
}
