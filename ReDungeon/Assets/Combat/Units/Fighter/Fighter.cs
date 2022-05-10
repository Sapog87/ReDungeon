using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FighterSO", menuName = "Units/Fighter")]
public class Fighter : Unit
{
    public override void SetDefaults()
    {
        Actions.Add(new SimpleStrike());
        Actions.Add(new Multihit());
        Actions.Add(new Brace());
        new BlindingGlory().AddPassive(this);
    }

    protected override void AddLevel(int level)
    {
        switch (level)
        {
            case 1:
                maxHP += 25;
                CurrentHP += 25;
            break;
            case 2:
                for (int i = 0; i < Actions.Count; i++)
                {
                    if (Actions[i].name == "MultiStrike")
                    {
                        if (Random.Range(0, 2) == 0)
                            Actions[i] = new Growinghit();
                        else
                            Actions[i] = new Initialhit();
                        break;
                    }
                }
                break;
            case 3:
                for (int i = 0; i < Actions.Count; i++)
                {
                    if (Actions[i].name == "Simple Strike")
                    {
                        if (Random.Range(0, 2) == 0)
                            Actions[i] = new HeavyStrike();
                        else
                            Actions[i] = new Strike();
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
            case 1:
                maxHP -= 25;
                CurrentHP -= 25;
            break;
            case 2:
                for (int i = 0; i < Actions.Count; i++)
                {
                    if (Actions[i].name == "Growing Strike" || Actions[i].name == "Initial Strike")
                    {
                        Actions[i] = new Multihit();
                        break;
                    }
                }
                break;
            case 3:
                for (int i = 0; i < Actions.Count; i++)
                {
                    if (Actions[i].name == "Strike" || Actions[i].name == "Heavy Strike")
                    {
                        Actions[i] = new SimpleStrike();
                        break;
                    }
                }
                break;
        }
    }
}
