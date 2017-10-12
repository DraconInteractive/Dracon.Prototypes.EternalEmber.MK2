using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;

public class MerchantCanvas : MonoBehaviour {
	public UIWindow window;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnWindowOpen (UIWindow window, UIWindow.VisualState vState) {
		if (vState == UIWindow.VisualState.Shown) {
			GetComponent<InventoryCanvas> ().ToggleInventory (true);
		}
	}

	public void ProcOpenWindow (List<Item_Type> itemsInWindow) {
		UIItemSlot[] slots = window.GetComponentsInChildren<UIItemSlot> ();
		foreach (UIItemSlot slot in slots) {
			slot.Unassign ();
		}
		for (int i = 0; i < itemsInWindow.Count; i++) {
			slots [i].Assign (Item_Type.GetInfoFromItem (itemsInWindow [i]));
			GameObject coinContainer = slots [i].transform.GetChild (3).gameObject;
			coinContainer.transform.GetChild (0).transform.GetChild (0).GetComponent<Text> ().text = itemsInWindow [i].convertedCost.x.ToString();
			coinContainer.transform.GetChild (1).transform.GetChild (0).GetComponent<Text> ().text = itemsInWindow [i].convertedCost.y.ToString();
			coinContainer.transform.GetChild (2).transform.GetChild (0).GetComponent<Text> ().text = itemsInWindow [i].convertedCost.z.ToString();
		}

		window.Show ();
	}
}
