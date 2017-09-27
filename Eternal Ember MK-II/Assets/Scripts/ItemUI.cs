using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Item associatedItem;
    public InventoryCanvas parentCanvas;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData data)
    {
        //parentCanvas.AddToolTipTarget (this);
    }

    public void OnPointerExit (PointerEventData data)
    {
        //parentCanvas.RemoveToolTipTarget (this);
    }


}
