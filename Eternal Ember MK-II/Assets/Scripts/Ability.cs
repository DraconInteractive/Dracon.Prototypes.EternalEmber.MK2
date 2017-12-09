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

	public virtual bool ProcAbility (GameObject caster, GameObject target, bool animateStandard)
    {
        //Debug.Log("Ability " + abilityName + " called");
        
		CharacterStatistics stat = caster.GetComponent<CharacterStatistics>();
        if (stat.mana.current > cost)
        {
            Debug.Log("Attack returned true");
			if (animateStandard) {
				caster.GetComponent<Animator> ().SetInteger ("attackIndex", animationIndex);
				caster.GetComponent<Animator>().SetTrigger("Attack_" + animationName);
			}
            
			stat.Damage(target, stat.mana, -cost);
            return true;
        } 
        else
        {
            Debug.Log("Attack returned false");
            return false;
        }
    }

    public virtual void ApplyEffect(GameObject caster, GameObject target)
    {
		foreach (AbilityEffect effect in effects) {
			if (effect.type == AbilityEffect.EffectType.Damage) {
				if (targetType == TargetType.Enemy) {
					CharacterStatistics targetHealth = target.GetComponent<CharacterStatistics>();
					if (targetHealth != null)
					{
						targetHealth.Damage(target, targetHealth.health, -effect.power);
					}
				} else if (targetType == TargetType.EnemyRange) {
					Collider[] colliders = Physics.OverlapSphere (target.transform.position, effect.AOERange);
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
							targetHealth.Damage(target, targetHealth.health, -effect.power);
						}
					}
				}

			}
			else if (effect.type == AbilityEffect.EffectType.StatChange) {
				if (targetType == TargetType.Self) {
					switch (effect.AffectedStat) {
					case AbilityEffect.Stat.Accuracy:
						Player.player.ProcApplyStatChange (caster.GetComponent<CharacterStatistics>().meleeAccuracy, effect.power, effect.duration);
						Player.player.ProcApplyStatChange (caster.GetComponent<CharacterStatistics>().rangedAccuracy, effect.power, effect.duration);
						break;
					case AbilityEffect.Stat.Resistance:
						Player.player.ProcApplyStatChange (caster.GetComponent<CharacterStatistics>().meleeResist, effect.power, effect.duration);
						Player.player.ProcApplyStatChange (caster.GetComponent<CharacterStatistics>().magicResist ,effect.power, effect.duration);
						break;
					}
				}

			}
			else if (effect.type == AbilityEffect.EffectType.Status) {
				List<GameObject> targets = new List<GameObject> ();
				if (targetType == TargetType.Self) {
					
					targets.Add(caster);
					 
				} else if (targetType == TargetType.EnemyRange) {
					
					Collider[] colliders = Physics.OverlapSphere (target.transform.position, effect.AOERange);
					List<Enemy> targetEnemies = new List<Enemy> ();
					foreach (Collider c in colliders) {
						Enemy e = c.gameObject.GetComponent<Enemy> ();
						if (e != null) {
							targets.Add (e.gameObject);
						}
					}

				} else if (targetType == TargetType.Enemy) {
					targets.Add (target);
				}
		
				Player.player.ProcStatus (effect.EffectStatus, effect.power, effect.duration, targets);
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

	public float AOERange;
}
