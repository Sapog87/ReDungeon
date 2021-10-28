using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class will never get an instance, it's used for easier creation, compatability and possibly (not approved, so don't get your hopes up) modding


public abstract class Unit : MonoBehaviour
{
    public string unitName;
    public int maxHP;
    public int currentHP;
    public int attackBase;
    public int attackBonusAdd;
    public float attackBonusMult;
    public int defenceBase;
    public int defenceBonusAdd;
    public float damageReduction;
    public int recoil;
    public List<Action> actions; 

    delegate void onHitHandler(Unit sender, Unit attacker, int DamageBase);
    delegate void onAttackHandler(Unit sender, Unit target, int DamageBase);
    delegate void preTurnHandler(Unit sender);
    delegate void postTurnHandler(Unit sender);

    public Unit()
    {
        SetDefaults();
    }

    public virtual void SetDefaults() { }
    public virtual void PreTurn () { }
    public virtual void PostTurn() { }
    public void Hurt(int Damage)
    {
        int dealtDamage = Mathf.Max(0,Mathf.RoundToInt(Damage / damageReduction - defenceBase - defenceBonusAdd));
        currentHP = Mathf.Max(0,currentHP - dealtDamage);
        if (currentHP == 0)
        {
            Die(); 
        }
    }
    public void Die()
    {

    }
    public virtual void ModifyHitByUnit(Action action, Unit attacker, ref int damage, ref bool crit) { }
    public virtual void OnHitByUnit(Action action, Unit attacker, int damage, bool crit) 
    { 
        ModifyHitByUnit(action, attacker, ref damage, ref crit);
    }
    public virtual void ModifyHitUnit(Action action, Unit Victim, ref int damage, ref bool crit) { }
    public virtual void OnHitUnit(Action action, Unit Victim, ref int damage, ref bool crit) 
    {
        ModifyHitUnit(action, Victim, ref damage, ref crit);
        Victim.OnHitByUnit(action, this, damage, crit);
    }
}
