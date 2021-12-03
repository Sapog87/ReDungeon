using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StrongStrike : Action
{
    public StrongStrike()
    {
        Name = "Strong Strike";
        TargetType = TargetType.MANY;
        CDType = CDType.turn;
        MaxUses = 1;
        MaxCooldown = 2;
        ResetCooldown();
    }

    public override List<Unit> GetValidTargets(Unit User)
    {
        return User.enemies.Where(x => !x.isDead).ToList();
    }

    public override void Invoke(Unit User, List<Unit> targets)
    {
        foreach (Unit target in targets)
        {
            Debug.Log(User.unitName);
            Debug.Log(target.unitName);
            Debug.Log(Name);
            User.Attack(target, User, this, 20, false, 10);
        }
        // TODO replace with proper recoil
        UsesLeft--;
        User.recoil += 20;
        if(UsesLeft == 0)
        {
            Cooldown = MaxCooldown;
        }
    }
}
