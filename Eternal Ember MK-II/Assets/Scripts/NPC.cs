using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    Animator anim;

    public enum NPCType { Base, Guide, Guard };
    public NPCType myType;

    public string[] converseDetails;
    private void Awake()
    {
        anim = GetComponent<Animator> ();
        switch (myType)
        {
            case NPCType.Guard:
                anim.SetBool ("isGuard", true);
                break;
            case NPCType.Guide:
                anim.SetBool ("isGuide", true);
                break;
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
