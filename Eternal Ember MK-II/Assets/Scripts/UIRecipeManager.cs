using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DuloGames.UI;

public class UIRecipeManager : MonoBehaviour, IPointerDownHandler {
    public static UIRecipeManager selectedRecipe;

    public Text titleText, descText;
    public Button craftButton;
    public GameObject materialSlot;
    
    public NicerOutline outline;

    [HideInInspector]
    public Recipe assocRecipe;
    void Start ()
    {
        outline.enabled = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (selectedRecipe != null)
            {
                selectedRecipe.SetOutline(false);
            }
            
            SetOutline(true);
            selectedRecipe = this;
        }
    }

    public void SetOutline (bool state)
    {
        outline.enabled = state;
    }
}
