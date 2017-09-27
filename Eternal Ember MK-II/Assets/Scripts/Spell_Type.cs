using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;

[CreateAssetMenu(fileName = "SpellType", menuName = "Generate/Spell")]
public class Spell_Type : Ability
{
    public UISpellInfo info;

    void Start ()
    {
        SetInfo();
    }

    void SetInfo ()
    {
        info.Range = range;
    }

}
