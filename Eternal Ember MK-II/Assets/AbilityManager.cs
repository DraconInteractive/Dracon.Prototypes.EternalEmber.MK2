using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Manager", menuName = "Generate/Manager/Ability Manager")]
public class AbilityManager : ScriptableObject {
	private static AbilityManager _instance;
	public static AbilityManager Instance
	{
		get 
		{
			if (!_instance) {
				//_instance = Resources.FindObjectsOfTypeAll<AbilityManager> ().FirstOrDefault ();
				_instance = Resources.Load ("AbilityManager") as AbilityManager;
			}

			return _instance;
		}
	}

	public List<Ability> allAbilities = new List<Ability>();

	public Ability GetAbilityByID (int id) {
		foreach (Ability a in allAbilities) {
			if (a.ID == id) {
				return a;
			}
		}

		return null;
	}

	public Ability GetAbilityByName (string n) {
		foreach (Ability a in allAbilities) {
			if (a.abilityName == n) {
				return a;
			}
		}
		return null;
	}

	public void AssignIDs () {
		int counter = 0;
		foreach (Ability a in allAbilities) {
			a.ID = counter;
			counter++;
		}
	}
}
