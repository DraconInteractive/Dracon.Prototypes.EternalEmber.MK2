using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;

public class Ability : ScriptableObject {

	public int ID;
    public string abilityName;
	public string abilityDesc;
    public float range, damage, damageDelay;
    public bool delayByDistance;

    public string animationName;

    public enum Effect { Damage, StatChange, Status};
	public List<Effect> effects = new List<Effect>();

    public Sprite abilityIcon;

    public float cost;
	public float cooldown;

	public enum Stat {None, Accuracy, Damage, Resistance};
	public Stat AffectedStat;
	public enum Affect {None, Ignite, Blind, Root};
	public Affect AppliedEffect;

	public bool requiresEnemy;

    public virtual bool ProcAbility (GameObject target)
    {
        //Debug.Log("Ability " + abilityName + " called");
        
        CharacterStatistics stat = Player.player.GetComponent<CharacterStatistics>();
        if (stat.mana.current > cost)
        {
            Debug.Log("Attack returned true");
			Player.player.GetComponent<Animator>().SetTrigger("Attack_" + animationName);
            stat.Damage(stat.mana, -cost);
            return true;
        } 
        else
        {
            Debug.Log("Attack returned false");
            return false;
        }
    }

    public virtual void ApplyEffect(GameObject target)
    {
		if (effects.Contains(Effect.Damage)) {
            CharacterStatistics targetHealth = target.GetComponent<CharacterStatistics>();
            if (targetHealth != null)
            {
                targetHealth.Damage(targetHealth.health, -damage);
            }
		}

		if (effects.Contains (Effect.StatChange)) {
			switch (AffectedStat) {
			case Stat.Accuracy:
				Player.player.ProcApplyStatChange (Player.player.playerStats.meleeAccuracy, damage, range);
				Player.player.ProcApplyStatChange (Player.player.playerStats.rangedAccuracy, damage, range);
				break;
			case Stat.Resistance:
				Player.player.ProcApplyStatChange (Player.player.playerStats.meleeResist, damage, range);
				Player.player.ProcApplyStatChange (Player.player.playerStats.magicResist, damage, range);
				break;
			}
		}

		if (effects.Contains (Effect.Status)) {
			Player.player.ProcStatus (AppliedEffect, damage, damageDelay);
		}

    }



	public static UISpellInfo CreateSpellInfoFromAbility (Ability a) {
		UISpellInfo info = new UISpellInfo ();
		info.ID = a.ID;
		info.Name = a.name;
		info.Icon = a.abilityIcon;
		info.Description = a.abilityDesc;
		info.Range = a.range;
		info.Cooldown = a.cooldown;
		info.CastTime = a.damageDelay;
		info.PowerCost = a.cost;
		return info;
	}
}
