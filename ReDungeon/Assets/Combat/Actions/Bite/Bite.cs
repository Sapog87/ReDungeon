using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bite : Action
{
    public Bite() 
    {
        Name = "Bite";
        TargetType = TargetType.ONE;
        CDType = CDType.none;
        MaxUses = -1;
        MaxCooldown = -1;
        ResetCooldown();
    }
    public override List<Unit> GetValidTargets(Unit User)
    {
        return User.enemies.Where(x => !x.isDead).ToList();
    }

    public override void Invoke(Unit User, Unit target)
    {
        User.Attack(target, User, this, 20, false, 5);
        // TODO replace with proper recoil
        User.recoil += 10;
    }
}
