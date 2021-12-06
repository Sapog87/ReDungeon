using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealSelf : Action
{
    public HealSelf()
    {
        Name = "Mediocre Healing";
        TargetType = TargetType.ONE;
        CDType = CDType.fight;
        MaxUses = 3;
        MaxCooldown = 1;
        ResetCooldown();
    }

    public override bool IsReady(Unit User)
    {
        return base.IsReady(User) && ((float)User.currentHP / User.maxHP) < 0.5;
    }
    public override List<Unit> GetValidTargets(Unit User)
    {
        return new List<Unit> { User };
    }
    public override IEnumerator Invoke(Unit User, Unit target)
    {
        User.Heal(User, target, 40);
        // TODO replace with proper recoil
        User.recoil += 7;
        UsesLeft--;
        if (UsesLeft == 0)
        {
            Cooldown = MaxCooldown;
        }
        yield return new WaitForSeconds(1);
    }
}
