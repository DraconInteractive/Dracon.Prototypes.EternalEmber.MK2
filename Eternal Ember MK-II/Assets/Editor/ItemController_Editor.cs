using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor (typeof (ItemController))]
public class ItemController_Editor : Editor
{
    Item_Type searchedItem = null;
    string searchTerm = "";

    //Testing
    bool[] showItemSlots;
    SerializedProperty itemProperty;
    private void OnEnable()
    {
        ItemController myTarget = (ItemController)target;
        showItemSlots = new bool[myTarget.itemEquiv.Count];
        itemProperty = serializedObject.FindProperty ("itemEquiv");
    }
    public override void OnInspectorGUI()
    {
       
        DrawDefaultInspector ();
        serializedObject.Update ();
        ItemController myTarget = (ItemController)target;

        EditorGUILayout.LabelField ("______________________________");
        EditorGUILayout.LabelField ("Search for Item");
        
        searchTerm = EditorGUILayout.TextField (searchTerm);

        GUI.color = Color.green;
        if (GUILayout.Button ("Search"))
        {
            myTarget.SearchForItem (searchTerm);
        }
        GUI.color = Color.white;
        EditorGUILayout.LabelField ("______________________________");
        for (int i = 0; i < myTarget.itemEquiv.Count; i++)
        {
            ItemSlotGUI (i);
        }
        serializedObject.ApplyModifiedProperties ();
    }

    private void ItemSlotGUI(int index)
    {
        EditorGUILayout.BeginVertical (GUI.skin.box);
        EditorGUI.indentLevel++;

        showItemSlots[index] = EditorGUILayout.Foldout (showItemSlots[index], "Item: " + index);
        if (showItemSlots[index])
        {
            EditorGUILayout.PropertyField (itemProperty.GetArrayElementAtIndex (index));
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical ();
    }

    //Testing
    /* for (int i = 0; i<ItemController.allItems.Count; i++)
     {
         ItemSlotGUI(i);*/
}
/*
private bool[] showItemSlots = new bool[ItemController.allItems.Count];

private SerializedProperty itemsProperty;
private const string inventoryPropItemsName = "allItems";

private void OnEnable()
{
    itemsProperty = serializedObject.FindProperty (inventoryPropItemsName);
}
private void ItemSlotGUI(int index)
{
    EditorGUILayout.BeginVertical (GUI.skin.box);
    EditorGUI.indentLevel++;

    showItemSlots[index] = EditorGUILayout.Foldout (showItemSlots[index], "Item slot " + index);
    if (showItemSlots[index])
    {
        //EditorGUILayout.PropertyField (itemImagesProperty.GetArrayElementAtIndex (index));
        EditorGUILayout.PropertyField (itemsProperty.GetArrayElementAtIndex (index));
    }
    EditorGUI.indentLevel--;
    EditorGUILayout.EndVertical ();
}*/


[CustomEditor (typeof(Item_Type))]
public class Item_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector ();
        serializedObject.Update ();

        Item_Type myTarget = (Item_Type)target;

        EditorGUILayout.LabelField ("______________________________");
        EditorGUILayout.LabelField ("Sort into controllers");

        GUI.color = Color.green;
        if (GUILayout.Button("Sort"))
        {
            myTarget.Sort ();
        }
		if (GUILayout.Button("Set Cost")) {
			myTarget.SetCost ();
		}

        serializedObject.ApplyModifiedProperties ();
    }

   
}



