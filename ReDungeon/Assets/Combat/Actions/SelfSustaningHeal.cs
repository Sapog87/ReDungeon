using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SelfSustaningHeal : Action
{
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        acter.SetSprite(6);
        target.unit.GetHealed(Random.Range(10, 26));
        Debug.Log(target.unit.name);
        await Task.Delay(100);
        acter.SetSprite(0);
    }

    public override async Task PostAction(UnitObject acter)
    {
        acter.unit.GetHealed(Random.Range(5, 11));
        await Task.Yield();
    }
    public override void SetDefaults()
    {
        recoil = 21;
        name = "Self-Sustaining Healing";
        description = "Heals 1 target for 10-25 hp and oneself for 5-10, can also target oneself directly.";
    }

    public override IEnumerable<UnitObject> GetPossibleTargets(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        return allies;
    }

    public override IEnumerable<UnitObject> GetTargets(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        return UnitObject.FillUnits(GetPossibleTargets(acter, allies, opponents), 1);
    }
}
