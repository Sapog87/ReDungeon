using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Status
{
    public string name;
    public Sprite sprite;
    public string description;
    public HashSet<TriggerType> triggers = new HashSet<TriggerType>();
    public bool hidden;
    public void AddStatus(UnitObject target)
    {
        target.statuses.Add(this);
        foreach(TriggerType type in triggers)
        {
            switch (type)
            {
                case TriggerType.OnGetTargeted:
                    target.unit.OnGetTargeted += OnGetTargeted;
                    break;
                case TriggerType.PreAction:
                    target.unit.PreAction += PreAction;
                    break;
                case TriggerType.OnAction:
                    target.unit.OnAction += OnAction;
                    break;
                case TriggerType.PostAction:
                    target.unit.PostAction += PostAction;
                    break;
                case TriggerType.EarlyTakeHit:
                    target.unit.EarlyTakeHit += EarlyTakeHit;
                    break;
                case TriggerType.OnTakeHit:
                    target.unit.OnTakeHit += OnTakeHit;
                    break;
                case TriggerType.LateTakeHit:
                    target.unit.LateTakeHit += LateTakeHit;
                    break;
                case TriggerType.EarlyGetHurt:
                    target.unit.EarlyGetHurt += EarlyGetHurt;
                    break;
                case TriggerType.OnGetHurt:
                    target.unit.OnGetHurt += OnGetHurt;
                    break;
                case TriggerType.LateGetHurt:
                    target.unit.LateGetHurt += LateGetHurt;
                    break;
                case TriggerType.OnGetHealed:
                    target.unit.OnGetHealed += OnGetHealed;
                    break;
                case TriggerType.OnStrike:
                    target.unit.OnStrike += OnStrike;
                    break;
                case TriggerType.MOutDamage:
                    target.unit.MOutDamage += MOutDamage;
                    break;
                case TriggerType.OnKill:
                    target.unit.OnKill += OnKill;
                    break;
                case TriggerType.OnDeath:
                    target.unit.OnDeath += OnDeath;
                    break;
                case TriggerType.OnStatusApply:
                    target.unit.OnStatusApply += OnStatusApply;
                    break;
                case TriggerType.OnStatusRecieve:
                    target.unit.OnStatusRecieve += OnStatusRecieve;
                    break;
                case TriggerType.PreCombat:
                    target.unit.PreCombat += PreCombat;
                    break;
                case TriggerType.PostCombat:
                    target.unit.PostCombat += PostCombat;
                    break;
            }
        }
        OnSelfApply(target);
    }
    public void RemoveStatus(UnitObject target)
    {
        foreach (TriggerType type in triggers)
        {
            switch (type)
            {
                case TriggerType.OnGetTargeted:
                    target.unit.OnGetTargeted -= OnGetTargeted;
                    break;
                case TriggerType.PreAction:
                    target.unit.PreAction -= PreAction;
                    break;
                case TriggerType.OnAction:
                    target.unit.OnAction -= OnAction;
                    break;
                case TriggerType.PostAction:
                    target.unit.PostAction -= PostAction;
                    break;
                case TriggerType.EarlyTakeHit:
                    target.unit.EarlyTakeHit -= EarlyTakeHit;
                    break;
                case TriggerType.OnTakeHit:
                    target.unit.OnTakeHit -= OnTakeHit;
                    break;
                case TriggerType.LateTakeHit:
                    target.unit.LateTakeHit -= LateTakeHit;
                    break;
                case TriggerType.EarlyGetHurt:
                    target.unit.EarlyGetHurt -= EarlyGetHurt;
                    break;
                case TriggerType.OnGetHurt:
                    target.unit.OnGetHurt -= OnGetHurt;
                    break;
                case TriggerType.LateGetHurt:
                    target.unit.LateGetHurt -= LateGetHurt;
                    break;
                case TriggerType.OnGetHealed:
                    target.unit.OnGetHealed -= OnGetHealed;
                    break;
                case TriggerType.OnStrike:
                    target.unit.OnStrike -= OnStrike;
                    break;
                case TriggerType.MOutDamage:
                    target.unit.MOutDamage -= MOutDamage;
                    break;
                case TriggerType.OnKill:
                    target.unit.OnKill -= OnKill;
                    break;
                case TriggerType.OnDeath:
                    target.unit.OnDeath -= OnDeath;
                    break;
                case TriggerType.OnStatusApply:
                    target.unit.OnStatusApply -= OnStatusApply;
                    break;
                case TriggerType.OnStatusRecieve:
                    target.unit.OnStatusRecieve -= OnStatusRecieve;
                    break;
                case TriggerType.PreCombat:
                    target.unit.PreCombat -= PreCombat;
                    break;
                case TriggerType.PostCombat:
                    target.unit.PostCombat -= PostCombat;
                    break;
            }
            OnClear(target);
            target.statuses.Remove(this);
        }
    }
    public abstract void RenewStatus(Status self);
    public virtual void OnSelfApply(UnitObject bearer) { }
    public virtual void OnClear(UnitObject bearer) { }
    public virtual void OnGetTargeted(UnitObject bearer, UnitObject targeter) { }
    public virtual void PreAction(UnitObject bearer) { }
    public virtual void OnAction(UnitObject bearer, UnitObject target) { }
    public virtual void PostAction(UnitObject bearer) { }
    public virtual void EarlyTakeHit(UnitObject bearer, UnitObject attacker, ref int damage) { }
    public virtual void OnTakeHit(UnitObject bearer, UnitObject attacker, int damage) { }
    public virtual void LateTakeHit(UnitObject bearer, UnitObject attacker, ref int damage) { }
    public virtual void EarlyGetHurt(UnitObject bearer, ref int damage) { }
    public virtual void OnGetHurt(UnitObject bearer, ref int damage) { }
    public virtual void LateGetHurt(UnitObject bearer, ref int damage) { }
    public virtual void OnGetHealed(UnitObject bearer, ref int heal) { }
    public virtual void OnDeath(UnitObject bearer, ref int fatalDamage) { }
    public virtual void OnStrike(UnitObject bearer, UnitObject target, ref int damage) { }
    public virtual void MOutDamage(UnitObject bearer, ref int damage) { }
    public virtual void OnKill(UnitObject bearer, UnitObject target, ref int fataldamage) { }
    public virtual void OnStatusApply(UnitObject bearer, UnitObject target, Status status) { }
    public virtual void OnStatusRecieve(UnitObject bearer, UnitObject applier, Status status) { }
    public virtual void PreCombat(UnitObject bearer) { }
    public virtual void PostCombat(UnitObject bearer) { }
    public override string ToString()
    {
        return "<color=yellow>" + name + "</color>" + "\n" + description;
    }
}