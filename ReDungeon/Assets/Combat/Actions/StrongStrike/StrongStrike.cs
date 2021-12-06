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
        MaxCooldown = 3;
        ResetCooldown();
    }

    public override List<Unit> GetValidTargets(Unit User)
    {
        return User.enemies.Where(x => !x.isDead).ToList();
    }

    public override IEnumerator Invoke(Unit User, List<Unit> targets)
    {
        foreach (Unit target in targets)
        {
            if(User.Attack(target, User, this, 20, false, 10))
            target.recoil += 5;
            yield return new WaitForSeconds(0.1f);
        }
        // TODO replace with proper recoil
        UsesLeft--;
        User.recoil += 10;
        if (UsesLeft == 0)
        {
            Cooldown = MaxCooldown;
        }
        yield return new WaitForSeconds(1f);
        yield return User.Approach(User.transform.parent.position, 1f);
    }
}
