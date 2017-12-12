using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;

public class CharacterCanvas : MonoBehaviour {

    public UIWindow characterWindow;
    public Text healthValueText, manaValueText, staminaText;
    public Text strengthText, dexterityText, consitutionText, intelligenceText, wisdomText;
    public Text damageText, attackSpeedText, dpsText, dodgeText;
    public Text armourText, durabilityText, moveSpeedMultText;
    public Text levelText, pointsText, expText;
    public UIProgressBar weaponCraftBar, armourCraftBar, repairBar, runeCraftBar;
    CharacterStatistics stats;
	// Use this for initialization
	void Start () {
        stats = Player.player.playerStats;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.C))
        {
            characterWindow.Toggle();
        }
	}

    public void CharacterItemAssigned (UIEquipSlot slot)
    {
        UIItemInfo info = slot.GetItemInfo();
        Inventory p_inv = Player.player.playerInventory;
        Item targetItem = null;
        foreach (Item i in p_inv.items)
        {
            if (i.itemName == info.Name)
            {
                targetItem = i;
            }
        }

        if (targetItem == null)
        {
            Debug.LogError("Item assigned that isnt in inventory bruh. \n Item designation: " + info.Name);
            return;
        }

        targetItem.inCharacter = true;

        GameObject prefab = targetItem.item_type.itemPrefab;
        if (prefab == null)
        {
            return;
        }

        switch (slot.equipType)
        {
            case UIEquipmentType.Weapon_MainHand:
                Player.player.EquipItemOnCharacter(targetItem.item_type.itemPrefab, targetItem.item_type.itemEquipType);
                break;
            case UIEquipmentType.Weapon_OffHand:
                break;
        }
    }

    public void CharacterItemUnassigned (UIEquipSlot slot)
    {

        UIItemInfo info = slot.GetItemInfo();

        if (info == null)
        {
            return;
        }

        Inventory p_inv = Player.player.playerInventory;
        Item targetItem = null;
        foreach (Item i in p_inv.items)
        {
            if (i.itemName == info.Name)
            {
                targetItem = i;
            }
        }

        if (targetItem == null)
        {
            Debug.LogError("Item assigned that isnt in inventory bruh. \n Item designation: " + info.Name);
            return;
        }

        targetItem.inCharacter = false;
    }

    public void OnOpen ()
    {
        stats.CalculateResultants();
        healthValueText.text = stats.health.maximum.ToString();
        manaValueText.text = stats.mana.maximum.ToString();
        staminaText.text = stats.stamina.maximum.ToString();

        strengthText.text = stats.strength.current.ToString();
        dexterityText.text = stats.dexterity.current.ToString();
        consitutionText.text = stats.constitution.current.ToString();
        intelligenceText.text = stats.intelligence.current.ToString();
        wisdomText.text = stats.wisdom.current.ToString();

        Inventory inventory = Player.player.playerInventory;
        Item weapon = null;
        foreach (Item i in inventory.items)
        {
            if (i.inCharacter && i.item_type.itemEquipType == UIEquipmentType.Weapon_MainHand)
            {
                weapon = i;
            }
        }
        float damage = 0;
        float atkSpeed = 1;
        if (weapon != null)
        {
            foreach (CharacterAttribute c in weapon.item_type.attributes)
            {
                if (c.ca_type == CharacterAttribute.AttributeType.Damage)
                {
                    damage = c.amount;
                }
                else if (c.ca_type == CharacterAttribute.AttributeType.AttackSpeed)
                {
                    atkSpeed = c.amount;
                }
            }
        }

        damage += stats.baseMeleeDamage.current;
        
        damageText.text = damage.ToString();
        attackSpeedText.text = atkSpeed.ToString();
        float dps = damage * atkSpeed;
        dpsText.text = dps.ToString();
        dodgeText.text = stats.dodgeChance.current.ToString();

        moveSpeedMultText.text = stats.moveSpeedMultiplier.current.ToString();
        levelText.text = stats.level.current.ToString();
        expText.text = stats.level.current.ToString();
        pointsText.text = stats.talentPoints.current.ToString();

        repairBar.fillAmount = stats.repairSkill.current / 100;
        
    }
}
