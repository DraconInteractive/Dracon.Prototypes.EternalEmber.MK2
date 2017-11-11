using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistence : MonoBehaviour {

	public static List<Persistence> saveableObjects = new List<Persistence>();

	public void Awake () {
		saveableObjects.Add (this);
	}

	public virtual void Save () {
		
	}

	public virtual void Load () {
		
	}
}
