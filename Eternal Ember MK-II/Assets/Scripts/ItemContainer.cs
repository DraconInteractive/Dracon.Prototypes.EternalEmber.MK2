using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;

public class ItemContainer : MonoBehaviour
{
    public List<Item_Type> itemsInContainer;
	public bool interactable;

    public void AddToInventory ()
    {
        Inventory p_Inventory = Player.player.playerInventory;
        print ("There are " + itemsInContainer.Count + " items in container");
		foreach (Item_Type item in itemsInContainer)
        {
			p_Inventory.AddItem(new Item(item, 1));
        }
		InventoryCanvas.invCanvas.PopulateInventorySlots ();
		if (interactable) {
			GetComponent<Interactable>().uiWindow.Hide();
		}
        
        Destroy(this);
    }

	public void AddToInventory (List<Item_Type> items) {
		Inventory p_Inventory = Player.player.playerInventory;
		print ("There are " + items.Count + " items in container");
		foreach (Item_Type item in items)
		{
			p_Inventory.AddItem(new Item(item, 1));
		}
		InventoryCanvas.invCanvas.PopulateInventorySlots ();
		if (interactable) {
			GetComponent<Interactable>().uiWindow.Hide();
		}

		Destroy(this);
	}

	public void AddToInventory (Dictionary<Item_Type, int> items) {
		Inventory p_Inventory = Player.player.playerInventory;
		print ("There are " + items.Keys.Count + " items in container");
//		foreach (Item_Type item in items)
//		{
//			p_Inventory.AddItem(new Item(item, 1));
//		}
		foreach (KeyValuePair <Item_Type, int> pair in items) {
			p_Inventory.AddItem (new Item (pair.Key, pair.Value));
		}
		InventoryCanvas.invCanvas.PopulateInventorySlots ();
		if (interactable) {
			GetComponent<Interactable>().uiWindow.Hide();
		}

		Destroy(this);
	}
}


