using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlimeSO", menuName = "Units/Slime")]
public class Slime : Unit
{
    public override void SetDefaults()
    {
        Actions.Add(new PureLick());
    }

    protected override void AddLevel(int level)
    {
        switch (level)
        {
            case 2:
                for (int i = 0; i < Actions.Count; i++)
                {
                    if (Actions[i].name == "Purified Lick")
                    {
                        Actions[i] = new SimpleLick();
                        break;
                    }
                }
                break;
        }
    }

    protected override void RemoveLevel(int level)
    {
        switch (level)
        {
            case 2:
                for (int i = 0; i < Actions.Count; i++)
                {
                    if (Actions[i].name == "Simple Lick")
                    {
                        Actions[i] = new PureLick();
                        break;
                    }
                }
                break;
        }
    }
}
