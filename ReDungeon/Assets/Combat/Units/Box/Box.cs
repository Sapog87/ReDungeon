using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoxSO", menuName = "Units/Box")]
public class Box : Unit
{
    public override void SetDefaults()
    {
        Actions.Add(new BoxRam());
        Actions.Add(new FlexTape());
    }

    public override Action Ai(UnitObject[] allies, UnitObject[] opponents)
    {
        return Actions[0];
    }
}
