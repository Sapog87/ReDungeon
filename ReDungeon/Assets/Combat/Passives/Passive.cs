using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Passive
{
    public string name;
    public Sprite sprite;
    public string description;
    public HashSet<TriggerType> triggers = new HashSet<TriggerType>();
    public void AddPassive(Unit target)
    {
        target.Passives.Add(this);
        foreach (TriggerType type in triggers)
        {
            switch (type)
            {
                case TriggerType.OnGetTargeted:
                    target.OnGetTargeted += OnGetTargeted;
                    break;
                case TriggerType.OnAction:
                    target.OnAction += OnAction;
                    break;
                case TriggerType.OnTakeHit:
                    target.OnTakeHit += OnTakeHit;
                    break;
                case TriggerType.OnGetHurt:
                    target.OnGetHurt += OnGetHurt;
                    break;
                case TriggerType.OnGetHealed:
                    target.OnGetHealed += OnGetHealed;
                    break;
                case TriggerType.OnStrike:
                    target.OnStrike += OnStrike;
                    break;
                case TriggerType.MOutDamage:
                    target.MOutDamage += MOutDamage;
                    break;
                case TriggerType.OnKill:
                    target.OnKill += OnKill;
                    break;
                case TriggerType.OnDeath:
                    target.OnDeath += OnDeath;
                    break;
                case TriggerType.OnStatusApply:
                    target.OnStatusApply += OnStatusApply;
                    break;
                case TriggerType.OnStatusRecieve:
                    target.OnStatusRecieve += OnStatusRecieve;
                    break;
            }
        }
        OnSelfApply(target);
    }
    public void RemovePassive(Unit target)
    {
        foreach (TriggerType type in triggers)
        {
            switch (type)
            {
                case TriggerType.OnGetTargeted:
                    target.OnGetTargeted -= OnGetTargeted;
                    break;
                case TriggerType.OnAction:
                    target.OnAction -= OnAction;
                    break;
                case TriggerType.OnTakeHit:
                    target.OnTakeHit -= OnTakeHit;
                    break;
                case TriggerType.OnGetHurt:
                    target.OnGetHurt -= OnGetHurt;
                    break;
                case TriggerType.OnGetHealed:
                    target.OnGetHealed -= OnGetHealed;
                    break;
                case TriggerType.OnStrike:
                    target.OnStrike -= OnStrike;
                    break;
                case TriggerType.MOutDamage:
                    target.MOutDamage -= MOutDamage;
                    break;
                case TriggerType.OnKill:
                    target.OnKill -= OnKill;
                    break;
                case TriggerType.OnDeath:
                    target.OnDeath -= OnDeath;
                    break;
                case TriggerType.OnStatusApply:
                    target.OnStatusApply -= OnStatusApply;
                    break;
                case TriggerType.OnStatusRecieve:
                    target.OnStatusRecieve -= OnStatusRecieve;
                    break;
            }
            OnClear(target);
            target.Passives.Remove(this);
        }
    }
    public virtual void OnSelfApply(Unit bearer) { }
    public virtual void OnClear(Unit bearer) { }
    public virtual void OnGetTargeted(UnitObject bearer, UnitObject targeter, int number) { }
    public virtual void OnAction(UnitObject bearer, UnitObject target, int number) { }
    public virtual void OnTakeHit(UnitObject bearer, UnitObject attacker, int damage) { }
    public virtual void OnGetHurt(UnitObject bearer, int damage) { }
    public virtual void OnGetHealed(UnitObject bearer, int heal) { }
    public virtual void OnDeath(UnitObject bearer, int fatalDamage) { }
    public virtual void OnStrike(UnitObject bearer, UnitObject target, int damage) { }
    public virtual void MOutDamage(UnitObject bearer, ref int damage) { }
    public virtual void OnKill(UnitObject bearer, UnitObject target, int fataldamage) { }
    public virtual void OnStatusApply(UnitObject bearer, UnitObject target, Status status) { }
    public virtual void OnStatusRecieve(UnitObject bearer, UnitObject applier, Status status) { }
}
