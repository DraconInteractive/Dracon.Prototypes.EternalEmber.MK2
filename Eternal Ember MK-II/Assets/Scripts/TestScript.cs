using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        List<Transform> t = new List<Transform>();
        foreach (Transform child in gameObject.transform)
        {
            t.Add(child);
            Debug.Log(child.name);
        }
        Debug.Log("Finished naming children");
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
