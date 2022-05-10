using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "TFOSO", menuName = "Units/The Forge Master")]
public class TheForgeMaster : Unit
{
    int Turn = 0;
    public override void SetDefaults()
    {
        Actions.Add(new SimpleSpell());
        Actions.Add(new HealingSpell());
        new Mercy().AddPassive(this);
    }
    async public override Task PreAI(UnitObject[] allies, UnitObject[] opponents)
    {
        Turn++;
    }
}
