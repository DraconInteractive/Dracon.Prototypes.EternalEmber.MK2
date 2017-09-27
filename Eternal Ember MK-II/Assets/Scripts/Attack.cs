using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Generate/Attack")]
public class Attack : Ability
{
    public enum Attack_Type { Melee, Ranged };
    public Attack_Type atk_type;

    public override void ApplyEffect(GameObject target)
    {
        base.ApplyEffect(target);
    }
}
