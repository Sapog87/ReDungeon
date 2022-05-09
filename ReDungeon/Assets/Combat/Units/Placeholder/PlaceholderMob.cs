using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlaceholderMobSO", menuName = "Units/PlaceholderMob")]
public class PlaceholderMob : Unit
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
