using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;

public class CraftingCanvas : MonoBehaviour {

    public UIWindow window;
    public GameObject rc_weapon, rc_armour, rc_rune, rc_material;
    public GameObject recipePrefab, recipeSlotPrefab, recipeSlotSpacer;

    public List<Recipe> allRecipes = new List<Recipe>();
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown(KeyCode.V))
  //      {
  //          window.Toggle();
  //      }
	}

    public void OnWindowOpen ()
    {
        foreach (Transform t in rc_weapon.transform)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in rc_armour.transform)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in rc_rune.transform)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in rc_material.transform)
        {
            Destroy(t.gameObject);
        }


        foreach (Recipe r in allRecipes)
        {
            Transform r_parent = null;

            switch (r.result.i_type)
            {
                case Item_Type.ItemType.Weapon:
                    r_parent = rc_weapon.transform;
                    break;
                case Item_Type.ItemType.Shield:
                    r_parent = rc_weapon.transform;
                    break;
                case Item_Type.ItemType.Armour:
                    r_parent = rc_armour.transform;
                    break;
                case Item_Type.ItemType.Rune:
                    r_parent = rc_rune.transform;
                    break;
                case Item_Type.ItemType.Material:
                    r_parent = rc_material.transform;
                    break;
            }
            GameObject instRecipe = Instantiate(recipePrefab, r_parent);
            //start experimental
            UIItemInfo info = Item_Type.GetInfoFromItem(r.result);
            //end experimental
            instRecipe.transform.GetChild(0).GetComponent<UIItemSlot>().Assign(info);
            UIRecipeManager manager = instRecipe.GetComponent<UIRecipeManager>();
            manager.assocRecipe = r;
            manager.titleText.text = r.recipeName;
            manager.descText.text = r.result.itemDesc;
            //manager.craftButton.onClick.AddListener(() => CraftItem(r.result, r));
            foreach (RecipeMaterial rm in r.materials)
            {
                GameObject slot = Instantiate(recipeSlotPrefab, manager.materialSlot.transform);
                UIItemInfo rmInfo = Item_Type.GetInfoFromItem(rm.material);
                slot.GetComponent<UIItemSlot>().Assign(rmInfo);
                slot.transform.GetChild(4).GetComponent<Text>().text = "x " + rm.amount;
            }
            GameObject spacer = Instantiate(recipeSlotSpacer, manager.materialSlot.transform);
        }
    }

    public void TryCraft ()
    {
        print("TryCraft");
        bool result = CraftItem();
    }
    public bool CraftItem ()
    {
        print("Begin Craft");
        if (UIRecipeManager.selectedRecipe == null)
        {
            print("No Selected Recipe");
            return false;
        }
        //Retrieve recipe and inventory
        Recipe r = UIRecipeManager.selectedRecipe.assocRecipe;
        Inventory inv = Player.player.playerInventory;
        //Create bool for checks
        bool sufficientMaterials = true;
        //Check if inventory contains all requisite materials and amounts.
        foreach (RecipeMaterial rm in r.materials)
        {
            Item retrieved = inv.FindItemInInventory(rm.material);
            if (retrieved != null)
            {
                if (retrieved.itemQuantity < rm.amount)
                {
                    print("Not enough " + rm.material.itemName);
                    sufficientMaterials = false;
                }
                else
                {
                    print("Positive amount of " + rm.material.itemName);
                }
            } 
            else
            {
                print(rm.material.itemName + " Missing from inventory");
                sufficientMaterials = false;
            }
            
        }
        
        if (sufficientMaterials)
        {
            print("Passed overall material check");
            if (inv.items.Count < inv.sizeLimit)
            {
                print("Passed inv size check");
                foreach (RecipeMaterial rm in r.materials)
                {
                    Item retrieved = inv.FindItemInInventory(rm.material);
                    inv.RemoveItem(retrieved, rm.amount);
                }
                print("Creating Item");
                inv.AddItem(new Item(r.result, 1));
            }
            else
            {
                print("No space in inventory");
                sufficientMaterials = false;
            }
        }

        return sufficientMaterials;
    }
}
