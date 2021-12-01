using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongStrike : Action
{
    public string name => "Strong Strike";
    public TargetType targetType => TargetType.team;
    public CDType CDType => CDType.turn;
    public int MaxUses { get; } = 1;
    public int MaxCooldown { get; } = 2;
    public int usesLeft { get; set; }
    public int cooldown { get; set; }

    public override Unit[] GetValidTargets(Unit User)
    {
        return User.enemies;
    }

    public override void invoke(Unit User, Unit[] targets)
    {
        foreach(Unit target in targets)
            Unit.Attack(target, User, this, 20, false, 10);
        // TODO replace with proper recoil
        User.recoil += 20;
    }
}
