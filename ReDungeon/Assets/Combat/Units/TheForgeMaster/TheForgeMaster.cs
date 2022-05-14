using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
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
        body.Manager.textbox.alignment = TMPro.TextAlignmentOptions.TopLeft;
        foreach (char c in monolog)
        {
            builder.Append(c);
            body.Manager.UpdateTextBox("<color=#38b5a2>" + builder.ToString() + "</color>");
            await Task.Delay(10);
        }
        while (!Input.GetMouseButtonDown(0))
        {
            await Task.Delay(2);
        }
        body.Manager.UpdateTextBox();
        body.Manager.textbox.alignment = TMPro.TextAlignmentOptions.Top;
    }
    public override Action Ai(UnitObject[] allies, UnitObject[] opponents)
    {
        if (Phase == 0)
            return new Wait();
        IEnumerable<Action> actions = Actions.Where(x => x.IsAvailable(body, allies, opponents));
        return actions.ToArray()[Random.Range(0,actions.Count())];
    }
    async public override Task PreAI(UnitObject[] allies, UnitObject[] opponents)
    {
        Turn++;
        if(HasPassive("Undying Resolve"))
        {
            if (Phase == 0 && !GetPassive("Undying Resolve").hidden)
            {
                Turn = 0;
                Phase++;
            }
        }
        if (Phase == 1)
        { 
            if (Turn == 1)
            {
                await Speak("Sorry, got lost in thoughts again. So, you've come to defeat me, have you? If I knew I had guests I would have prepared the place, but this option seems to be out of my reach for now, the architectural team has messed everything up... but at least I can recieve you properly myself. Press your mouse if you are done");
                body.sprite.sprite = Instantiate(Resources.Load<Sprite>("TheForgeMaster/TheForgeMasterSnap2"));
                await Task.Delay(800);
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("Snap");
                await Task.Delay(175);
                body.sprite.sprite = Instantiate(Resources.Load<Sprite>("TheForgeMaster/TheForgeMasterSnap1"));
                body.Manager.playerBasePositions[1].localPosition = new Vector3(-225,35,3);
                body.Manager.playerBasePositions[2].localPosition = new Vector3(-250, -60, 1);
                body.Manager.playerBasePositions[3].localPosition = new Vector3(-125, 30, 3);
                body.Manager.playerBasePositions[4].localPosition = new Vector3(-125, -55, 1);
                body.Manager.background.sprite = Instantiate(Resources.Load<Sprite>("TheForgeMaster/FinalBattle_Background"));
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().SmoothFadeAllTrackChange("FinalBattle_Phase2");
                await Task.Delay(1000);
            }
        }
    }
}
