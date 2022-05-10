using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlaceholderMobSO", menuName = "Units/PlaceholderMob")]
public class PlaceholderMob : Unit
{
    public override void SetDefaults()
    {
        Actions.Add(new SimpleLick());
        Actions.Add(new Sandstorm());
        Actions.Add(new FlexTape());
        Actions.Add(new Scratch());
        Actions.Add(new BoxRam());
    }

    public override Action Ai(UnitObject[] allies, UnitObject[] opponents)
    {
        return Actions[Random.Range(0, Actions.Count)];
    }
}
