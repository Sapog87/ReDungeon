using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InvokeAction : Action
{
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        acter.unit.RecieveStatus(acter, new Glaive(1));
        acter.unit.RecieveStatus(acter, new Sword(1));
        acter.unit.RecieveStatus(acter, new Spear(1));
    }
    public override void SetDefaults()
    {
        recoil = 0;
        name = "Invoke";
        description = "Creates one of many weapons...";
        returnspeed = 0.05f;
    }
    public override async Task PostAction(UnitObject acter)
    {
        cooldown = 2;
        await Task.Yield();
    }
    public override IEnumerable<UnitObject> GetPossibleTargets(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        return new UnitObject[] {acter};
    }
    public override IEnumerable<UnitObject> GetTargets(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        return new UnitObject[] { acter };
    }
}
