using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHUDScript: MonoBehaviour
{
    public Slider hpSlider;
    public Text Max;
    public Text Current;

    public void UpdateHP(Unit unit)
    {
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        Max.text = unit.maxHP.ToString();
        Current.text = unit.currentHP.ToString();
    }
}
