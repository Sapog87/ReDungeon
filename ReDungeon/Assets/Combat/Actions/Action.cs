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
    public virtual bool IsReady(Unit User)
    {
        return (GetValidTargets(User).Count > 0) && (UsesLeft != 0);
    }
    public abstract List<Unit> GetValidTargets(Unit User);
    public virtual IEnumerator Invoke(Unit User, List<Unit> targets) { yield return null; }
    public virtual IEnumerator Invoke(Unit User, Unit target) { yield return null; }
}
