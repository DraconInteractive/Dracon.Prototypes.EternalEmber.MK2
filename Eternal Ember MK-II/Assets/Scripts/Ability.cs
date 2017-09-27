using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;

public class Ability : ScriptableObject {
    string abilityName;
    public float range, damage, damageDelay;
    public bool delayByDistance;

    public string animationName;

    public enum Effect { Damage, StatChange, Status};
    public Effect effect;

    public Sprite abilityIcon;


    public float cost;
    public virtual bool ProcAbility (GameObject target)
    {
        //Debug.Log("Ability " + abilityName + " called");
        Player.player.GetComponent<Animator>().SetTrigger("Attack_" + animationName);
        CharacterStatistics stat = Player.player.GetComponent<CharacterStatistics>();
        if (stat.mana.current > cost)
        {
            Debug.Log("Attack returned true");
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

        switch (effect)
        {
            case Effect.Damage:
                CharacterStatistics targetHealth = target.GetComponent<CharacterStatistics>();
                if (targetHealth != null)
                {
                    targetHealth.Damage(targetHealth.health, -damage);
                }
                break;
            case Effect.StatChange:
                break;
            case Effect.Status:
                break;
        }
    }
}
