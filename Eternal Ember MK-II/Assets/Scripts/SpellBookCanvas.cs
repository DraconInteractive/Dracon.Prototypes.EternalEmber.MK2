using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;

public class SpellBookCanvas : MonoBehaviour {
    public UIWindow window;

    public GameObject skillContainer, spellContainer, talentContainer;
    public UISpellSlot[] skills, spells;
    public UITalentApply[] talents;
    public Spell [] allSpells;
	public Attack [] allSkills;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.K))
        {
            window.Toggle();
        }
	}

    public void OnWindowOpen ()
    {
        spells = spellContainer.GetComponentsInChildren<UISpellSlot>(true);

        for (int i = 0; i < spells.Length; i ++)
        {
			
            spells[i].Assign(allSpells[i].assocSpell.info);
            GameObject infoObj = spells[i].GetComponent<SpellSlotInfoLink>().infoObject;
            infoObj.transform.GetChild(1).GetComponent<Text>().text = allSpells[i].assocSpell.info.Description;
            infoObj.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = allSpells[i].assocSpell.info.Name;
            infoObj.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "Rank <b>" + allSpells[i].Rank.ToString() + "</b>";
        }
        talents = talentContainer.GetComponentsInChildren<UITalentApply>(true);
        for (int i = 0; i < talents.Length; i++)
        {
            talents[i].Apply();
        }

		skills = skillContainer.GetComponentsInChildren<UISpellSlot> (true);
		GameObject skillSlotPrefab = skills [0].transform.parent.gameObject;
//		for (int i = 0; i < skills.Length; i++) {
//			UISpellInfo nInfo = Ability.CreateSpellInfoFromAbility (allSkills [i].assocAttack);
//			skills [i].Assign (nInfo);
//			GameObject infoObj = skills [i].GetComponent<SpellSlotInfoLink> ().infoObject;
//			infoObj.transform.GetChild (1).GetComponent<Text> ().text = allSkills [i].assocAttack.abilityDesc;
//			infoObj.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = allSkills[i].assocAttack.abilityName;
//			infoObj.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "Rank <b>" + allSkills[i].Rank.ToString() + "</b>";
//		}
		for (int i = 0; i < allSkills.Length; i++) {
			UISpellInfo nInfo = Ability.CreateSpellInfoFromAbility (allSkills [i].assocAttack);
			if (i > skills.Length - 1) {
				GameObject newSlot = Instantiate (skillSlotPrefab, skills [0].transform.parent.transform.parent);
				UISpellSlot nSlot = newSlot.transform.GetChild(0).GetComponent<UISpellSlot> ();

				nSlot.Assign (nInfo);
				GameObject infoObj = nSlot.GetComponent<SpellSlotInfoLink> ().infoObject;
				infoObj.transform.GetChild (1).GetComponent<Text> ().text = allSkills [i].assocAttack.abilityDesc;
				infoObj.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = allSkills[i].assocAttack.abilityName;
				infoObj.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "Rank <b>" + allSkills[i].Rank.ToString() + "</b>";
			} else {
				print ("AS: " + allSkills.Length + " S: " + skills.Length + "I: " + i);
				skills [i].Assign (nInfo);
				GameObject infoObj = skills [i].GetComponent<SpellSlotInfoLink> ().infoObject;
				infoObj.transform.GetChild (1).GetComponent<Text> ().text = allSkills [i].assocAttack.abilityDesc;
				infoObj.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = allSkills[i].assocAttack.abilityName;
				infoObj.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "Rank <b>" + allSkills[i].Rank.ToString() + "</b>";
			}

		}
    }
}
[Serializable]
public class Spell
{
    public Spell_Type assocSpell;
    public int Rank;
}

[Serializable]
public class Attack
{
	public Ability assocAttack;
	public int Rank;
}


