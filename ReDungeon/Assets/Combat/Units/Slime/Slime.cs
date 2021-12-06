using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Unit
{
    private void Awake()
    {
        init(this);
        actions.Add(new Lick());
    }
    public override void SetDefaults()
    {

    }
}
