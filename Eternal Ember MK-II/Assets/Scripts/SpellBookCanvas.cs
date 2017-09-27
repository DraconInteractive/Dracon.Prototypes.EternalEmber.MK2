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
        spells = spellContainer.GetComponentsInChildren<UISpellSlot>();
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
    }
}
[Serializable]
public class Spell
{
    public Spell_Type assocSpell;
    public int Rank;
}


