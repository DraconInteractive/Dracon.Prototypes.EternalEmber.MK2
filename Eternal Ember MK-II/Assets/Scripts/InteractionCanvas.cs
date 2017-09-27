using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DuloGames.UI;

public class InteractionCanvas : MonoBehaviour {

    public Text titleText, mainText;

    public void CloseWindow ()
    {
        Destroy(this.gameObject);
    }
}


