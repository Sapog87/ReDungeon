using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    private Image darker;

    private void Awake()
    {
        darker = GameObject.Find("Darker").GetComponent<Image>();
    }

    /// <summary>
    /// Darkens / Brightens screen background
    /// </summary>    
    private void DarkenScreenBackground(bool darken)
    {
        Color darkerColor = darker.color;
        if (darken)
            darkerColor.a = 0.5f;
        else
            darkerColor.a = 0;
        darker.color = darkerColor;
    }

    /// <summary>
    /// Opens skill tree and darkens background
    /// </summary>
    public void Open()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            DarkenScreenBackground(true);
        }
    }

    /// <summary>
    /// Closes skill tree and brightens background
    /// </summary>
    public void Close()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            DarkenScreenBackground(false);
        }
    }
}
