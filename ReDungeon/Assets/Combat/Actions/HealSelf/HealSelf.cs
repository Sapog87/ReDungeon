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
    public override List<Unit> GetValidTargets(Unit User)
    {
        return new List<Unit>{User}.Where(x=>x.currentHP/x.maxHP<0.75).ToList();
    }
    public override void Invoke(Unit User, Unit target)
    {
        User.Heal(User, target, 40);
        // TODO replace with proper recoil
        User.recoil += 10;
        UsesLeft--;
        User.recoil += 20;
        if (UsesLeft == 0)
        {
            Cooldown = MaxCooldown;
        }
    }
}
