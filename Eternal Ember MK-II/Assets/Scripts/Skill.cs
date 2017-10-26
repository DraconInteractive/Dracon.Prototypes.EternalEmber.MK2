using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Generate/Skill")]
public class Skill : Ability
{
    public enum Attack_Type { Melee, Ranged, Magic };
    public Attack_Type atk_type;

    public override void ApplyEffect(GameObject target)
    {
        base.ApplyEffect(target);
    }
}
