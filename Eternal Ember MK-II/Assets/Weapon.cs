using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public Item_Type assocItem;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = assocItem.handPosOffset;
		transform.localRotation = Quaternion.Euler (assocItem.handRotOffset);
	}
}
