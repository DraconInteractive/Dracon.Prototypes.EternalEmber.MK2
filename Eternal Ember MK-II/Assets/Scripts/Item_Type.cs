using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "Generate/Item", order = 0)]
public class Item_Type : ScriptableObject
{
    public string itemName;
    public string itemDesc;

    public Sprite itemSprite;

    public int reqLevel;
    public UIItemQuality itemQuality;
    public UIEquipmentType itemEquipType;
    public List<CharacterAttribute> attributes = new List<CharacterAttribute> ();

    public enum ItemType { Weapon, Consumable, Rune, Currency, Shield, Armour, Material, Book};
    public ItemType i_type;

    public GameObject itemPrefab;

    public int uiItemType;
    GameObject itemObject;

    public ItemController[] controllers;

    public bool questItem;

    public Recipe itemRecipe;

	public string cost;

	public Vector3 convertedCost;


	public Vector3 handPosOffset, handRotOffset;
    public void Sort ()
    {
        foreach (ItemController control in controllers)
        {
            if (!control.allItems.Contains(this))
            {
                control.allItems.Add (this);
                control.itemEquiv.Add (new Item (this));
            }
        }

        Debug.Log ("Done sorting: " + itemName);
    }

	public void SetCost () {
		//int hexCost = int.Parse (cost, System.Globalization.NumberStyles.HexNumber);
		float copper = 0;
		float silver = 0;
		float gold = 0;

		string s;
		char[] chars;

		//s = cost.ToString ();
		chars = cost.ToCharArray ();

		copper = int.Parse (chars[chars.Length - 2].ToString() + chars [chars.Length - 1].ToString());
		silver = int.Parse (chars [chars.Length - 4].ToString () + chars [chars.Length - 3].ToString ());
		gold = int.Parse (chars [chars.Length - 6].ToString () + chars [chars.Length - 5].ToString ());
		//copper = int.Parse (chars [index].ToString ());
		convertedCost = new Vector3 (gold,silver,copper);
	}

    public static UIItemInfo GetInfoFromItem (Item_Type item)
    {
        UIItemInfo info = new UIItemInfo();
        info.Name = item.itemName;
        info.Description = item.itemDesc;
        info.Icon = item.itemSprite;
        info.Quality = item.itemQuality;
        info.EquipType = item.itemEquipType;
        info.Type = item.i_type.ToString();

        foreach (CharacterAttribute att in item.attributes)
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

        info.RequiredLevel = item.reqLevel;
        info.ItemType = item.uiItemType;
		info.cost = item.convertedCost;
        return info;
    }
}

[Serializable]
public class CharacterAttribute
{
    public enum AttributeType { Strength, Dexterity, Consitution, Wisdom, Intelligence, Charisma, Damage, Health, Armour, Stamina, AttackSpeed, Block};
    public AttributeType ca_type;
    public float amount;
}


[Serializable]
public class Item
{
    public Item_Type item_type;

    public string itemName;
    public int itemQuantity;

    public bool inCharacter = false;

    public Item (Item_Type template, int quantity)
    {
        item_type = template;
        itemName = template.itemName;
        itemQuantity = quantity;
    }

    public Item (Item_Type template)
    {
        item_type = template;
        itemName = template.itemName;
        itemQuantity = 0;
    }
}

[Serializable]
public class Inventory
{
    public List<Item> items;
    public int sizeLimit;
    public bool AddItem (Item i)
    {
        Item item = FindItemInInventory (i.item_type);
        if (item != null)
        {
            Debug.Log ("Item found in inventory. Increasing quantity by " + i.itemQuantity);
            item.itemQuantity += i.itemQuantity;
            return true;
        }
        else
        {
            if (items.Count < sizeLimit && i.itemQuantity > 0)
            {
                items.Add (i);
                return true;
            } 
            else
            {
                return false;
            }
        }
    }

    public bool RemoveItem(Item i, int quantityToRemove)
    {
        bool result = false;
        Item item = FindItemInInventory (i.item_type);
        if (item != null)
        {
            if (item.itemQuantity >= quantityToRemove)
            {
                item.itemQuantity -= quantityToRemove;
                result = true;
            }
            else
            {
                Debug.Log ("Insufficient item quantity");
                item.itemQuantity = 0;
                result = false;
            }

            if (item.itemQuantity == 0)
            {
                items.Remove (item);
            }

            return result;
        }
        else
        {
            Debug.Log ("Item not available");
            return false;
        }
    }

    public Item FindItemInInventory (Item_Type i)
    {
        foreach (Item item in items)
        {
            if (item.item_type == i)
            {
                return item;
            }
        }

        return null;
    }

	public Item FindItemInInventory (string s) {
		foreach (Item item in items) {
			if (item.itemName == s) {
				return item;
			}
		}

		return null;
	}

}




