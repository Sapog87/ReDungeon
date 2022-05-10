using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.UI;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using TMPro;

public class UnitObject : MonoBehaviour
{
    [HideInInspector]
    public Unit unit;
    [HideInInspector]
    public GameObject pointer;
    public IEnumerable<UnitObject> Allies;
    public IEnumerable<UnitObject> Opponents;
    [HideInInspector]
    public BattleManager Manager;
    private int _recoil;
    [HideInInspector]
    public int Recoil { get => _recoil; set { _recoil = Mathf.Max(value,0); UpdateRSlider(); } }
    public List<Status> statuses = new List<Status>();
    [HideInInspector]
    public int defence;
    [HideInInspector]
    public float DR;
    public SpriteRenderer sprite;
    public Slider HpSlider;
    public Slider SSlider;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI RecoilText;

    private void OnMouseUpAsButton()
    {
        if (Manager.PossibleTargets.Contains(this))
        {
            if (Manager.state == BattleState.TargetSelection)
            {
                if (Manager.targetedUnits.Contains(this))
                {
                    pointer.GetComponent<PointerScript>().SetState(false);
                    Manager.targetedUnits.Remove(this);
                    Manager.UpdateTextBox($"Remaining targets: {Manager.maxU - Manager.targetedUnits.Count}");
                }
                else
                {
                    pointer.GetComponent<PointerScript>().SetState(true);
                    Manager.targetedUnits.Add(this);
                    Manager.UpdateTextBox($"Remaining targets: {Manager.maxU - Manager.targetedUnits.Count}");
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        if (Manager.state == BattleState.ActionSelection)
            Manager.UpdateTextBox(this.ToString());
    }

    private void OnMouseExit()
    {
        if (Manager.state == BattleState.ActionSelection)
            Manager.UpdateTextBox();
    }

    public void Setup(Unit unit, IEnumerable<UnitObject> allies, IEnumerable<UnitObject> opponents, BattleManager manager)
    {
        this.unit = unit;
        unit.body = this;
        Allies = allies;
        Opponents = opponents;
        sprite.sprite = this.unit.sprites[6];
        Manager = manager;
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
    public static UnitObject[] FillUnits(IEnumerable<UnitObject> possibleTargets, int count)
    {
        List<UnitObject> unitsP = possibleTargets.ToList();
        UnitObject[] units = new UnitObject[Mathf.Min(count,unitsP.Count())];
        for (int i = 0; i < units.Length;i++)
        {
            units[i] = unitsP[Random.Range(0, unitsP.Count)];
            unitsP.Remove(units[i]);
        }
        return units;
    }
    public static IEnumerable<UnitObject> FilterAlive(IEnumerable<UnitObject> units)
    {
        return units.Where(x => !x.unit.isDead);
    }
    public static IEnumerable<UnitObject> FilterDead(IEnumerable<UnitObject> units)
    {
        return units.Where(x => x.unit.isDead);
    }
    public static IEnumerable<UnitObject> FilterByType(IEnumerable<UnitObject> units, UnitType type, bool BlackList = true)
    {
        return units.Where(x => BlackList ^ x.unit.UnitTypes.Contains(type));
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"{unit.name}\nLevel:<color=yellow>{unit.level+1}</color>\n{unit.CurrentHP}/{unit.maxHP} HP");
        if (unit.UnitTypes.Count > 0)
        {
            sb.Append("Types: ");
            foreach(UnitType type in unit.UnitTypes)
            {
                sb.Append(type + ", ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.AppendLine();
        }
        sb.AppendLine(unit.description);

        if (unit.Passives.Where(x=>!x.hidden).Count() > 0)
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

        if (statuses.Where(x => !x.hidden).Count() > 0)
        {
            sb.Append("\n");
            sb.Append($"Status Effects:");
            foreach(Status status in statuses)
            {
                if (!status.hidden)
                sb.Append("\n" + status.ToString());
            }
            sb.Append("\n");
        }
        return sb.ToString();
    }
}
