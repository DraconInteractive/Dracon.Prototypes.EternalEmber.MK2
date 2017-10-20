using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;

public class UnitFrame : MonoBehaviour {
    public static UnitFrame playerFrame, enemyFrame;
    public UIProgressBar hpBar, mpBar;
    public Text hpText, mpText, levelText;
    public bool isPlayer;
	// Use this for initialization
	void Awake () {
		if (isPlayer)
        {
            playerFrame = this;
        }
	}

    private void Start()
    {
        Invoke("UpdateFrame", 0.2f);
    }

    // Update is called once per frame
    void Update () {

	}
}
