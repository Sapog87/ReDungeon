using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class HealingSpell : Action
{
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        acter.SetSprite(6);
        target.unit.GetHealed(Random.Range(20, 31));
        Debug.Log(target.unit.name);
        await Task.Delay(100);
        acter.SetSprite(0);
    }
    public override void SetDefaults()
    {
        recoil = 21;
        name = "Healing Spell";
        description = "Heals either 1 target for 20-30 hp or splits heals all with a low chance";
    }

    public override UnitObject[] GetTargets(UnitObject acter, UnitObject[] allies, UnitObject[] opponents)
    {
        switch (Random.Range(0, 5))
        {
            case 0:
                return new UnitObject[] { acter };
            case 1:
                return new UnitObject[] { allies[Random.Range(0, allies.Length)] };
            case 2:
                return new UnitObject[] { allies[Random.Range(0, allies.Length)] };
            case 3:
                return new UnitObject[] { allies[Random.Range(0, allies.Length)] };
            case 4:
                return allies;
            default:
                return allies;
        }
    }
}
