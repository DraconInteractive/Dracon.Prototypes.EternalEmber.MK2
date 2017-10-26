using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndRotate : MonoBehaviour {

	public Vector3 move, rotate;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += move * Time.deltaTime;
		transform.rotation *= Quaternion.Euler (rotate * Time.deltaTime);
	}
}
