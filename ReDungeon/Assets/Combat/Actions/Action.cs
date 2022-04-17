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
    public int recoil;

    public Action()
    {
        SetDefaults();
    }

    public abstract void SetDefaults();
    
    async public Task DoAction(UnitObject acter, UnitObject target)
    {
        acter.unit.OnAction.Invoke(acter, target, 0);
        target.unit.OnGetTargeted.Invoke(target, acter, 0);
        await Act(acter, target);
        await Task.Delay(1000);
    }

    public static UnitObject[] filterAlive(UnitObject[] units)
    {
        return units.Where(x => !x.unit.isDead).ToArray();
    }

    public abstract Task Act(UnitObject acter, UnitObject target);

    public abstract UnitObject[] GetTargets(UnitObject acter, UnitObject[] allies, UnitObject[] opponents);
}
