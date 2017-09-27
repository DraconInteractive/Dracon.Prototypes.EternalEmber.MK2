using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuloGames.UI;

public class GameMenuCanvas : MonoBehaviour {

    public static GameMenuCanvas thisCanvas;
    public UIWindow gameMenuWindow;
    public bool menuOpen;
	// Use this for initialization
	void Awake () {
        thisCanvas = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTransitionComplete (UIWindow window, UIWindow.VisualState state)
    {
        if (state == UIWindow.VisualState.Shown)
        {
            Time.timeScale = 0;
            menuOpen = true;
        }
        else
        {
            if (!Player.player.pauseCanvas.activeSelf)
            {
                Time.timeScale = 1;
            }
            menuOpen = false;
        }
    }

    public void ExitToDesktop ()
    {
        Application.Quit();
    }
}
