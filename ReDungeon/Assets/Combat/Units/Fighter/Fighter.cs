using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : Unit
{
    void Awake()
    {
        init(this);
        actions.Add(new Strike());
        actions.Add(new StrongStrike());
        actions.Add(new HealSelf());
    }
    public override void SetDefaults()
    {

    }
}