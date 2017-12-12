using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(StatisticsProfile))]
public class StatisticsProfileEditor : Editor {

	public override void OnInspectorGUI () {
		DrawDefaultInspector ();

		var myTarget = (StatisticsProfile)target;

		EditorGUILayout.LabelField ("______________________________");
		EditorGUILayout.LabelField ("Calculate Resultant Statistics");

		GUI.color = Color.green;
		if (GUILayout.Button ("Calculate"))
		{
			myTarget.stats.SetupStatistics ();
		}
	}
}
