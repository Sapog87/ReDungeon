using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public CharacterUI_Box[] UI;

    public static Dictionary<string, CharacterUI_Box> UI_boxes;
    public static MainPlayerCombat player;

    private void Awake()
    {
        UI_boxes = new Dictionary<string, CharacterUI_Box>();
        UI_boxes.Add("Mage", UI[0]);
        UI_boxes.Add("Fighter", UI[1]);
        UI_boxes.Add("Shield", UI[2]);
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
        foreach(Unit unit in player._playerUnits)
        {
            if (UI_boxes.ContainsKey(unit.name))
            {
                UI_boxes[unit.name].HpSlider.maxValue = unit.maxHP;
                UI_boxes[unit.name].HpSlider.value = unit.CurrentHP;
                UI_boxes[unit.name].HpText.text = $"{unit.CurrentHP}/{unit.maxHP}";
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


