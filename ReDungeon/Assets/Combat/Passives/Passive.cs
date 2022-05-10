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
    public bool hidden;
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
                case TriggerType.PreAction:
                    target.PreAction += PreAction;
                    break;
                case TriggerType.OnAction:
                    target.OnAction += OnAction;
                    break;
                case TriggerType.PostAction:
                    target.PostAction += PostAction;
                    break;
                case TriggerType.EarlyTakeHit:
                    target.EarlyTakeHit += EarlyTakeHit;
                    break;
                case TriggerType.OnTakeHit:
                    target.OnTakeHit += OnTakeHit;
                    break;
                case TriggerType.LateTakeHit:
                    target.LateTakeHit += LateTakeHit;
                    break;
                case TriggerType.EarlyGetHurt:
                    target.EarlyGetHurt += EarlyGetHurt;
                    break;
                case TriggerType.OnGetHurt:
                    target.OnGetHurt += OnGetHurt;
                    break;
                case TriggerType.LateGetHurt:
                    target.LateGetHurt += LateGetHurt;
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
                case TriggerType.PreCombat:
                    target.PreCombat += PreCombat;
                    break;
                case TriggerType.PostCombat:
                    target.PostCombat += PostCombat;
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
                case TriggerType.PreAction:
                    target.PreAction -= PreAction;
                    break;
                case TriggerType.OnAction:
                    target.OnAction -= OnAction;
                    break;
                case TriggerType.PostAction:
                    target.PostAction -= PostAction;
                    break;
                case TriggerType.EarlyTakeHit:
                    target.EarlyTakeHit -= EarlyTakeHit;
                    break;
                case TriggerType.OnTakeHit:
                    target.OnTakeHit -= OnTakeHit;
                    break;
                case TriggerType.LateTakeHit:
                    target.LateTakeHit -= LateTakeHit;
                    break;
                case TriggerType.EarlyGetHurt:
                    target.EarlyGetHurt -= EarlyGetHurt;
                    break;
                case TriggerType.OnGetHurt:
                    target.OnGetHurt -= OnGetHurt;
                    break;
                case TriggerType.LateGetHurt:
                    target.LateGetHurt -= LateGetHurt;
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
                case TriggerType.PreCombat:
                    target.PreCombat -= PreCombat;
                    break;
                case TriggerType.PostCombat:
                    target.PostCombat -= PostCombat;
                    break;
            }
            OnClear(target);
            target.Passives.Remove(this);
        }
    }
    public virtual void OnSelfApply(Unit bearer) { }
    public virtual void OnClear(Unit bearer) { }
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
