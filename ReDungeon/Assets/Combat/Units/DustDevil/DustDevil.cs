using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DustDevilSO", menuName = "Units/DustDevil")]
public class DustDevil : Unit
{
    public override void SetDefaults()
    {
        Actions.Add(new Sandstorm());
    }

    public override Action Ai(UnitObject[] allies, UnitObject[] opponents)
    {
        return Actions[Actions.Count - 1];
    }
}

