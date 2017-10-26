using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

[RequireComponent(typeof(CharacterStatistics))]
public class Enemy : MonoBehaviour {
    public static List<Enemy> allEnemies = new List<Enemy> ();

    CharacterStatistics enemyHealth;
    Animator anim;

    bool inCombat;

	public enum HitResponse {Color, Animation};
	public HitResponse hitResponse;

	bool hasOutline;
	Outline outline;
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

	bool targeted;
	public GameObject targetedIndicator;
    private void Awake()
    {
        allEnemies.Add (this);
        enemyHealth = GetComponent<CharacterStatistics> ();
        enemyHealth.onDeath = OnDeath;
        anim = GetComponent<Animator> ();
		outline = GetComponent<Outline> ();
		if (outline != null) {
			hasOutline = true;
		} else {
			hasOutline = false;
		}
		targetedIndicator.SetActive (false);
    }
    // Use this for initialization
    void Start () {
        InCombat = false;
		if (hasOutline) {
			outline.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseEnter () {
		if (hasOutline) {
			outline.enabled = true;
		}
	}

	void OnMouseExit () {
		if (hasOutline) {
			outline.enabled = false;
		}
	}

	void OnMouseDown () {
		if (Player.player.targetedEnemy != null) {
			Player.player.targetedEnemy.GetComponent<Enemy> ().ToggleTargeted(false);
		}

		Player.player.targetedEnemy = this.gameObject;
		ToggleTargeted (true);
	}

	public void ToggleTargeted (bool state) {
		targeted = state;
		targetedIndicator.SetActive (state);
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
