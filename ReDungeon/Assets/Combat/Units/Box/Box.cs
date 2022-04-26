using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Unit
{
    public override void SetDefaults()
    {
        Actions.Add(new BoxRam());
        Actions.Add(new FlexTape());
    }

    public override Action Ai(UnitObject[] allies, UnitObject[] opponents)
    {
        return Actions[Actions.Count - 1];
    }
}
