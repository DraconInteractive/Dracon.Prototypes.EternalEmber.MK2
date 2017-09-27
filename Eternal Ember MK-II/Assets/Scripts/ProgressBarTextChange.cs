using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;

public class ProgressBarTextChange : MonoBehaviour {
    public UIProgressBar assocBar;
    Text t;
	// Use this for initialization
	void Start () {
        t = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBarChange ()
    {
        float f = assocBar.fillAmount * 100;
        t.text = f.ToString() + " / 100";
    }
}
