using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive
{
    int weight;
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
}
