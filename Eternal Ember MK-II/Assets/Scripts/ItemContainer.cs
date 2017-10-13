using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;

public class ItemContainer : MonoBehaviour
{
    public List<Item_Type> itemsInContainer;

    public void AddToInventory ()
    {
        Inventory p_Inventory = Player.player.playerInventory;
        print ("There are " + itemsInContainer.Count + " items in container");
		foreach (Item_Type item in itemsInContainer)
        {
			p_Inventory.AddItem(new Item(item, 1));
        }
		InventoryCanvas.invCanvas.PopulateInventorySlots ();
        GetComponent<Interactable>().uiWindow.Hide();
        Destroy(this);
    }
}


