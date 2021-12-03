using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CDType
{
    none, turn, fight, floor
}

public enum TargetType
{
    ONE, MANY
}
public abstract class Action
{
    public string Name;
    public TargetType TargetType;
    public CDType CDType;
    public int MaxUses;
    public int MaxCooldown;
    public int UsesLeft;
    public int Cooldown;
    public void ResetCooldown() 
    {
        Debug.Log(Name);
        UsesLeft = MaxUses;
        Cooldown = 0;
    }
    public abstract List<Unit> GetValidTargets(Unit User);
    public virtual void Invoke(Unit User, List<Unit> targets) {}
    public virtual void Invoke(Unit User, Unit target) {}
}
