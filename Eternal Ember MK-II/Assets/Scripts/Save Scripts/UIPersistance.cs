using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPersistance : Persistence {

	SettingsCanvas settings_canvas;
	SpellBookCanvas spell_book_canvas;
	UnitFrame unit_frame;
	CraftingCanvas crafting_canvas;
	MerchantCanvas merchant_canvas;
	ActionBarCanvas action_bar_canvas;

	//Settings Canvas
	Resolution r;
	void Start () {
		settings_canvas = SettingsCanvas.settingsCanvas;
	}

	public override void Save () {
		PlayerPrefs.SetInt ("HasSave", 1);
		r = Screen.currentResolution;
		PlayerPrefs.SetInt ("ScreenWidth", r.width);
		PlayerPrefs.SetInt ("ScreenHeight", r.height);
		if (Screen.fullScreen) {
			PlayerPrefs.SetInt ("isFullScreen", 1);
		} else {
			PlayerPrefs.SetInt ("isFullScreen", 0);
		}
	}

	public override void Load () {
		if (PlayerPrefs.GetInt("HasSave", 0) == 1) {
			Resolution newRes = new Resolution ();
			bool fullScreen = PlayerPrefs.GetInt ("isFullScreen");
			int screenWidth = PlayerPrefs.GetInt ("ScreenWidth");
			int screenHeight = PlayerPrefs.GetInt ("ScreenHeight");
			newRes.width = screenWidth;
			newRes.height = screenHeight;
			settings_canvas.currentRes = newRes;
			settings_canvas.isFullScreen = fullScreen;
		}
	}
}
