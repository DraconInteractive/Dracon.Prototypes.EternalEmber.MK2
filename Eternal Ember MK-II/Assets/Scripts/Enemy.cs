using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using cakeslice;

[RequireComponent(typeof(CharacterStatistics))]
public class Enemy : MonoBehaviour {
    public static List<Enemy> allEnemies = new List<Enemy> ();

    CharacterStatistics enemyHealth;
	NavMeshAgent agent;
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

	public List<EnemyAction> actions;
    private void Awake()
    {
        allEnemies.Add (this);
        enemyHealth = GetComponent<CharacterStatistics> ();
		agent = GetComponent<NavMeshAgent> ();
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
		StartCoroutine (PerformAction ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator PerformAction () {
		while (actions.Count > 0) {
			EnemyAction thisAction = actions [0];
			switch (thisAction.action) {
			case EnemyAction.ActionType.GoTo:
				agent.SetDestination (thisAction.GetGotoPoint ());
				yield return null;
				while (agent.velocity.magnitude >= 0.15f) {
					yield return null;
				}
				break;
			case EnemyAction.ActionType.Attack:
				break;
			case EnemyAction.ActionType.Wait:
				break;
			}

			actions.Remove (thisAction);
			yield return null;
		}

		yield break;
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
			StartCoroutine (PulseColor (0.2f, Color.red));
			break;
		case HitResponse.Animation:
			anim.SetTrigger ("Damage");
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
[Serializable]
public class EnemyAction {
	public enum ActionType {GoTo, Attack, Wait};
	public ActionType action;

	public float duration;
	public Vector3 gotoPoint;
	public GameObject gotoObject;

	public float GetGotoDistance (Vector3 start) {
		if (gotoObject != null) {
			return Vector3.Distance (start, gotoObject.transform.position);
		} else {
			return Vector3.Distance (start, gotoPoint);
		}
	}

	public Vector3 GetGotoPoint () {
		if (gotoObject != null) {
			return gotoObject.transform.position;
		} else {
			return gotoPoint;
		}
	}
}
