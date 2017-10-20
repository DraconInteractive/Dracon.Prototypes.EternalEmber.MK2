using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterStatistics))]
public class Enemy : MonoBehaviour {
    public static List<Enemy> allEnemies = new List<Enemy> ();

    CharacterStatistics enemyHealth;
    Animator anim;

    bool inCombat;

	public enum HitResponse {Color, Animation};
	public HitResponse hitResponse;
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

	public void OnHit () {
		switch (hitResponse) {
		case HitResponse.Color:
			
			break;
		case HitResponse.Animation:
			break;
		}
	}

	IEnumerator PulseColor (float duration, Color c) {
		Renderer[] renderers = GetComponentsInChildren<Renderer> ();

		float d = 0;

		while (d < duration * 0.5f) {

			for (int i = 0; i < renderers.Length; i++) {
				renderers [i].material.color = Color.Lerp (renderers [i].material.color, c, 0.4f);
			}
			d += Time.deltaTime;
			yield return null;
		}

		while (d > 0) {
			for (int i = 0; i < renderers.Length; i++) {
				renderers [i].material.color = Color.Lerp (renderers [i].material.color, Color.white, 0.4f);
			}
			d -= Time.deltaTime;
			yield return null;
		}

		for (int i = 0; i < renderers.Length; i++) {
			renderers [i].material.color = Color.white;
		}
		yield break;
	}
}
