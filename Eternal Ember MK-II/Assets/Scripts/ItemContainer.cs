using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;

public class ItemContainer : MonoBehaviour
{
    public List<Item_Type> itemsInContainer;
    public List<Item> itemEquiv = new List<Item> ();
    // Use this for initialization
    void Start()
    {
        foreach (Item_Type i in itemsInContainer)
        {
            itemEquiv.Add (new Item (i, 1));
        }
    }

    public void AddToInventory ()
    {
        Inventory p_Inventory = Player.player.playerInventory;
        print ("There are " + itemsInContainer.Count + " items in container");
        foreach (Item item in itemEquiv)
        {
            p_Inventory.AddItem(item);
        }
		InventoryCanvas.invCanvas.PopulateInventorySlots ();
        GetComponent<Interactable>().uiWindow.Hide();
        Destroy(this);
    }
}


