using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;

public class UITalentApply : MonoBehaviour {
    UITalentSlot slot;
    public Spell targetSpell;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Apply ()
    {
        slot = GetComponent<UITalentSlot>();
        print(name + " applied");
        UITalentInfo info = new UITalentInfo();
        info.maxPoints = 5;
        slot.Assign(info, targetSpell.assocSpell.info);
    }
}
