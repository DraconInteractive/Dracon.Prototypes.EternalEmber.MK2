using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatistics : MonoBehaviour {
    public static CharacterStatistics playerStatistics;
    //Level
    public CharacterStatistic level;
    public CharacterStatistic experience;
    public CharacterStatistic talentPoints;
    //Base statistics
    public CharacterStatistic b_health, b_stamina, b_mana;
    public CharacterStatistic b_strength, b_dexterity, b_constitution, b_wisdom, b_intelligence, b_charisma;
    //Resultant simple statistics
    public CharacterStatistic health, stamina, mana;
    [HideInInspector]
    public CharacterStatistic strength, dexterity, constitution, wisdom, intelligence, charisma;
    //Resultant combat statistics
    [HideInInspector]
    public CharacterStatistic baseMeleeDamage, baseMagicDamage;
    [HideInInspector]
    public CharacterStatistic meleeResist, magicResist;
    [HideInInspector]
    public CharacterStatistic dodgeChance;
    //Resultant misc statistic
    [HideInInspector]
    public CharacterStatistic persuasion;
    [HideInInspector]
    public CharacterStatistic moveSpeedMultiplier;
    //Crafting statistics
    public CharacterStatistic baseCraftingSkill;
    public CharacterStatistic b_repairSkill, b_armourCraftSkill, b_weaponCraftSkill, b_runeCraftSkill;
    [HideInInspector]
    public CharacterStatistic repairSkill, armourCraftSkill, weaponCraftSkill, runeCraftSkill;
    [HideInInspector]
    public CharacterStatistic collectionSkill;
	public string currentMoney = "000000";
    public delegate void Die();
    public Die onDeath;


    private void Awake()
    {
        if (GetComponent<Player>())
        {
            playerStatistics = this;
        }
    }
    private void Start()
    {
        CalculateResultants();
        StatisticInitiate ();

		//DEBUG:
		currentMoney = "202020";
    }

    private void Update()
    {
        if (mana.current < mana.maximum)
        {
            Damage(mana, Time.deltaTime);
        } 
    }

    public void StatisticInitiate ()
    {
        health.current = health.maximum;
        stamina.current = stamina.maximum;
        mana.current = mana.maximum;
    }
    
    public void Damage (CharacterStatistic stat, float amount)
    {
        
        stat.current += amount;
        if (stat == health && stat.current <= 0)
        {
            onDeath ();
        }
        stat.current = Mathf.Clamp (stat.current, 0, stat.maximum);

        if (gameObject == Player.player.gameObject)
        {
            UnitFrame.playerFrame.UpdateFrame();
        }
    }


    public void ChangeHSM_Max (CharacterStatistic stat, float amount)
    {
        stat.maximum += amount;
        stat.maximum = Mathf.Clamp (stat.maximum, 0, Mathf.Infinity);
    }

    public void CalculateResultants ()
    {
        experience.maximum = level.current * 50;
        while (experience.current > experience.maximum)
        {
            LevelUp();
        }

        health.maximum = b_health.maximum + (level.current * constitution.current * (strength.current * 0.5f));
        stamina.maximum = b_stamina.maximum + (level.current * dexterity.current * (strength.current * 0.5f));
        mana.maximum = b_mana.maximum + (level.current * wisdom.current * (intelligence.current * 0.5f));

        strength.current = b_strength.current + strength.points;
        dexterity.current = b_dexterity.current + dexterity.points;
        constitution.current = b_constitution.current + constitution.points;
        wisdom.current = b_wisdom.current + wisdom.points;
        intelligence.current = b_intelligence.current + intelligence.points;
        charisma.current = b_charisma.current + charisma.points;

        baseMeleeDamage.current = level.current * (strength.current * 1.5f) * (dexterity.current * 0.5f);
        baseMagicDamage.current = level.current * wisdom.current * (intelligence.current * 0.5f) + (charisma.current * 0.5f);

        meleeResist.current = (level.current * dexterity.current * constitution.current) * 0.1f;
        magicResist.current = (level.current * wisdom.current * intelligence.current) * 0.1f;

        dodgeChance.current = (level.current * dexterity.current * (0.5f * intelligence.current));

        persuasion.current = level.current * charisma.current * (strength.current * intelligence.current) * 0.5f;
        moveSpeedMultiplier.current = 1 + (level.current + dexterity.current) * 0.01f;

        repairSkill.current = baseCraftingSkill.current + b_repairSkill.current + repairSkill.points;
        armourCraftSkill.current = baseCraftingSkill.current + b_armourCraftSkill.current + armourCraftSkill.points;
        weaponCraftSkill.current = baseCraftingSkill.current + b_weaponCraftSkill.current + weaponCraftSkill.points;
        runeCraftSkill.current = baseCraftingSkill.current + b_runeCraftSkill.current + runeCraftSkill.points;
    }

    void LevelUp ()
    {
        level.current++;
        experience.current -= experience.maximum;
    }
}
[Serializable]
public class CharacterStatistic
{
    [SerializeField]
    public float current;
    [SerializeField]
    public float maximum;
    [SerializeField]
    public float points = 0;

    bool hasMax;
    public CharacterStatistic (float _current, float _maximum)
    {
        current = _current;
        maximum = _maximum;
        hasMax = true;
    }

    public CharacterStatistic (float _current)
    {
        current = _current;
        hasMax = false;
    }
}
