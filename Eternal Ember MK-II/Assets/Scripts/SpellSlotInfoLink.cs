using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlotInfoLink : MonoBehaviour {

    public Text nameText, descText, rankText;

	public void GetValues () {
		Transform p = transform.parent;
		nameText = p.GetChild (1).transform.GetChild (0).GetComponent<Text> ();
		descText = p.GetChild (2).GetComponent<Text> ();
		rankText = p.GetChild (1).transform.GetChild (1).transform.GetChild(1).GetComponent<Text> ();
	}
}
