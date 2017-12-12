using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivationVolume : MonoBehaviour {

	public List<Enemy> allEnemies = new List<Enemy>();

	void Start () {
		foreach (Enemy e in allEnemies) {
			e.canAttack = false;
		}
	}
	void OnTriggerEnter (Collider col) {
		if (col.GetComponent<Player>()) {
			foreach (Enemy e in allEnemies) {
				e.canAttack = true;
			}
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.GetComponent<Player>()) {
			foreach (Enemy e in allEnemies) {
				e.canAttack = false;
			}
		}
	}
}
