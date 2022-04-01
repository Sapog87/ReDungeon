using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public enum UnitType
{
    Human, Undead, Construct
}

public enum TriggerType
{
    OnGetTargeted, OnAction, OnTakeHit, OnGetHurt, OnGetHealed, OnStrike, MOutDamage, OnKill, OnDeath, OnStatusApply, OnStatusRecieve
}
[CreateAssetMenu (fileName = "New Unit", menuName = "Unit")]

public abstract class Unit:ScriptableObject
{
    public UnitObject body;

    public delegate void TargetLessDelegate(UnitObject self, int number);
    public delegate void TargetedDelegate(UnitObject self, UnitObject target, int number);
    public delegate void StatusDelegate(UnitObject self, UnitObject target, Status status);
    public delegate void ModifyingTLDelegate(UnitObject self, ref int number);
    public delegate void ModifyingTDelegate(UnitObject self,ref UnitObject target, ref int number);

    public new string name;
    public string description;

    public int maxHP;
    [SerializeField]
    private int _currentHP;
    public int CurrentHP { get => _currentHP; set { _currentHP = Mathf.Clamp(value, 0, maxHP); } }
    public bool isDead;

    public int defence;
    public float DR;

    public HashSet<UnitType> UnitTypes = new HashSet<UnitType>();
    public List<Action> Actions = new List<Action>();
    public List<Passive> Passives = new List<Passive>();

    // 0 - idle, 1 - taking damage, 2-4 - attacking, 5 - downed, 6 - special, 7 - victory
    public Sprite[] sprites = new Sprite[8];

    public TargetedDelegate OnGetTargeted = delegate { };
    public TargetedDelegate OnAction = delegate { };
    public TargetedDelegate OnTakeHit = delegate { };
    public TargetLessDelegate OnGetHurt = delegate { };
    public TargetLessDelegate OnGetHealed = delegate { };
    public TargetedDelegate OnStrike = delegate { };
    public ModifyingTLDelegate MOutDamage = delegate { };
    public TargetedDelegate OnKill = delegate { };
    public TargetLessDelegate OnDeath = delegate { };
    public StatusDelegate OnStatusApply = delegate { };
    public StatusDelegate OnStatusRecieve = delegate { };

    public abstract void SetDefaults();

    public virtual Action Ai(UnitObject[] allies, UnitObject[] opponents)
    {
        return new Wait();
    }

    public Unit()
    {
        SetDefaults();
    }

    public void TakeHit(UnitObject attacker, int damage)
    {
        try
        {
            OnTakeHit.Invoke(body, attacker, damage);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
        GetHurt(damage);
    }

    public void GetHurt(int damage)
    {
        OnGetHurt.Invoke(body, damage);
        CurrentHP -= damage;
        Debug.Log($"{name} {damage}");
        if (CurrentHP == 0)
            Die(damage);
        body.UpdateSlider();
    }

    public void GetHealed(int heal)
    {
        OnGetHealed.Invoke(body, heal);
        CurrentHP += heal;
        isDead = false;
        body.UpdateSlider();
    }
    public void Strike(UnitObject target, int mindamage, int maxdamage)
    {
        int damage = Random.Range(mindamage, maxdamage);
        MOutDamage.Invoke(body, ref damage);
        OnStrike.Invoke(body, target, damage);
        target.unit.TakeHit(body, damage);
    }

    public void Die(int Fataldamage)
    {
        OnDeath.Invoke(body, Fataldamage);
        isDead = true;
    }

    public void RecieveStatus(UnitObject applier, Status status)
    {
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
}
