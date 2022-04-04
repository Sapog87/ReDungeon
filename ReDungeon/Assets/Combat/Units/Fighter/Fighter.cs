using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FighterSO", menuName = "Units/Fighter")]
public class Fighter : Unit
{
    public override void SetDefaults()
    {
        Actions.Add(new SimpleStrike());
        new BlindingGlory().AddPassive(this);
    }

    public override Action Ai(UnitObject[] allies, UnitObject[] opponents)
    {
        return Actions[Actions.Count - 1];
    }
}
