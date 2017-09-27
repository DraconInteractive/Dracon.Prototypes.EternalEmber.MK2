using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Recipe", menuName = "Generate/Recipe")]
public class Recipe : ScriptableObject {

    public string recipeName;
    public RecipeMaterial[] materials;
    public Item_Type result;
}
[Serializable]
public class RecipeMaterial
{
    public Item_Type material;
    public int amount;
}
