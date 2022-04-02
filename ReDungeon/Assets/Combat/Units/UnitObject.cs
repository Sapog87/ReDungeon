using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.UI;
using UnityEngine;
using System.Threading.Tasks;

public class UnitObject : MonoBehaviour
{
    public Unit unit;
    public int recoil;
    public List<Status> statuses = new List<Status>();
    public int defence;
    public float DR;
    public SpriteRenderer sprite;
    public Slider slider;
    public Text hpText;

    public void Setup(Unit unit)
    {
        this.unit = unit;
        unit.body = this;
        sprite.sprite = this.unit.sprites[6];
        UpdateSlider();
    }

    public void UpdateSlider()
    {
        slider.maxValue = unit.maxHP;
        slider.value = unit.CurrentHP;
        hpText.text = unit.CurrentHP.ToString();
    }

    async public Task approach(Transform position, float closeness, float speed)
    {
        Vector2 pos = transform.position;
        for (float i = 0; i <= closeness; i += speed)
        {
            transform.position = Vector2.Lerp(pos, position.position,i);
            await Task.Delay(10);
        }
    }
    async public Task goBack(float speed)
    {
        SetSprite(0);
        Vector2 pos = transform.localPosition;
        for (float i = 0; i <= 1; i += speed)
        {
            transform.localPosition = Vector2.Lerp(pos, Vector2.zero, i);
            await Task.Delay(10);
        }
    }

    public void SetSprite(int state)
    {
        sprite.sprite = unit.sprites[Mathf.Clamp(state, 0, 7)];
    }
}
