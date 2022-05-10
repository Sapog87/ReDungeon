using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TheGlaive : Action
{
    Glaive status;
    public TheGlaive(Glaive status)
    {
        SetDefaults();
        this.status = status;
    }
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        await acter.approach(target.transform, 0.95f, 0.1f);
        await acter.unit.Strike(target, 25, 30);
    }
    public override void SetDefaults()
    {
        recoil = 10;
        name = "The Glaive";
        description = "Strikes up to three targets for 5-10 base damage each";
        returnspeed = 0.05f;
    }
    public override async Task PostAction(UnitObject acter)
    {
        status.stack--;
        if (status.stack == 0)
            status.RemoveStatus(acter);
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
