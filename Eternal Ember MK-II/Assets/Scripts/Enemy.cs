using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using cakeslice;

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
			if (value == true) {
				if (combatActions.Count > 0) {
					StopCoroutine (actionRoutine);
					actionRoutine = StartCoroutine (PerformAction(combatActions));
				}

			} else {
				if (baseActions.Count > 0) {
					StopCoroutine (actionRoutine);
					actionRoutine = StartCoroutine (PerformAction (baseActions));
				}

			}

        }
    }

	bool targeted;
	public GameObject targetedIndicator;

	public List<EnemyAction> baseActions, combatActions;
	public EnemyAction currentAction;
	Coroutine actionRoutine;
	public Ability attackAbility;

	public EnemyProfile profile;
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
		actionRoutine = StartCoroutine (PerformAction (baseActions));
	}
	
	// Update is called once per frame
	void Update () {
		if (actionRoutine == null) {
			if (inCombat && combatActions.Count > 0) {
				actionRoutine = StartCoroutine (PerformAction (combatActions));
			} else if (!inCombat && baseActions.Count > 0) {
				actionRoutine = StartCoroutine (PerformAction (baseActions));
			}
		} 
	}

	IEnumerator PerformAction (List<EnemyAction> inputActions) {
		List<EnemyAction> actions = new List<EnemyAction> (inputActions);
		while (actions.Count > 0) {
			
			EnemyAction thisAction = actions [0];
			currentAction = thisAction;
			int repeats = thisAction.repeats;
			while (repeats >= 0) {
				thisAction.PlayAnim (anim);
				switch (thisAction.action) {
				case EnemyAction.ActionType.GoTo:
					agent.SetDestination (thisAction.GetGotoPoint ());
					agent.isStopped = false;
					yield return null;
					while (Vector3.Distance (transform.position, agent.destination) > thisAction.gotoDist) {
						yield return null;
					}
					agent.isStopped = true;
					yield return null;
					break;
				case EnemyAction.ActionType.Attack:
					bool b = attackAbility.ProcAbility (this.gameObject, Player.player.gameObject, false);
					if (!b) {
						yield break;
					}
					float delay = attackAbility.damageDelay;
					if (attackAbility.delayByDistance) { 
						delay += Vector3.Distance (Player.player.transform.position, transform.position) * 0.05f;
					}
					yield return new WaitForSeconds (delay);

					attackAbility.ApplyEffect (this.gameObject, Player.player.gameObject);
					yield return new WaitForSeconds (0.15f);
					yield return null;
					break;
				case EnemyAction.ActionType.Wait:
					//yield return new WaitForSeconds (thisAction.duration);
					float timer = thisAction.duration;
					while (timer > 0) {
						timer -= Time.deltaTime;
						yield return null;
					}

					break;
				case EnemyAction.ActionType.Interact:
					yield return new WaitForSeconds (0.5f);
					//Interact
					break;
				}

				repeats--;
			}

			currentAction = null;
			actions.Remove (thisAction);
			yield return null;
		}
		actionRoutine = null;
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
	public enum ActionType {GoTo, Attack, Wait, Interact};
	public ActionType action;

	public float duration;
	public Vector3 gotoPoint;
	public GameObject gotoObject;
	public float gotoDist;

	public int repeats;

	public string animTrigger;

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

	public void PlayAnim (Animator animator) {
		animator.SetTrigger (animTrigger);
	}
}
