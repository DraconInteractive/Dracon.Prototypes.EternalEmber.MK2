using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbilityManager))]
public class AbilityManager_Editor : Editor {

	public override void OnInspectorGUI () {
		DrawDefaultInspector ();

		AbilityManager a = (AbilityManager)target;
		if (GUILayout.Button("Assign ID's")) {
			a.AssignIDs ();
		}
	}
}
