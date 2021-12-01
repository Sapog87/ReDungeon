using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Unit
{
    private void Awake()
    {
        UnitHUD.UpdateHP(this);
        //actions.Add(new Lick());
    }
    public override void SetDefaults()
    {

    }
}
