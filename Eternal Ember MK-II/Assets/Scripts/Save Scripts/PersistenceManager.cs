using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceManager : MonoBehaviour {

	void Start () {
		Invoke ("Initiate", 0.5f);
	}

	void Initiate () {
		if (PlayerPrefs.GetInt("HasSave") == 1) {
			Load ();
		}
	}
	public void Save () {
		foreach (Persistence p in Persistence.saveableObjects) {
			p.Save ();
		}
	}

	public void Load () {
		foreach (Persistence p in Persistence.saveableObjects) {
			p.Load ();
		}
	}
}
