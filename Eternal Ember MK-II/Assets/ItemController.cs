using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Controller", menuName = "Generate/Item Controller", order = 0)]
public class ItemController : ScriptableObject
{ 
	public List<Item_Type> allItems = new List<Item_Type> ();
	public List<Item> itemEquiv = new List<Item> ();
	public Item_Type searchedItem = null;

	public void SearchForItem (string search)
	{
		Item_Type it = null;
		foreach (Item_Type i in allItems)
		{
			if (i.itemName == search)
			{
				it = i;
				break;
			}
		}
		if (it != null)
		{
			searchedItem = it;
		}
		else
		{
			searchedItem = null;
		}
		//searchedItem = new Item ("null", null, null, Item.ItemType.Consumable);
	}
}

