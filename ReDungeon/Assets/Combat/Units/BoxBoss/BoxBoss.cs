using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoxBossSO", menuName = "Units/BoxBoss")]
public class BoxBoss : Unit
{
    public override void SetDefaults()
    {
        Actions.Add(new BoxRam());
        Actions.Add(new FlexTape());
        Actions.Add(new BoxRage());
    }

    public override Action Ai(UnitObject[] allies, UnitObject[] opponents)
    {
        return Actions[Actions.Count - 1];
    }
}
