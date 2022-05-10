using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Brace : Action
{
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        acter.SetSprite(6);
        acter.unit.RecieveStatus(acter, new Counter(3));
        Debug.Log(target.unit.name);
        await Task.Delay(100);
        acter.SetSprite(0);
    }
    public override void SetDefaults()
    {
        recoil = 21;
        name = "Brace";
        description = "Applies 3 stacks of counter status on self";
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
