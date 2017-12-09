using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ItemContainerAdvanced {
	public List<Item_Type> itemsInContainer = new List<Item_Type>();
	public Dictionary <Item_Type, ItemCount> itemDictionary;
	public List<ItemCount> quantities = new List<ItemCount>();
	public bool interactable;
	// Use this for initialization
	void Start () {
		itemDictionary = new Dictionary<Item_Type, ItemCount> ();
		for (int i = 0; i < itemsInContainer.Count; i++) {
			itemDictionary.Add (itemsInContainer [i], quantities [i]);
		}
	}
	public void AddToInventory (GameObject target)
	{
		Inventory p_Inventory = Player.player.playerInventory;

		foreach (Item_Type item in itemsInContainer)
		{
			p_Inventory.AddItem(new Item(item, 1));
		}
		InventoryCanvas.invCanvas.PopulateInventorySlots ();
		if (interactable) {
			target.GetComponent<Interactable>().uiWindow.Hide();
		}
	}

	public void AddToInventory (GameObject target, List<Item_Type> items) {
		Inventory p_Inventory = Player.player.playerInventory;
		foreach (Item_Type item in items)
		{
			p_Inventory.AddItem(new Item(item, 1));
		}
		InventoryCanvas.invCanvas.PopulateInventorySlots ();
		if (interactable) {
			target.GetComponent<Interactable>().uiWindow.Hide();
		}
	}

	public void AddToInventory (GameObject target, Dictionary<Item_Type, int> items) {
		Inventory p_Inventory = Player.player.playerInventory;
		//		foreach (Item_Type item in items)
		//		{
		//			p_Inventory.AddItem(new Item(item, 1));
		//		}
		foreach (KeyValuePair <Item_Type, int> pair in items) {
			p_Inventory.AddItem (new Item (pair.Key, pair.Value));
		}
		InventoryCanvas.invCanvas.PopulateInventorySlots ();
		if (interactable) {
			target.GetComponent<Interactable>().uiWindow.Hide();
		}
	}
}
[Serializable]
public class ItemCount {
	public enum CountType {SingleNumber, RandomNumber, NumberRange, RandomChoice};
	public CountType count;

	public int single;
	public int rangeMin;
	public int rangeMax;
	public List<int> choices = new List<int>();

	public int GetResult () {
		int res = 0;
		switch (count)
		{
		case CountType.SingleNumber:
			res = single;
			break;
		case CountType.RandomNumber:
			res = UnityEngine.Random.Range (0, 100);
			break;
		case CountType.NumberRange:
			res = UnityEngine.Random.Range (rangeMin, rangeMax);
			break;
		case CountType.RandomChoice:
			int r = UnityEngine.Random.Range (0, choices.Count);
			res = choices [r];
			break;
		}

		return res;
	}
}
