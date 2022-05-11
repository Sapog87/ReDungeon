using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class TheSword2 : Action
{
    Sword status;
    bool tick;
    public TheSword2(Sword status)
    {
        SetDefaults();
        this.status = status;
    }
    async public override Task Act(UnitObject acter, UnitObject target)
    {
        await acter.approach(target.transform, 0.95f, 0.1f);
        if (tick)
        acter.unit.sprites[3] = Resources.Load<Sprite>("TheForgeMaster/TheForgeMasterSword1");
        else
        acter.unit.sprites[3] = Resources.Load<Sprite>("TheForgeMaster/TheForgeMasterSword2");
        await acter.unit.Strike(target, 25, 31);
        if (tick)
            acter.unit.sprites[3] = Resources.Load<Sprite>("TheForgeMaster/TheForgeMasterSword1");
        else
            acter.unit.sprites[3] = Resources.Load<Sprite>("TheForgeMaster/TheForgeMasterSword2");
        await acter.unit.Strike(target, 25, 31);
        if (tick)
            acter.unit.sprites[3] = Resources.Load<Sprite>("TheForgeMaster/TheForgeMasterSword1");
        else
            acter.unit.sprites[3] = Resources.Load<Sprite>("TheForgeMaster/TheForgeMasterSword2");
        await acter.unit.Strike(target, 5, 11);
    }
    public override void SetDefaults()
    {
        recoil = 30;
        name = "The Sword2";
        description = "Strikes one target for 5-10 base damage 3 times";
        returnspeed = 0.05f;
    }
    public override async Task PostAction(UnitObject acter)
    {
        status.stack--;
        if (status.stack <= 0)
            status.RemoveStatus(acter);
        await Task.Yield();
    }
    public override IEnumerable<UnitObject> GetPossibleTargets(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        return UnitObject.FilterAlive(opponents);
    }
    public override IEnumerable<UnitObject> GetTargets(UnitObject acter, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents)
    {
        return UnitObject.FillUnits(GetPossibleTargets(acter, allies, opponents), 1);
    }
}
