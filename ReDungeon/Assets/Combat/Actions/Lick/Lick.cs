using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lick : Action
{
    public Lick()
    {
        Name = "Lick";
        TargetType = TargetType.ONE;
        CDType = CDType.none;
        MaxUses = -1;
        MaxCooldown = 0;
        ResetCooldown();
    }
    public override List<Unit> GetValidTargets(Unit User)
    {
        return User.enemies.Where(x => !x.isDead).ToList();
    }

    public override IEnumerator Invoke(Unit User, Unit target)
    {
        yield return User.Approach(target.transform.position, 1f);
        bool success = User.Attack(target, User, this, 10, false, 5);
        if (success)
        {
            yield return new WaitForSeconds(0.5f);
            User.Heal(User, User, 10);
        }
        // TODO replace with proper recoil
        User.recoil += 10;
        yield return new WaitForSeconds(0.25f);
        yield return User.Approach(User.transform.parent.position, 1f);
    }
}
