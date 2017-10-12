using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotTypeAssociate : MonoBehaviour {
	public Item_Type assocItem;

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
}
