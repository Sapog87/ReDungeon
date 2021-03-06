using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class FlexTape : Action
{
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        acter.SetSprite(6);
        target.unit.GetHealed(target.unit.maxHP / 10 * 4);
        target.unit.RecieveStatus(acter, new DefenceBuff(2));
        await Task.Delay(100);
        acter.SetSprite(0);
    }
    public override void SetDefaults()
    {
        recoil = 0;
        name = "Flex Tape";
        description = "Heals for 40% of max health and applies 2 stacks of defence buff";
    }

    public override UnitObject[] GetTargets(UnitObject acter, UnitObject[] allies, UnitObject[] opponents)
    {
        return new UnitObject[] { acter };
    }
}

