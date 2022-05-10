using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlimeSO", menuName = "Units/Slime")]
public class Slime : Unit
{
    public override void SetDefaults()
    {
        Actions.Add(new SimpleLick());
    }
}
