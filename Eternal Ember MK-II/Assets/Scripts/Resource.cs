using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {
    public Item resource;

    public bool hasDissolve;

    public float respawnRate;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ProcDissolve ()
    {
        StartCoroutine(Dissolve());
        
    }
    IEnumerator Dissolve ()
    {
        float progress = 1;
        Material[] m = GetComponent<Renderer>().materials;

        while (progress > 0)
        {
            foreach (Material mm in m)
            {
                mm.SetFloat("_Progress", progress);
            }
            progress -= Time.deltaTime;
            yield return null;
        }

        GetComponent<Collider>().enabled = false;
        Invoke("ProcCondense", respawnRate);
        yield break;
    }

    public void ProcCondense ()
    {
        StartCoroutine(Condense());
    }

    IEnumerator Condense ()
    {
        float progress = 0;
        Material[] m = GetComponent<Renderer>().materials;

        while (progress < 1)
        {
            foreach (Material mm in m)
            {
                mm.SetFloat("_Progress", progress);
            }

            progress += Time.deltaTime;
            yield return null;
        }

        GetComponent<Collider>().enabled = true;
        yield break;
    }

    public void AddToInventory ()
    {
        bool b = Player.player.playerInventory.AddItem(new Item(resource.item_type, resource.itemQuantity));
        if (b)
        {
            ProcDissolve();
        }
        else
        {

        }
    }
}
