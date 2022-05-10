using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
    public CharacterUI_Box[] UI;
    public CharacterInfo_Box[] Info;

    public static Dictionary<string, CharacterUI_Box> UI_boxes;
    public static Dictionary<string, CharacterInfo_Box> Info_boxes;
    public static MainPlayerCombat player;



    private void Awake()
    {
        UI_boxes = new Dictionary<string, CharacterUI_Box>();
        UI_boxes.Add("Mage", UI[0]);
        UI_boxes.Add("Fighter", UI[1]);
        UI_boxes.Add("Shield", UI[2]);

        Info_boxes = new Dictionary<string, CharacterInfo_Box>();
        Info_boxes.Add("Mage", Info[0]);
        Info_boxes.Add("Fighter", Info[1]);
        Info_boxes.Add("Shield", Info[2]);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MainPlayerCombat>();
        RefreshUI();
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        RefreshUI();
    }

    static void RefreshUI()
    {
        int i = 0;
        foreach(Unit unit in player._playerUnits)
        {
            if (UI_boxes.ContainsKey(unit.name))
            {
                UI_boxes[unit.name].HpSlider.maxValue = unit.maxHP;
                UI_boxes[unit.name].HpSlider.value = unit.CurrentHP;
                UI_boxes[unit.name].HpText.text = $"{unit.CurrentHP}/{unit.maxHP}";

                UI_boxes[unit.name].ExpSlider.maxValue = player.UnitLvXp[i];
                UI_boxes[unit.name].ExpSlider.value = player.UnitXp[i];
                UI_boxes[unit.name].ExpText.text = $"{player.UnitXp[i]}/{player.UnitLvXp[i]}";
                i++;
            }
            if (Info_boxes.ContainsKey(unit.name))
            {
                StringBuilder sb = new StringBuilder();
                Info_boxes[unit.name].Description.text = $"{unit.name}\nLevel:{unit.level}\n{unit.description}";

                if (unit.Passives.Where(x => !x.hidden).Count() > 0)
                {
                    sb.Append("\n");
                    sb.Append($"Passive Effects:");
                    foreach (Passive passive in unit.Passives)
                    {
                        if (!passive.hidden)
                            sb.Append("\n" + passive.ToString());
                    }
                    sb.Append("\n");
                }

                if (unit.Actions.Where(x => x.name != "Wait").Count() > 0)
                {
                    sb.Append("\n");
                    sb.Append($"Actions:");
                    foreach (Action action in unit.Actions)
                    {
                        if (action.name != "Wait")
                            sb.Append("\n<color=red>" + action.name + "</color>\n" + action.description+ "\n");
                    }
                    sb.Append("\n");
                }

                Info_boxes[unit.name].Skills.text = sb.ToString();
            }
        }
        }

    }


[System.Serializable]
public class CharacterUI_Box
{
    public Slider HpSlider;
    public Slider ExpSlider;
    public TextMeshProUGUI HpText;
    public TextMeshProUGUI ExpText;

    public CharacterUI_Box(Slider hpSlider, TextMeshProUGUI hpText, Slider expSlider, TextMeshProUGUI expText)
    {
        HpSlider = hpSlider;
        HpText = hpText;
        ExpSlider = expSlider;
        ExpText = expText;
    }
}

[System.Serializable]
public class CharacterInfo_Box
{
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Skills;

    public CharacterInfo_Box(TextMeshProUGUI description, TextMeshProUGUI skills)
    {
        Description = description;
        Skills = skills;
    }
}
