using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeawispHunter.MinibufferConsole;
using UnityEngine.UI;
using DuloGames.UI;

public class Player : MonoBehaviour {

    public static Player player;
	[HideInInspector]
    public Animator anim;
    AudioSource audioSource;
	public StatisticsProfile playerStats;
	public float hp_current, mp_current;
	Rigidbody rb;

    public Transform head;

    [HideInInspector]
    public bool hasWeapon;
    [HideInInspector]
    public bool inCombat, inSafeZone;
    [HideInInspector]
    public bool canMove;

    public float interactWaitTime;

    public AudioClip footStep;

    public Inventory playerInventory = new Inventory ();
    public InventoryCanvas inv_canvas;
    public GameObject pauseCanvas;

    public float detectionDistance;

    public List<Enemy> detectedEnemies = new List<Enemy> ();

    Coroutine actionRoutine;
    public CharacterStatistic attackCooldown;

    public GameObject weaponAttachPoint;

    public bool InCombat
    {
        get
        {
            return inCombat;
        }

        set
        {
            inCombat = value;
            if (value)
            {
                anim.SetFloat ("movementType", 1);
            }
            else
            {
                anim.SetFloat ("movementType", 0);
            }
        }
    }

    public bool InSafeZone
    {
        get
        {
            return inSafeZone;
        }

        set
        {
            inSafeZone = value;
            if (value)
            {
                anim.SetFloat ("movementType", 2);
            }
            else
            {
                anim.SetFloat ("movementType", 0);
            }
        }
    }

	[HideInInspector]
	public GameObject targetedEnemy;
	[Header("Movement Stats")]
	public float movementSpeed;
	public float turnSpeed;
	Coroutine movementRoutine;

	public Item_Type[] itemsToStartWith;
    private void Awake()
    {
        player = this;
        anim = GetComponent<Animator> ();
        audioSource = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody> ();
        playerStats.stats.onDeath += OnDeath;

    }
    // Use this for initialization
    void Start () {
        InCombat = false;
        canMove = true;
        
		hp_current = playerStats.stats.health.maximum;
		mp_current = playerStats.stats.mana.maximum;

        StartCoroutine (Detection ());
        Minibuffer.Register (this);

		foreach (Item_Type i in itemsToStartWith) {
			playerInventory.AddItem (new Item (i, 1));
		}
		//DEBUG
		EquipItem (playerInventory.FindItemInInventory ("Vampiric Spear").item_type);
    }

//    [Command]
//    public void DMG(float amount)
//    {
//		print ("Damage Function Called");
//		playerStats.stats.Damage(this.gameObject, playerStats.stats.health, amount);
//    }
    // Update is called once per frame
    void Update () {
        Cooldown ();
	} 

	void FixedUpdate () {
		DirectMovement ();
	}

#region helpers
    [Command]
    public void EnterCombat()
    {
        if (inCombat)
        {
            return;
        }
        ResetStateValues ();
        InCombat = true;
    }
    [Command]
    public void EnterSafeZone()
    {
        if (inSafeZone)
        {
            return;
        }
        ResetStateValues ();
        InSafeZone = true;
    }
    [Command]
    public void EnterDefault()
    {
        ResetStateValues ();
        anim.SetTrigger ("EnterDefault");
    }

    void ResetStateValues()
    {
        inSafeZone = false;
        inCombat = false;
    }

    void Unsheath()
    {
        anim.SetTrigger ("Unsheath");
    }

    void Sheath()
    {
        anim.SetTrigger ("Sheath");
    }

    void Cooldown()
    {
        if (attackCooldown.current > 0)
        {
            attackCooldown.current -= Time.deltaTime;
        }
    }

	public float DistanceToTargetedEnemy () {
		float dist = Vector3.Distance (transform.position, targetedEnemy.transform.position);
		return dist;
	}

	public void EquipItem (Item_Type i) {
		Transform wap = weaponAttachPoint.transform;
		while (wap.childCount > 0) {
			Destroy (wap.GetChild (0).gameObject);
		}
		GameObject wep = Instantiate (i.itemPrefab, wap);
	}
    #endregion

    public void Interact()
    {
        if (actionRoutine != null)
        {
            StopCoroutine (actionRoutine);
        }
        actionRoutine = StartCoroutine (InteractionProtocol ());
    }

    IEnumerator InteractionProtocol()
    {
		//Wait for an appropriate interval
//        GetComponent<Movement> ().StopMoving ();
        yield return new WaitForSeconds (interactWaitTime);
        canMove = false;
        anim.SetTrigger ("Interact");
        yield return new WaitForSeconds (1.5f);
        canMove = true;
        yield break;
    }



#region animation recievers
    public void FootL ()
    {
    }

    public void FootR ()
    {
    }

    public void Hit ()
    {

    }
    #endregion

#region combat
    IEnumerator Detection ()
    {
        while (true)
        {
            foreach (Enemy enemy in Enemy.allEnemies)
            {
                bool detectable = false;
                string err = "";
                if (Vector3.Distance (enemy.transform.position, transform.position) < detectionDistance)
                {
                    Ray ray = new Ray (head.transform.position, (enemy.transform.position - head.transform.position));
                    RaycastHit hit;
                    if (Physics.Raycast (ray, out hit, detectionDistance))
                    {
                        if (hit.transform.gameObject != enemy.gameObject)
                        {
                            err += "| Obstacle: " + hit.transform.name + " in way of " + enemy.name;
                        }
                        else
                        {
                            detectable = true;
                        }
                        
                        
                    }
                    else
                    {
                        detectable = true;
                    }
                }
                else
                {
                    err += "Enemy: " + enemy.name + " out of range";
                }

                if (detectable)
                {
                    if (!detectedEnemies.Contains (enemy))
                    {
                        detectedEnemies.Add (enemy);
                    }
					if (!enemy.seenByPlayer) {
						enemy.seenByPlayer = true;
					}
//                    if (!enemy.InCombat)
//                    {
//                        enemy.InCombat = true;
//                    }
                }
                else
                {
                    //print (err);
                }
            }

            if (detectedEnemies.Count > 0 && !inCombat)
            {
                InCombat = true;
            }

            if (detectedEnemies.Count == 0 && inCombat)
            {
                InCombat = false;
            }

            yield return new WaitForSeconds (0.5f);
        }
        
        yield break;
    }

	public void ProcCombatProtocol (Ability targetAbility)
    {
        if (actionRoutine != null && attackCooldown.current <= 0)
        {
            StopCoroutine (actionRoutine);
        }
        actionRoutine = StartCoroutine (CombatProtocol (targetAbility));
    }

	IEnumerator CombatProtocol (Ability targetAbility)
    {
		if (detectedEnemies.Count <= 0 || Vector3.Distance(transform.position, detectedEnemies[0].transform.position) > targetAbility.range)
        {
            yield break;
        }        
		//OnAttackArrive();

		bool b = targetAbility.ProcAbility(playerStats.stats, anim, detectedEnemies[0].gameObject, false);
        if (!b)
        {
            yield break;
        }
        float delay = targetAbility.damageDelay;
        if (targetAbility.delayByDistance)
        { 
            delay += Vector3.Distance(detectedEnemies[0].transform.position, transform.position) * 0.05f;
        }
        yield return new WaitForSeconds(delay);

		targetAbility.ApplyEffect(this.gameObject, detectedEnemies[0].gameObject);
        
        attackCooldown.current = attackCooldown.maximum;
        yield break;
    }

	public void ProcApplyStatChange (CharacterStatistic c, float power, float duration) {
		StartCoroutine (ApplyStatChange (c, power, duration));
	}

	IEnumerator ApplyStatChange (CharacterStatistic c, float power, float duration) {
		c.points += power;
		yield return new WaitForSeconds (duration);
		c.points -= power;
		yield break;
	}

	public void ProcStatus (AbilityEffect.Status a, float power, float duration, List<GameObject> targets) {
		StartCoroutine (ApplyStatus(a, power, duration, targets));
	}

	IEnumerator ApplyStatus (AbilityEffect.Status a, float power, float duration, List<GameObject> targets) {

		List<CharacterStatistics> targets_c = new List<CharacterStatistics> ();
		foreach (GameObject target in targets) {
			targets_c.Add (target.GetComponent<CharacterStatistics> ());
		}

		float t = 0;
		switch (a) {
		case AbilityEffect.Status.Ignite:
			foreach (CharacterStatistics c in targets_c) {
				if (c.firePrefab != null) {
					c.firePrefab.SetActive (true);
				}
			}

			while (t < duration) {
				t += Time.deltaTime;
				foreach (CharacterStatistics c in targets_c) {
					c.Damage (targets[targets_c.IndexOf(c)],c.health, power * Time.deltaTime);
				}

				yield return null;
			}

			foreach (CharacterStatistics c in targets_c) {
				if (c.firePrefab != null) {
					c.firePrefab.SetActive (false);
				}
			}

			break;
		}
		yield break;
	}

    void OnAttackArrive ()
    {

    }

	public void Damage (float amount) {
		hp_current -= amount;
		if (hp_current > 0) {
			OnHit ();
		} else {
			OnDeath ();
		}
	}

	public void OnHit () {
		
	}

	void OnDeath ()
	{

	}
		
#endregion


		
    public void EquipItemOnCharacter (GameObject template, UIEquipmentType equipType)
    {
        switch (equipType)
        {
            case UIEquipmentType.Weapon_MainHand:
                foreach (Transform t in weaponAttachPoint.transform)
                {
                    GameObject.Destroy(t.gameObject);
                }
                if (template != null)
                {
                    Instantiate(template, weaponAttachPoint.transform);
                    template.transform.localPosition = Vector3.zero;
                }
                break;
        }
    }

	#region Movement

	void DirectMovement () {
		Vector2 directionalInput = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
//		anim.SetFloat ("movementSpeed", directionalInput.y);
		anim.SetFloat ("forwardSpeed", directionalInput.y);
		rb.MovePosition (transform.position + transform.forward * directionalInput.y * movementSpeed * Time.deltaTime);
		rb.MoveRotation (transform.rotation * Quaternion.Euler (0, directionalInput.x * turnSpeed * Time.deltaTime, 0));
	}

	void ProcMoveToPoint () {
		if (movementRoutine != null) {
			StopCoroutine (movementRoutine);
		}
	}

	IEnumerator MoveToPoint () {
		yield break;
	}

	#endregion
}

