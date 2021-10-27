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
    public int attackBonusMult;
    public int defenceBase;
    public int defenceBonusAdd;
    public int defenceBonusMult;
    public int damageReduction;
    public int recoil;
    // TODO: Make Action class which holds an active skill: it's name, description, stats, function e.t.c. it shouldn't include any tree parts. They will be parts of the wrapper.
    //public List<Action> actions; 

    delegate void onHitHandler(Unit sender, Unit attacker, int DamageBase);
    delegate void onAttackHandler(Unit sender, Unit target, int DamageBase);
    delegate void preTurnHandler(Unit sender);
    delegate void postTurnHandler(Unit sender);

    public Unit()
    {
        setDefaults();
    }

    public virtual void setDefaults() { }
}
