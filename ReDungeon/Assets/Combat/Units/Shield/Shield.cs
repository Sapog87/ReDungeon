using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldSO", menuName = "Units/Shield")]
public class Shield : Unit
{
    public override void SetDefaults()
    {
        Actions.Add(new SimpleStrike());
    }

    public override Action Ai(UnitObject[] allies, UnitObject[] opponents)
    {
        return Actions[Actions.Count - 1];
    }
}
