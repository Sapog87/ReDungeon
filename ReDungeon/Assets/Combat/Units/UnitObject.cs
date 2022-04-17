using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.UI;
using UnityEngine;
using System.Threading.Tasks;

public class UnitObject : MonoBehaviour
{
    public Unit unit;
    [SerializeField]
    private int _recoil;
    public int Recoil { get => _recoil; set { _recoil = Mathf.Max(value,0); UpdateRSlider(); } }
    public List<Status> statuses = new List<Status>();
    public int defence;
    public float DR;
    public SpriteRenderer sprite;
    public Slider HpSlider;
    public Slider SSlider;
    public Text hpText;
    public Text RecoilText;

    private void OnMouseDown()
    {
        
    }

    public void Setup(Unit unit)
    {
        this.unit = unit;
        unit.body = this;
        sprite.sprite = this.unit.sprites[6];
        UpdateSlider();
        UpdateRSlider();
    }

    public void UpdateSlider()
    {
        HpSlider.maxValue = unit.maxHP;
        HpSlider.value = unit.CurrentHP;
        hpText.text = unit.CurrentHP.ToString();
    }
    public void UpdateRSlider()
    {
        SSlider.value = Mathf.Max(0,100 - Recoil);
        RecoilText.text = Recoil.ToString();
    }

    async public Task approach(Transform position, float closeness, float speed)
    {
        SetSprite(2);
        Vector2 pos = transform.position;
        for (float i = 0; i <= closeness; i += speed)
        {
            transform.position = Vector2.Lerp(pos, position.position,i);
            await Task.Delay(10);
        }
    }
    async public Task goBack(float speed)
    {
        SetSprite(2);
        Vector2 pos = transform.localPosition;
        for (float i = 0; i <= 1; i += speed)
        {
            transform.localPosition = Vector2.Lerp(pos, Vector2.zero, i);
            await Task.Delay(10);
        }
        SetSprite(0);
    }

    public void SetSprite(int state)
    {
        sprite.sprite = unit.sprites[Mathf.Clamp(state, 0, 7)];
    }
}
