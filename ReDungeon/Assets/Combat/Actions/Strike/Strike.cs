using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Strike: Action
{
    public Strike()
    {
        Name = "Strike";
        TargetType = TargetType.ONE;
        CDType = CDType.none;
        MaxUses = -1;
        MaxCooldown = -1;
        ResetCooldown();
    }
    public override List<Unit> GetValidTargets(Unit User)
    {
        return User.enemies.Where(x=>!x.isDead).ToList();
    }

    public override IEnumerator Invoke(Unit User, Unit target)
    {
        yield return User.Approach(target.transform.position, 1f);
        User.Attack(target, User, this, 20, false, 0);
        User.recoil += 10;
        yield return new WaitForSeconds(0.25f);
        yield return User.Approach(User.transform.parent.position, 1f);
    }
}
