using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class HeavyStrike : Action
{
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        await acter.approach(target.transform, 0.95f, 0.1f);
        await acter.unit.Strike(target, 45, 60);
    }
    public override void SetDefaults()
    {
        recoil = 25;
        name = "Heavy Strike";
        description = "Strikes one target for 45-60 base damage, but becomes unavailable for the next turn";
        returnspeed = 0.05f;
        ImageReference = "Skill_Icons/Warrior/Warrior-Skill19";
    }

    public override async Task PostAction(UnitObject acter)
    {
        cooldown = 2;
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
