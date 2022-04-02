using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Unit
{
    public override void SetDefaults()
    {
        Actions.Add(new SimpleLick());
    }

    public override Action Ai(UnitObject[] allies, UnitObject[] opponents)
    {
        return Actions[Actions.Count - 1];
    }
}
