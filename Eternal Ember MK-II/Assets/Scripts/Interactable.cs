using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;

public class Interactable : MonoBehaviour {

    public float stoppingDistance;
    public GameObject selection;

    public enum ActionType { Converse, Loot, Information, Door, Quest, CraftingTable, Resource };
    public ActionType i_type;

    public  GameObject associateCanvas;
    InteractionCanvas canvasAccess;

    public bool rotatingSelection;
    public string desc;

    public UIWindow uiWindow;

    public bool hasInterCanvas;

	cakeslice.Outline objOutline;
    // Use this for initialization
    void Start()
    {
		objOutline = GetComponent<cakeslice.Outline> ();
		if (objOutline != null) {
			objOutline.enabled = false;
		}
        SetupCanvas ();

		if (selection != null) {
			selection.SetActive (false);
		}
        

        if (uiWindow != null)
        {
            uiWindow.Hide();
        }
        
        
    }

    void SetupCanvas()
    {   
        switch (i_type)
        {
            case ActionType.Converse:
                NPC me = GetComponent<NPC> ();
                string ss = "";
                foreach (string sss in me.converseDetails)
                {
                    ss += sss;
                    ss += "\n\n";
                }
                canvasAccess = associateCanvas.GetComponent<InteractionCanvas>();
                canvasAccess.titleText.text = gameObject.name;
                canvasAccess.mainText.text = ss;

                break;
            case ActionType.Information:
                Interactable_Info info = GetComponent<Interactable_Info> ();
                string i = "";
                foreach (string ii in info.information)
                {
                    i += ii;
                    i += "\n\n";
                }
                canvasAccess = associateCanvas.GetComponent<InteractionCanvas>();
                canvasAccess.titleText.text = gameObject.name;
                canvasAccess.mainText.text = i;

                break;
            case ActionType.Loot:
                canvasAccess = associateCanvas.GetComponent<InteractionCanvas>();
                canvasAccess.titleText.text = gameObject.name;
                canvasAccess.mainText.text = desc;
                break;
            case ActionType.Quest:
                break;
            case ActionType.Resource:
                canvasAccess = associateCanvas.GetComponent<InteractionCanvas>();
                canvasAccess.titleText.text = gameObject.name;
                canvasAccess.mainText.text = desc;
                break;
        }

    }
    // Update is called once per frame
    void Update()
    {
		if (selection != null) {
			if (selection.activeSelf && i_type != ActionType.Door && rotatingSelection)
			{
				//selection.transform.LookAt(Camera.main.transform.position);
				selection.transform.forward = -(Camera.main.transform.position - selection.transform.position);
			}
		}
        
    }

    private void OnMouseDown()
    {
        if (UIWindowManager.WindowOpen())
        {
            print("window open");
            return;
        }
		//TODO: Move to position, when arrived call BeginInteract. E.g.:

//        playerMovement.ProcMove (transform.position, stoppingDistance);
//        playerMovement.onArrived = BeginInteract;

    }

    private void OnMouseEnter()
    {
        //print ("Mouse present over: " + gameObject.name);
		if (selection != null) {
			selection.SetActive (true);
		}
        
		if (objOutline != null) {
			objOutline.enabled = true;
		}
		print ("mouse over on" + name);
    }

    private void OnMouseExit()
    {
		if (selection != null) {
			selection.SetActive (false);
		}
        
		if (objOutline != null) {
			objOutline.enabled = false;
		}
    }

    void BeginInteract()
    {
        StartCoroutine (Interact ());
    }

    IEnumerator Interact()
    {
        Player.player.Interact ();
        yield return new WaitForSeconds (1.5f + Player.player.interactWaitTime);
        if (uiWindow == null)
        {
			if (associateCanvas != null) {
				associateCanvas.SetActive(true);
			}
        }
        else
        {
            uiWindow.Show();
        }
        

        switch (i_type)
        {
            case ActionType.Door:
                Door d = GetComponent<Door> ();
                if (!d.locked)
                {
					if (associateCanvas != null) {
						associateCanvas.SetActive (false);
					}
					d.ProcOpen ();
                }
                break;
            case ActionType.Loot:
                //ItemContainer container = canvasAccess.transform.parent.gameObject.GetComponent<ItemContainer>();
                ItemContainer container = GetComponent<ItemContainer>();
                GameObject slotsContainer = uiWindow.transform.GetChild(2).transform.GetChild(1).transform.gameObject;

                UIItemSlot[] invSlots = uiWindow.gameObject.GetComponentsInChildren<UIItemSlot>();

                foreach (UIItemSlot slot in invSlots)
                {
                    slot.Unassign();
                }
                
                int counter = 0;
                string slotPop = "";
                foreach (Item i in container.itemEquiv)
                {
                    
                    slotPop += ("Slotpop: " + counter + "|" + i.itemName + "\n");
                    UIItemInfo info = Item_Type.GetInfoFromItem(i.item_type); ;
                    
                    invSlots[counter].Assign(info);
                    counter++;
                }
                print(slotPop);
                break;
            case ActionType.Resource:
                Resource thisResource = GetComponent<Resource>();
                GameObject slotsRContainer = uiWindow.transform.GetChild(2).transform.GetChild(1).transform.gameObject;

                UIItemSlot[] invRSlots = uiWindow.gameObject.GetComponentsInChildren<UIItemSlot>();

                foreach (UIItemSlot slotR in invRSlots)
                {
                    slotR.Unassign();
                }

                UIItemInfo infoR = Item_Type.GetInfoFromItem(thisResource.resource.item_type); ;

                invRSlots[0].Assign(infoR);
                break;
        }
        yield break;
    }

    void CloseAssociateCanvas()
    {
        associateCanvas.SetActive (false);
    }
}
