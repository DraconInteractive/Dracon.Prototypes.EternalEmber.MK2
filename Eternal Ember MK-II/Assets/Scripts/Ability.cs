using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;

public class Ability : ScriptableObject {

	public int ID;
    public string abilityName;
	public string abilityDesc;
    public float range, damageDelay;
    public bool delayByDistance;

    public string animationName;
	public int animationIndex;

   
	public List<AbilityEffect> effects = new List<AbilityEffect>();

    public Sprite abilityIcon;

    public float cost;
	public float cooldown;

	public enum TargetType {Self, Enemy, EnemyRange};
	public TargetType targetType;

    public virtual bool ProcAbility (GameObject target)
    {
        //Debug.Log("Ability " + abilityName + " called");
        
        CharacterStatistics stat = Player.player.GetComponent<CharacterStatistics>();
        if (stat.mana.current > cost)
        {
            Debug.Log("Attack returned true");
			Player.player.GetComponent<Animator> ().SetInteger ("attackIndex", animationIndex);
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
		foreach (AbilityEffect effect in effects) {
			if (effect.type == AbilityEffect.EffectType.Damage) {
				if (targetType == TargetType.Enemy) {
					CharacterStatistics targetHealth = target.GetComponent<CharacterStatistics>();
					if (targetHealth != null)
					{
						targetHealth.Damage(targetHealth.health, -effect.power);
					}
				} else if (targetType == TargetType.EnemyRange) {
					Collider[] colliders = Physics.OverlapSphere (target.transform.position, effect.duration);
					List<Enemy> targetEnemies = new List<Enemy> ();
					foreach (Collider c in colliders) {
						Enemy e = c.gameObject.GetComponent<Enemy> ();
						if (e != null) {
							targetEnemies.Add (e);
						}
					}
					foreach (Enemy enemy in targetEnemies) {
						CharacterStatistics targetHealth = enemy.GetComponent<CharacterStatistics>();
						if (targetHealth != null)
						{
							targetHealth.Damage(targetHealth.health, -effect.power);
						}
					}
				}

			}
			else if (effect.type == AbilityEffect.EffectType.StatChange) {
				if (targetType == TargetType.Self) {
					switch (effect.AffectedStat) {
					case AbilityEffect.Stat.Accuracy:
						Player.player.ProcApplyStatChange (Player.player.playerStats.meleeAccuracy, effect.power, effect.duration);
						Player.player.ProcApplyStatChange (Player.player.playerStats.rangedAccuracy, effect.power, effect.duration);
						break;
					case AbilityEffect.Stat.Resistance:
						Player.player.ProcApplyStatChange (Player.player.playerStats.meleeResist, effect.power, effect.duration);
						Player.player.ProcApplyStatChange (Player.player.playerStats.magicResist, effect.power, effect.duration);
						break;
					}
				}

			}
			else if (effect.type == AbilityEffect.EffectType.Status) {
				Player.player.ProcStatus (effect.EffectStatus, effect.power, effect.duration);
			}
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

[Serializable]
public class AbilityEffect {
	
	public enum EffectType { Damage, StatChange, Status};
	public EffectType type;

	public float power, duration;

	public enum Stat {None, Accuracy, Damage, Resistance};
	public Stat AffectedStat;

	public enum Status {None, Ignite, Blind, Root, Mark};
	public Status EffectStatus;
}
