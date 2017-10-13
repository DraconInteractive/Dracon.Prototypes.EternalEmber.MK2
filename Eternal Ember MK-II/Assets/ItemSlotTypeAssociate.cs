using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;

public class ItemSlotTypeAssociate : MonoBehaviour {
	public Item_Type assocItem;

	[Tooltip("If Applicable")]
	public ItemContainer itemContainer;

	public void BuyItem() {
		//check for money, and if item adds, subtract money;
		int playerMoney = int.Parse(CharacterStatistics.playerStatistics.currentMoney);
		int itemCost = int.Parse(assocItem.cost);

		print ("PM: " + playerMoney + " IC: " + itemCost + "DIFF: " + (playerMoney - itemCost).ToString());

		if (playerMoney > itemCost) {
			if (Player.player.playerInventory.AddItem (new Item (assocItem, 1))) {
				playerMoney -= itemCost;

				print ("Buy Successful");

			}
		}
		InventoryCanvas.invCanvas.PopulateInventorySlots ();
		print ("End Buy");
	}

	public void AddItemToInventory () {
		if (Player.player.playerInventory.AddItem(new Item(assocItem, 1))) {
			itemContainer.itemsInContainer.Remove (assocItem);
			itemContainer.GetComponent<Interactable> ().RefreshLootWindow ();
			InventoryCanvas.invCanvas.PopulateInventorySlots ();
		}

		print ("Items left: " + itemContainer.itemsInContainer.Count);
		if (itemContainer.itemsInContainer.Count == 0) {
			print ("No more items left");
			transform.parent.parent.parent.GetComponent<UIWindow> ().Hide();
		}
	}
}
