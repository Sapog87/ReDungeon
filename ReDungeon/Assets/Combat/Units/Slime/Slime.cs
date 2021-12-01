using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Unit
{
    private void Awake()
    {
        UnitHUD.UpdateHP(this);
        //actions.Add(new Bite());
    }
    public override void SetDefaults()
    {

    }
}
