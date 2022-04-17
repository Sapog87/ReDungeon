using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SimpleSpell : Action
{
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        await acter.unit.Strike(target, 5, 15);
        switch (Random.Range(0, 2))
        {
            case 0:
                target.unit.RecieveStatus(acter, new Acid(3));
                break;
            case 1:
                target.Recoil += 10;
                break;
        }
    }
    public override void SetDefaults()
    {
        recoil = 21;
        name = "Simple Spell";
        description = "Enchants one target for 7-17 base damage and applies either 2 stacks of Acid or 10 recoil";
    }

    public override UnitObject[] GetTargets(UnitObject acter, UnitObject[] allies, UnitObject[] opponents)
    {
        return new UnitObject[] { opponents[Random.Range(0, opponents.Length)] };
    }
}
