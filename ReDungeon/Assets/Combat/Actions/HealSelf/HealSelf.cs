using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSelf : Action
{
    public string name => "Mediocre Healing";
    public TargetType targetType => TargetType.unit;
    public CDType CDType => CDType.fight;
    public int MaxUses { get; } = 3;
    public int MaxCooldown { get; } = 1;
    public int usesLeft { get; set; }
    public int cooldown { get; set; }
    public override Unit[] GetValidTargets(Unit User)
    {
        return new Unit[]{User};
    }
    public override void invoke(Unit User, Unit[] targets)
    {
        Unit.Heal(User, targets[0], 40);
        // TODO replace with proper recoil
        User.recoil += 10;
    }
}
