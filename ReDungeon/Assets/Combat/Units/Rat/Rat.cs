using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Unit
{
    private void Awake()
    {
        init(this);
        actions.Add(new Bite());
    }
    public override void SetDefaults()
    {

    }
}
