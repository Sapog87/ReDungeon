using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;


public enum UnitType
{
    Human, Undead, Construct
}

public enum TriggerType
{
    OnGetTargeted, PreAction, OnAction, PostAction, EarlyTakeHit, OnTakeHit, LateTakeHit, EarlyGetHurt, OnGetHurt, LateGetHurt, OnGetHealed, OnStrike, MOutDamage, OnKill, OnDeath, OnStatusApply, OnStatusRecieve, PreCombat, PostCombat
}

public abstract class Unit:ScriptableObject
{
    [HideInInspector]
    public UnitObject body;

    public delegate void EmptyDelegate(UnitObject self);
    public delegate void TargetLessEmptyDelegate(UnitObject self);
    public delegate void TargetedEmptyDelegate(UnitObject self, UnitObject target);
    public delegate void TargetLessDelegate(UnitObject self, ref int number);
    public delegate void TargetedDelegate(UnitObject self, UnitObject target, ref int number);
    public delegate void TargetedSDelegate(UnitObject self, UnitObject target, int number);
    public delegate void StatusDelegate(UnitObject self, UnitObject target, Status status);
    public delegate void ModifyingTLDelegate(UnitObject self, ref int number);
    public delegate void ModifyingTDelegate(UnitObject self,ref UnitObject target, ref int number);

    public new string name;
    public string description;
    public int level = 0;

    public int maxHP;
    [SerializeField]
    private int _currentHP;
    public int CurrentHP { get => _currentHP; set { _currentHP = Mathf.Clamp(value, 0, maxHP); } }
    [HideInInspector]
    public bool isDead;
    /// <summary>
    /// Use this field only if you want to change Defence permanently for the charecter
    /// </summary>
    [field: SerializeField]
    public int defence;
    /// <summary>
    /// Use this field only if you want to change Damage Reduction permanently for the charecter
    /// </summary>
    [field: SerializeField]
    public float DR;

    public HashSet<UnitType> UnitTypes = new HashSet<UnitType>();
    public List<Action> Actions = new List<Action>();
    public List<Passive> Passives = new List<Passive>();
    /// <summary>
    ///  0 - idle, 1 - taking damage, 2 - movement 3-4 - attacking, 5 - downed, 6 - special, 7 - victory
    /// </summary>
    public Sprite[] sprites = new Sprite[8];

    public TargetedEmptyDelegate OnGetTargeted = delegate { };
    public TargetLessEmptyDelegate PreAction = delegate { };
    public TargetedEmptyDelegate OnAction = delegate { };
    public TargetLessEmptyDelegate PostAction = delegate { };
    public TargetedDelegate EarlyTakeHit = delegate { };
    public TargetedSDelegate OnTakeHit = delegate { };
    public TargetedDelegate LateTakeHit = delegate { };
    public TargetLessDelegate EarlyGetHurt = delegate { };
    public TargetLessDelegate OnGetHurt = delegate { };
    public TargetLessDelegate LateGetHurt = delegate { };
    public TargetLessDelegate OnGetHealed = delegate { };
    public TargetedDelegate OnStrike = delegate { };
    public ModifyingTLDelegate MOutDamage = delegate { };
    public TargetedDelegate OnKill = delegate { };
    public TargetLessDelegate OnDeath = delegate { };
    public StatusDelegate OnStatusApply = delegate { };
    public StatusDelegate OnStatusRecieve = delegate { };
    public EmptyDelegate PreCombat = delegate { };
    public EmptyDelegate PostCombat = delegate { };

    public abstract void SetDefaults();
    async public virtual Task PreAI(UnitObject[] allies, UnitObject[] opponents) { await Task.Yield(); }
    async public virtual Task PostAI(UnitObject[] allies, UnitObject[] opponents) { await Task.Yield(); }
    public virtual Action Ai(UnitObject[] allies, UnitObject[] opponents)
    {
        for (int i = Actions.Count - 1; i >= 0; i--)
            if (Actions[i].IsAvailable(body, allies, opponents))
                return Actions[i];
        return new Wait();
    }

    public static Unit Create(Unit unit, int level = 0)
    {
        Unit result = Instantiate(unit);
        result.SetDefaults();
        result.Setlevel(level);
        return result;
    }
    public Unit()
    {
    }
    public void Setlevel(int level)
    {
        if (this.level < level)
            for (int i = this.level + 1; i <= level; i++)
                AddLevel(i);
        else if (this.level > level)
            for (int i = this.level; i > level; i--)
                RemoveLevel(i);
        this.level = level;
    }

    protected virtual void AddLevel(int level){ }

    protected virtual void RemoveLevel(int level) { }

    public async Task TakeHit(UnitObject attacker, int damage)
    {
        body.SetSprite(1);
        try
        {
            EarlyTakeHit.Invoke(body, attacker, ref damage);
            int trueDamage = Mathf.Max(0, Mathf.RoundToInt((damage - defence - body.defence) / (1 - (DR + body.DR))));
            OnTakeHit.Invoke(body, attacker, trueDamage);
            LateTakeHit.Invoke(body, attacker, ref trueDamage);
            GetHurt(trueDamage);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
        await Task.Delay(500);
        body.SetSprite(0);
    }

    public void GetHurt(int damage)
    {
        EarlyGetHurt.Invoke(body, ref damage);
        OnGetHurt.Invoke(body, ref damage);
        LateGetHurt.Invoke(body,ref damage);
        CurrentHP -= damage;
        if (CurrentHP == 0)
            Die(damage);
        body.UpdateSlider();
    }

    public void GetHealed(int heal)
    {
        OnGetHealed.Invoke(body,ref heal);
        CurrentHP += heal;
        isDead = false;
        body.UpdateSlider();
    }
    public async Task Strike(UnitObject target, int mindamage, int maxdamage)
    {
        body.SetSprite(3);
        int damage = Random.Range(mindamage, maxdamage);
        MOutDamage.Invoke(body, ref damage);
        OnStrike.Invoke(body, target, ref damage);
        await target.unit.TakeHit(body, damage);
        body.SetSprite(0);
    }

    public void Die(int Fataldamage)
    {
        OnDeath.Invoke(body,ref Fataldamage);
        isDead = true;
    }

    public void RecieveStatus(UnitObject applier, Status status)
    {
        if (applier != null)
            applier.unit.OnStatusApply.Invoke(applier, body, status);
        OnStatusRecieve.Invoke(body, applier, status);
        for (int i = 0; i < body.statuses.Count; i++)
        {
                if (body.statuses[i].name == status.name)
                {
                    body.statuses[i].RenewStatus(status);
                    return;
                }
        }
        status.AddStatus(body);
    }

    public bool HasPassive(string name)
    {
        foreach (Passive status in Passives)
            if (status.name == name)
                return true;
        return false;
    }

    public Passive GetPassive(string name)
    {
        foreach (Passive status in Passives)
            if (status.name == name)
                return status;
        return null;
    }
    public bool HasStatus(string name)
    {
        foreach(Status status in body.statuses)
            if (status.name == name)
                return true;
        return false;
    }

    public Status GetStatus(string name)
    {
        foreach(Status status in body.statuses)
            if(status.name == name)
                return status;
        return null;
    }
    public bool HasAction(string name)
    {
        foreach (Action status in Actions)
            if (status.name == name)
                return true;
        return false;
    }

    public Action GetAction(string name)
    {
        foreach (Action status in Actions)
            if (status.name == name)
                return status;
        return null;
    }
}
