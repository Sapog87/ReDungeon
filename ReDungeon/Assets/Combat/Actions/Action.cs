using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public abstract class Action
{
    public string name;
    public Sprite sprite;
    public string description;
    public float returnspeed = 0.1f;
    public int recoil;
    public int cooldown = 0;
    public string ImageReference;

    public Action()
    {
        SetDefaults();
    }
    public virtual bool IsAvailable(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        return (cooldown == 0) && (GetPossibleTargets(acter,allies,opponents).Count() > 0);
    }

    public abstract void SetDefaults();
    
    async public Task DoAction(UnitObject acter, UnitObject target)
    {
        acter.unit.OnAction.Invoke(acter, target);
        target.unit.OnGetTargeted.Invoke(target, acter);
        await Act(acter, target);
        await Task.Delay(20);
    }
    async public virtual Task PreAction(UnitObject acter) { }
    public abstract Task Act(UnitObject acter, UnitObject target);
    async public virtual Task PostAction(UnitObject acter) { }
    public abstract IEnumerable<UnitObject> GetPossibleTargets(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents);
    public abstract IEnumerable<UnitObject> GetTargets(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents);
}
