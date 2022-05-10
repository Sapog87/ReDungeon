using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Multihit : Action
{
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        await acter.approach(target.transform, 0.95f, 0.1f);
        await acter.unit.Strike(target, 5, 10);
    }
    public override void SetDefaults()
    {
        recoil = 20;
        name = "MultiStrike";
        description = "Strikes up to three targets for 5-10 base damage each";
        returnspeed = 0.05f;
    }
    public override async Task PostAction(UnitObject acter)
    {
        cooldown = 3;
        await Task.Yield();
    }
    public override IEnumerable<UnitObject> GetPossibleTargets(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        return UnitObject.FilterAlive(opponents);
    }
    public override IEnumerable<UnitObject> GetTargets(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        return UnitObject.FillUnits(GetPossibleTargets(acter, allies, opponents), 3);
    }
}
