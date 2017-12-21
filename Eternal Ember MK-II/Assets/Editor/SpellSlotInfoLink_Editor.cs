using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SpellSlotInfoLink))]
public class SpellSlotInfoLink_Editor : Editor {

	public override void OnInspectorGUI () {
		DrawDefaultInspector();
		var myTarget = (SpellSlotInfoLink)target;
		GUI.color = Color.green;
		if (GUILayout.Button("Get Values")) {
			myTarget.GetValues ();
		}
	}
}
