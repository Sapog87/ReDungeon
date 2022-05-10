using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SimpleSplashI : Action
{
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        await acter.unit.Strike(target, 9, 20);
        switch (Random.Range(0, 5))
        {
            case 0:
                target.unit.RecieveStatus(acter, new Acid(2));
                break;
            default:
                target.Recoil += 15;
                break;
        }
    }
    public override void SetDefaults()
    {
        recoil = 21;
        name = "Simple Splash I";
        description = "Enchants one target for 9-19 base damage and applies 7 stacks of Acid or rarely 7 recoil";
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
