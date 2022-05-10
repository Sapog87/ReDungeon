using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TheSpear : Action
{
    Spear status;
    public TheSpear(Spear status)
    {
        SetDefaults();
        this.status = status;
    }
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        acter.unit.sprites[3] = Resources.Load<Sprite>("TheForgeMaster/TheForgeMasterPoleSpear");
        await acter.approach(target.transform, 0.95f, 0.1f);
        await acter.unit.Strike(target, 10, 15);
        acter.defence -= 5;
    }
    public override void SetDefaults()
    {
        recoil = 30;
        name = "The Spear";
        description = "Strikes one target for 10-15 base damage and reduces their defence by 5";
        returnspeed = 0.05f;
    }
    public override async Task PostAction(UnitObject acter)
    {
        status.stack--;
        if (status.stack <= 0)
            status.RemoveStatus(acter);
        await Task.Yield();
    }
    public override IEnumerable<UnitObject> GetPossibleTargets(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        return UnitObject.FilterAlive(opponents);
    }
    public override IEnumerable<UnitObject> GetTargets(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        return UnitObject.FillUnits(GetPossibleTargets(acter, allies, opponents), 1);
    }
}
