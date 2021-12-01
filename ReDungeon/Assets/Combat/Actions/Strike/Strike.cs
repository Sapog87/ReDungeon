using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike: Action
{
    public string name => "Strike";
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
        Unit.Attack(targets[0], User, this, 20, false, 0);
        // TODO replace with proper recoil
        User.recoil += 10;
    }
}
