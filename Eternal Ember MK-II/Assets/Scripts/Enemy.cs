using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public static List<Enemy> allEnemies = new List<Enemy> ();

    CharacterStatistics enemyHealth;
    Animator anim;

    bool inCombat;

    public bool InCombat
    {
        get
        {
            return inCombat;
        }

        set
        {
            inCombat = value;
            anim.SetBool ("inCombat", value);
        }
    }

    private void Awake()
    {
        allEnemies.Add (this);
        enemyHealth = GetComponent<CharacterStatistics> ();
        enemyHealth.onDeath = OnDeath;
        anim = GetComponent<Animator> ();
    }
    // Use this for initialization
    void Start () {
        InCombat = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDeath ()
    {
        print("Enemy Death");
        allEnemies.Remove (this);
        Player.player.detectedEnemies.Remove (this);
        anim.SetTrigger("Death");
        Destroy (this);
    }
}
