using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RatSO", menuName = "Units/Rat")]
public class Rat : Unit
{
    public override void SetDefaults()
    {
        Actions.Add(new Scratch());
    }

    public override Action Ai(UnitObject[] allies, UnitObject[] opponents)
    {
        return Actions[Actions.Count - 1];
    }
}
