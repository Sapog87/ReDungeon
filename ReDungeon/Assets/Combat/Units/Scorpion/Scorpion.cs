using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScorpionSO", menuName = "Units/Scorpion")]
public class Scorpion : Unit
{
    public override void SetDefaults()
    {
        Actions.Add(new SimpleLick());
    }

    public override Action Ai(UnitObject[] allies, UnitObject[] opponents)
    {
        return Actions[0];
    }
}
