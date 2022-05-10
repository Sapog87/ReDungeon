using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MageSO", menuName = "Units/Mage")]
public class Mage : Unit
{
    public override void SetDefaults()
    {
        Actions.Add(new SimpleSpell());
        Actions.Add(new HealingSpell());
        new Mercy().AddPassive(this);
    }

    protected override void AddLevel(int level)
    {
        switch (level)
        {
            case 1:
                for(int i = 0; i < Actions.Count; i++)
                {
                    if (Actions[i].name == "Healing Spell")
                    {
                        Actions[i] = new SelfSustaningHeal();
                        break;
                    }
                }
                break;
            case 2:
                for(int i = 0; i < Actions.Count;i++)
                {
                    if (Actions[i].name == "Simple Spell")
                    {
                        if (Random.Range(0, 2) == 0)
                            Actions[i] = new SimpleSparkI();
                        else
                            Actions[i] = new SimpleSplashI();
                        break;
                    }
                }
                break;
            case 3:
                maxHP += 10;
                break;
        }
        
    }

    protected override void RemoveLevel(int level)
    {
        switch (level)
        {
            case 1:
                for (int i = 0; i < Actions.Count; i++)
                {
                    if (Actions[i].name == "Self-Sustaining Healing")
                    {
                        Actions[i] = new HealingSpell();
                        break;
                    }
                }
                break;
            case 2:
                for (int i = 0; i < Actions.Count; i++)
                {
                    if (Actions[i].name == "Simple Spark I" || Actions[i].name == "Simple Splash I")
                    {
                        Actions[i] = new SimpleSpell();
                        break;
                    }
                }
                break;
            case 3:
                maxHP -= 10;
                break;
        }
    }
}
