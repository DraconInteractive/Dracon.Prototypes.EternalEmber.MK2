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


    public void UpdateFrame()
    {
        CharacterStatistics stats = Player.player.GetComponent<CharacterStatistics>();
        hpBar.fillAmount = stats.health.current / stats.health.maximum;
        mpBar.fillAmount = stats.mana.current / stats.mana.maximum;
        float hpt = (int)(hpBar.fillAmount * 100);
        float mpt = (int)(mpBar.fillAmount * 100);
        hpText.text = hpt.ToString() + "%";
        mpText.text = mpt.ToString() + "%";
        levelText.text = stats.level.current.ToString();

		Invoke ("UpdateFrame", 0.2f);
    }

    void SetupFrame ()
    {
        
    }
}
