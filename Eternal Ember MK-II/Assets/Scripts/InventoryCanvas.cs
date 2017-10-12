using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;

public class InventoryCanvas : MonoBehaviour {

    public Player player;

    public UIWindow invWindow;
	// Use this for initialization
	void Start () {
        //gameObject.SetActive (false);
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
       
    }
   
    public void ToggleInventory ()
    {
        invWindow.Toggle();
        if (invWindow.IsOpen)
        {
            PopulateInventorySlots();
        }
    }

	public void ToggleInventory (bool state) {
		if (state == invWindow.IsOpen) {
			return;
		}
		if (state) {
			invWindow.Show ();
			PopulateInventorySlots ();
		} else {
			invWindow.Hide ();
		}
	}

    public void PopulateInventorySlots ()
    {
        print("Populating Slots");
        UIItemSlot[] invSlots = invWindow.gameObject.GetComponentsInChildren<UIItemSlot>();

        foreach (UIItemSlot slot in invSlots)
        {
            slot.Unassign();
        }


        Inventory p_Inventory = player.playerInventory;
        int counter = 0;
        string slotPop = "";
        foreach (Item i in p_Inventory.items)
        {
            slotPop += ("Slotpop: " + counter + "|" + i.itemName + "\n");
            UIItemInfo info = new UIItemInfo();
            info.Name = i.itemName;
            info.Description = i.item_type.itemDesc;
            info.Icon = i.item_type.itemSprite;
            info.Quality = i.item_type.itemQuality;
            info.EquipType = i.item_type.itemEquipType;
            info.Type = i.item_type.i_type.ToString();

            foreach (CharacterAttribute att in i.item_type.attributes)
            {
                
                switch (att.ca_type)
                {
                    case CharacterAttribute.AttributeType.Damage:
                        info.Damage = (int)att.amount;
                        break;
                    case CharacterAttribute.AttributeType.Armour:
                        info.Armor = (int)att.amount;
                        break;
                    case CharacterAttribute.AttributeType.AttackSpeed:
                        info.AttackSpeed = att.amount;
                        break;
                    case CharacterAttribute.AttributeType.Block:
                        info.Block = (int)att.amount;
                        break;
                    default:
                        info.ItemAttributes.Add(att.ca_type.ToString(), att.amount);
                        break;
                }
            }

            info.RequiredLevel = i.item_type.reqLevel;
            info.ItemType = i.item_type.uiItemType;
            invSlots[counter].Assign(info);
            invSlots[counter].transform.GetChild(3).GetComponent<Text>().text = i.itemQuantity.ToString();
            counter++;
        }
        print(slotPop);
        foreach (UIItemSlot slot in invSlots)
        {
            if (!slot.IsAssigned())
            {
                slot.transform.GetChild(3).GetComponent<Text>().text = "-";
            }
        }
    }
}
