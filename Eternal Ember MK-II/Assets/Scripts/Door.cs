using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Door : MonoBehaviour
{

    public bool locked;

    public enum DoorType { ForceField, Slide, Part, Swing };
    public DoorType d_type;

    public GameObject[] pieces;
    public float distance, speed;
    Coroutine actionRoutine;

    public Renderer[] statusImages;
    public Sprite lockedSprite, openSprite;
    public bool Locked
    {
        get
        {
            return locked;
        }

        set
        {
            locked = value;
            if (value)
            {
                Lock ();
            }
            else
            {
                Unlock ();
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        Locked = locked;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void ProcOpen()
    {
        if (actionRoutine != null)
        {
            //StopCoroutine (actionRoutine);
            return;
        }
        actionRoutine = StartCoroutine (OpenDoor ());
    }

    IEnumerator OpenDoor()
    {
        switch (d_type)
        {
            case DoorType.ForceField:

                //FLICKER MOTHERFUCKER
                for (int i = 0; i < 5; i++)
                {
                    pieces[0].SetActive (false);
                    yield return new WaitForSeconds (Random.Range (0.1f, 0.3f));
                    pieces[0].SetActive (true);
                    yield return new WaitForSeconds (Random.Range (0.1f, 0.3f));
                }

                pieces[0].SetActive (false);
                break;
            case DoorType.Part:
                float m = 0;

                while (m < 1 * distance)
                {
                    pieces[0].transform.position = transform.position - transform.right * m * speed * Time.deltaTime;
                    pieces[1].transform.position = transform.position + transform.right * m * speed * Time.deltaTime;

                    m += Time.deltaTime;
                    yield return null;
                }
                break;
            case DoorType.Slide:
                float s = 0;

                while (s < 1 * distance)
                {
                    pieces[0].transform.position = transform.position - transform.right * s * speed * Time.deltaTime;

                    s += Time.deltaTime;
                    yield return null;
                }
                break;
            case DoorType.Swing:
                bool characterInFront = false;
                Vector3 toCharacter = (Player.player.transform.position - transform.position).normalized;
                float dProduct = Vector3.Dot (transform.forward, toCharacter);
                if (dProduct > 0)
                {
                    characterInFront = true;
                }
                
                float p = 0;
                float pMod = 1;
                if (!characterInFront)
                {
                    pMod = -1;
                }
                while (p < 1 * distance)
                {
                    //pieces[0].transform.rotation *= Quaternion.Euler (new Vector3 (0,speed * pMod * Time.deltaTime,0));
                    pieces[0].transform.Rotate (transform.up, pMod * speed * Time.deltaTime);
                    p += Time.deltaTime;
                    yield return null;
                }
                break;
        }
        GetComponent<Collider> ().enabled = false;
        GetComponent<NavMeshObstacle> ().enabled = false;
        actionRoutine = null;
        yield break;
    }

    public void ProcClose()
    {

    }

    public void Lock()
    {
        if (d_type == DoorType.Swing)
        {
            foreach (SpriteRenderer i in statusImages)
            {
                //i.material.SetColor ("_Color", Color.green);
                i.sprite = lockedSprite;
            }
        }
        else
        {
            foreach (Renderer i in statusImages)
            {
                //i.material.SetColor ("_Color", Color.green);
                i.material.color = Color.red;
            }
        }
        
    }

    public void Unlock()
    {
        if (d_type == DoorType.Swing)
        {
            foreach (SpriteRenderer i in statusImages)
            {
                //i.material.SetColor ("_Color", Color.green);
                i.sprite = openSprite;
            }
        }
        else
        {
            foreach (Renderer i in statusImages)
            {
                //i.material.SetColor ("_Color", Color.green);
                i.material.color = Color.green;
            }
        }
        
    }
}
