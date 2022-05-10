using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "TFOSO", menuName = "Units/The Forge Master")]
public class TheForgeMaster : Unit
{
    int Turn = 0;
    int Phase = 0;
    string defDescription;
    public override void SetDefaults()
    {
        defDescription = description;
        Actions.Add(new InvokeAction());
        new UndyingResolve().AddPassive(this);
        Turn = 0;
        Phase = 0;
    }
    async private Task Speak(string monolog)
    {
        StringBuilder builder = new StringBuilder();
        foreach (char c in monolog)
        {
            builder.Append(c);
        }
        body.Manager.UpdateTextBox("<color=cyan>"+builder.ToString()+"</color>");
        while (!Input.GetMouseButtonDown(0))
        {
            await Task.Delay(2);
        }
    }
    async public override Task PreAI(UnitObject[] allies, UnitObject[] opponents)
    {
        await Speak(name);
        Turn++;
        if(HasPassive("Undying Resolve"))
        {
            if (Phase == 0 && !GetPassive("Undying Resolve").hidden)
            {
                Turn = 0;
                Phase++;
                await Speak("So, you've come to defeat me, have you? sorry, if I knew I had guests I would have prepared the place, but this option seems to be out of my reach for now, the architectural team has messed everything up... but at least I can recieve you properly. Also, press your mouse if you are done");
            }
        }
    }
}
