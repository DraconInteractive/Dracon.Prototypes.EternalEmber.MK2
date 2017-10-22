using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;

public class ActionBarAbilityLink : MonoBehaviour {
	Ability assocAbility;

	public void OnAssigned (UISpellSlot slot) {
		UISpellInfo info = slot.GetSpellInfo();
		assocAbility = AbilityManager.Instance.GetAbilityByName (info.Name);

	}

	public void OnUnAssign (UISpellSlot slot) {
		assocAbility = null;
	}

	public void TriggerLinkedAbility (UISpellSlot slot) {
//		// Handle cooldown just for the demonstration
//		if (this.m_Cooldown != null)
//		{
//			// If the spell is not on cooldown
//			if (!this.m_Cooldown.IsOnCooldown)
//			{
//				// Start the cooldown
//				this.m_Cooldown.StartCooldown(this.m_SpellInfo.ID, this.m_SpellInfo.Cooldown);
//			}
//		}
		if (assocAbility == null) {
			return;
		}
		GameObject target = null;
		if (assocAbility.requiresEnemy) {
			target = Player.player.targetedEnemy;
		} else {
			target = Player.player.gameObject;
		}
		if (target != null) {
			bool b = assocAbility.ProcAbility (target);

			if (slot.cooldownComponent != null) {
				if (!slot.cooldownComponent.IsOnCooldown && b) {
					slot.cooldownComponent.StartCooldown (slot.GetSpellInfo ().ID, slot.GetSpellInfo ().Cooldown);
				}
			}

		}
	}
}
