using System.Collections.Generic;
using UnityEngine;


// This class will never get an instance, it's used for easier creation, compatability and possibly (not approved, so don't get your hopes up) modding
public enum UnitType
{
    Human, Undead, Construct
}

public abstract class Unit : MonoBehaviour
{
    public string unitName;

    public int maxHP;
    public int currentHP;
    public bool isDead;

    public int defenceBase;
    public float damageReduction;
    
    public int recoil = 0;
    public bool Allied;
    public bool playerControl;
    public List<UnitType> unitTypes;
    public List<Action> actions = new List<Action>();
    public List<Unit> allies;
    public List<Unit> enemies;
    public List<Passive>[] passives = new List<Passive>[5] { new List<Passive>(), new List<Passive>(), new List<Passive>(), new List<Passive>(), new List<Passive>()};
    public List<Status>[] statuses = new List<Status>[5] { new List<Status>(), new List<Status>(), new List<Status>(), new List<Status>(), new List<Status>() };

    // Ignore everything below, this part is still in progress //

    void Awake()
    {
        SetDefaults();
    }
    public void init(Unit unit)
    {
        Debug.Log(unit.unitName);
        unit.transform.parent.GetComponentInChildren<UnitHUDScript>().UpdateHP(unit);
    }

    /// <summary>
    /// Call this to attack the unit, if your skill doesn't attack, don't call this. If you want to deal damage through non-skill means(debuffs etc.) use Hurt(). If you want to bypass any and all Damage modification, use TakeDamage().
    /// Returns true if the attack is successful
    /// </summary>
    /// <param name="attacker">insert the attacking untit</param>
    /// <param name="attack">insert the attack action</param>
    public bool Attack (Unit Victim, Unit attacker, Action attack, int damage, bool crit, int piercingDamage)
    {
        Debug.Log(Victim.statuses);
        Debug.Log(attacker.unitName);
        Debug.Log(attack);
        for (int i = 0; i < 5; i++)
        {
            if(Victim.statuses[i].Count>0)
            foreach (Status status in Victim.statuses[i])
            {
                status.ModifyIncomingDamage(ref Victim, ref attacker, ref attack, ref damage, ref crit, ref piercingDamage);
            }

            if(Victim.passives[i].Count > 0)
            foreach (Passive passive in Victim.passives[i])
            {
                passive.ModifyIncomingDamage(ref Victim, ref attacker, ref attack, ref damage, ref crit, ref piercingDamage);
            }
        }
        Victim.ModifyIncomingDamage(ref Victim, ref attacker, ref attack, ref damage, ref crit, ref piercingDamage);
        damage = Mathf.RoundToInt(Mathf.Max((damage - piercingDamage - Victim.defenceBase) * (1 - Victim.damageReduction), 0));
        if (crit) damage *= 2;
        damage += piercingDamage;

        for (int i = 0; i < 5; i++)
        {
            foreach (Status status in Victim.statuses[i])
            {
                status.PreHit(Victim, attacker, attack, damage, crit);
            }
            foreach (Passive passive in Victim.passives[i])
            {
                passive.PreHit(Victim, attacker, attack, damage, crit);
            }
        }
        Victim.PreHit(Victim, attacker, attack, damage, crit);

        bool canHurt = !Victim.isDead;
        for (int i = 0; i < 5; i++)
        {
            foreach (Status status in Victim.statuses[i])
            {
                canHurt = canHurt && status.CanHurt(Victim, attacker, attack, damage, crit);
            }
            foreach (Passive passive in Victim.passives[i])
            {
                canHurt = canHurt && passive.CanHurt(Victim, attacker, attack, damage, crit);
            }
        }
        canHurt = canHurt && Victim.CanHurt(Victim, attacker, attack, damage, crit);

        if (canHurt)
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (Status status in Victim.statuses[i])
                {
                    status.OnHit(Victim, attacker, attack, damage, crit);
                }
                foreach (Passive passive in Victim.passives[i])
                {
                    passive.OnHit(Victim, attacker, attack, damage, crit);
                }
            }
            Victim.OnHit(Victim, attacker, attack, damage, crit);
            Victim.Hurt(Victim,attacker, damage);

            for (int i = 0; i < 5; i++)
            {
                foreach (Status status in Victim.statuses[i])
                {
                    status.PostHit(Victim, attacker, attack, damage, crit);
                }
                foreach (Passive passive in Victim.passives[i])
                {
                    passive.PostHit(Victim, attacker, attack, damage, crit);
                }
            }
            Victim.PostHit(Victim, attacker, attack, damage, crit);
        }

        Victim.PostAssault(Victim, attacker, attack, damage, crit, canHurt);
        ///TODO PostAssault for each passive and status
        return canHurt;
    }
    public virtual void ModifyIncomingDamage(ref Unit Victim, ref Unit attacker, ref Action attack, ref int damage, ref bool crit, ref int piercingDamage) { }
    /// <summary>
    /// Override to modify what happens right before the unit gets attacked
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="attack"></param>
    /// <param name="damage"></param>
    /// <param name="crit"></param>
    public virtual void PreHit(Unit Victim, Unit attacker, Action attack, int damage, bool crit) { }
    /// <summary>
    /// return true if you want the Unit to take damage
    /// </summary>
    /// <param name="crit"></param>
    /// <param name="damage"></param>
    /// <returns></returns>
    public virtual bool CanHurt(Unit Victim, Unit attacker, Action attack, int damage, bool crit) { return true; }
    /// <summary>
    /// Override to modify what happens right before the unit gets attacked
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="attack"></param>
    /// <param name="damage"></param>
    /// <param name="crit"></param>
    public virtual void OnHit(Unit Victim, Unit attacker, Action attack, int damage, bool crit) { }
    /// <summary>
    /// Override to modify what happens right after the unit gets attacked
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="attack"></param>
    /// <param name="damage"></param>
    /// <param name="crit"></param>
    public virtual void PostHit(Unit Victim, Unit attacker, Action attack, int damage, bool crit) { }
    /// <summary>
    /// Override to modify what happens right after the unit gets attacked
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="attack"></param>
    /// <param name="damage"></param>
    /// <param name="crit"></param>
    public virtual void PostAssault(Unit Victim, Unit attacker, Action attack, int damage, bool crit, bool Success) { }

    /// <summary>
    /// Call if you only want to call functions that correspond to only taking pure damage
    /// </summary>
    /// <param name="damageSource"></param>
    /// <param name="damage"></param>
    public void Hurt (Unit Victim, MonoBehaviour damageSource, int damage)
    {
        Victim.OnHurt(damageSource,damage);
        ///TODO OnHurt for each passive and skill

        Victim.TakeDamage(Victim,damageSource,damage);

        Victim.PostHurt(damageSource,damage);
        ///TODO PostHurt for each passive and skill
    }
    /// <summary>
    /// Override to modify what you want to happen right before the Unit takes damage
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="oldDamage"></param>
    public virtual void OnHurt(MonoBehaviour damageSource, int damage) { }
    /// <summary>
    /// Takes Damage without processing
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(Unit Victim, MonoBehaviour damageSource, int damage) 
    {
        Victim.currentHP -= damage;
        if (Victim.currentHP <= 0)
        {
            Victim.Death(Victim,damageSource);
        }
        Victim.transform.parent.GetComponentInChildren<UnitHUDScript>().UpdateHP(Victim);
    } 
    /// <summary>
    /// Override to modify what you want to happen right after the Unit takes damage
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="oldDamage"></param>
    public virtual void PostHurt(MonoBehaviour damageSource, int damage) { }

    /// <summary>
    /// Call this if you want to try to kill the unit
    /// </summary>
    private void Death(Unit Victim,MonoBehaviour killer)
    {
        if (Victim.currentHP <= 0) {
            Victim.PreDeath(killer);
            ///TODO PreDeath for each passive and status

            bool canDie = Victim.CanDie(killer);
            ///TODO canDie for each passive and status
            
            if (canDie)
            {
                Victim.OnDeath(killer);
                ///TODO PreDeath for each passive and status

                if (killer.Equals(typeof(Unit)))
                    ((Unit)killer).OnKill(Victim);
                    

                Die(Victim, killer);

                Victim.PostDeath(killer);
                ///TODO PostDeath for each passive and status

                if (killer.Equals(typeof(Unit)))
                    ((Unit)killer).PostKill(Victim);
            }
        }
    }

    public virtual void PreDeath(MonoBehaviour killer) { }
    public virtual bool CanDie(MonoBehaviour killer) { return true; }
    public virtual void OnDeath(MonoBehaviour killer) { }
    public virtual void OnKill(MonoBehaviour Victim) { }
    public void Die(Unit Victim, MonoBehaviour killer) 
    {
        Victim.isDead = true;
        Victim.currentHP = 0;
    }
    public virtual void PostDeath(MonoBehaviour killer) { }
    public virtual void PostKill(MonoBehaviour killer) { }

    // uses old healing algorythm, not completely finished.

    public void Heal(Unit Patient, MonoBehaviour healingSource,int healAmount) 
    {
        Patient.OnHeal(healAmount);

        TakeHealing(Patient,healAmount);

        Patient.PostHeal(healAmount);

    }
    /// <summary>
    /// Changes healing before multiplications.
    /// </summary>
    /// <param name="healAmount"></param>
    public virtual void ModifyHealing1(ref int healAmount) { }
    /// <summary>
    /// If you wish to multiply healing, do it here.
    /// </summary>
    /// <param name="healAmount"></param>
    public virtual void ModifyHealing2(ref int healAmount) { }
    /// <summary>
    /// Changes healing after multiplications.
    /// </summary>
    /// <param name="healAmount"></param>
    public virtual void ModifyHealing3(ref int healAmount) { }
    /// <summary>
    /// Last resort damage modification, use only if you want to override all previous modification. Exists only for the Unit.
    /// </summary>
    /// <param name="healAmount"></param>
    public virtual void ModifyHealing4(ref int healAmount) { }
    /// <summary>
    /// Override to modify what you want to happen right before the Unit takes damage
    /// </summary>
    /// <param name="healAmount"></param>
    /// <param name="oldHealAmount"></param>
    public virtual void OnHeal(int healAmount) { }
    /// <summary>
    /// Takes Healing without processing
    /// </summary>
    /// <param name="damage"></param>
    public void TakeHealing(Unit Patient,int healAmount)
    {
        Patient.currentHP += healAmount;
        if (Patient.currentHP > Patient.maxHP)
        {
            Patient.currentHP = Patient.maxHP;
        }
        Patient.transform.parent.GetComponentInChildren<UnitHUDScript>().UpdateHP(Patient);
    }
    /// <summary>
    /// Override to modify what you want to happen right after the Unit takes damage
    /// </summary>
    /// <param name="healAmount"></param>
    /// <param name="oldHealAmount"></param>
    public virtual void PostHeal(int healAmount) { }
    public abstract void SetDefaults();
    public static bool CheckUnitType(Unit unit, IEnumerable<UnitType> unitTypes)
    {
        bool res = true;
        foreach (UnitType type in unitTypes)
        {
            res = res && unit.unitTypes.Contains(type);
        }
        return res;
    }

    public virtual Action AI()
    {
        for (int i = actions.Count-1; i >= 0; i--)
        {
            if (actions[i].UsesLeft != 0 && (actions[i].GetValidTargets(this).Count>0))
                return actions[i];
        }
        return new Skip();
    }
}
