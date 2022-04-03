using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MageSO", menuName = "Units/Mage")]
public class Mage : Unit
{
    public override void SetDefaults()
    {
        Actions.Add(new SimpleSpell());
    }

    public override Action Ai(UnitObject[] allies, UnitObject[] opponents)
    {
        return Actions[Actions.Count - 1];
    }
}
