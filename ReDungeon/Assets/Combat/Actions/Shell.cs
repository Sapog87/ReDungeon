using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Shell : Action
{
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        acter.SetSprite(6);
        target.unit.RecieveStatus(acter, new DefenceBuff(3));
        await Task.Delay(100);
        acter.SetSprite(0);
    }
    public override void SetDefaults()
    {
        recoil = 25;
        name = "Shell";
        description = "Applies 3 stacks of defence buff";
        ImageReference = "Skill_Icons/Defender/Defender-Skill10";
    }

    public override async Task PostAction(UnitObject acter)
    {
        cooldown = 3;
        await Task.Yield();
    }

    public override IEnumerable<UnitObject> GetPossibleTargets(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        return new UnitObject[] { acter };
    }

    public override IEnumerable<UnitObject> GetTargets(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        return new UnitObject[] { acter };
    }
}
