using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class HealingSpell : Action
{
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        acter.SetSprite(6);
        target.unit.GetHealed(Random.Range(10, 21));
        Debug.Log(target.unit.name);
        await Task.Delay(100);
        acter.SetSprite(0);
    }
    public override void SetDefaults()
    {
        recoil = 21;
        name = "Healing Spell";
        description = "Heals 1 target for 10-20 hp";
        ImageReference = "Skill_Icons/Mage/Mage-Skill11";
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
