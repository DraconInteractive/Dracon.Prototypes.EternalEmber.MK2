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
}

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



