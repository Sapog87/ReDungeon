using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lick : Action
{
    public string name => "Lick";
    public TargetType targetType => TargetType.unit;
    public CDType CDType => CDType.none;
    public int MaxUses { get; } = -1;
    public int MaxCooldown { get; } = -1;
    public int usesLeft { get; set; }
    public int cooldown { get; set; }
    public override Unit[] GetValidTargets(Unit User)
    {
        return User.enemies;
    }

    public override void invoke(Unit User, Unit[] targets)
    {
        bool success = Unit.Attack(targets[0], User, this, 10, false, 5);
        if (success)
            Unit.Heal(User, User, 5);
        // TODO replace with proper recoil
        User.recoil += 10;
    }
}
