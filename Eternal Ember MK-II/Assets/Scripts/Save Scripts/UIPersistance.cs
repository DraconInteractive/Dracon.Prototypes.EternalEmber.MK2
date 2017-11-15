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
		SaveSettingsCanvas ();
	}

	void SaveSettingsCanvas () {
		PlayerPrefs.SetInt ("HasSave", 1);
		r = Screen.currentResolution;
		PlayerPrefs.SetInt ("ScreenWidth", r.width);
		PlayerPrefs.SetInt ("ScreenHeight", r.height);
		if (Screen.fullScreen) {
			PlayerPrefs.SetInt ("isFullScreen", 1);
		} else {
			PlayerPrefs.SetInt ("isFullScreen", 0);
		}

		bool bloomOn = settings_canvas.bloomToggle.isOn;
		bool aaOn = settings_canvas.aaToggle.isOn;

		if (bloomOn) {
			PlayerPrefs.SetInt ("BloomOn", 1);
		} else {
			PlayerPrefs.SetInt ("BloomOn", 0);
		}

		if (aaOn) {
			PlayerPrefs.SetInt ("AAOn", 1);
		} else {
			PlayerPrefs.SetInt ("AAOn", 0);
		}

		PlayerPrefs.SetInt ("GraphicsQuality", (int)settings_canvas.graphicsQualitySlider.value);
	}

	public override void Load () {
		if (PlayerPrefs.GetInt("HasSave", 0) == 1) {
			LoadSettingsCanvas ();
		}
	}

	void LoadSettingsCanvas () {
		if (PlayerPrefs.GetInt("HasSave", 0) == 1) {
			Resolution newRes = new Resolution ();
			bool fullScreen = false;
			if (PlayerPrefs.GetInt("isFullScreen") == 0) {
				fullScreen = false;
			} else {
				fullScreen = true;
			}

			int screenWidth = PlayerPrefs.GetInt ("ScreenWidth");
			int screenHeight = PlayerPrefs.GetInt ("ScreenHeight");
			newRes.width = screenWidth;
			newRes.height = screenHeight;
			settings_canvas.currentRes = newRes;
			settings_canvas.isFullScreen = fullScreen;

			if (PlayerPrefs.GetInt("BloomOn") == 1) {
				settings_canvas.bloomToggle.isOn = true;
			} else {
				settings_canvas.bloomToggle.isOn = false;
			}

			if (PlayerPrefs.GetInt("AAOn") == 1) {
				settings_canvas.aaToggle.isOn = true;
			} else {
				settings_canvas.aaToggle.isOn = false;
			}

			int graphics_quality = PlayerPrefs.GetInt ("GraphicsQuality");
			settings_canvas.graphicsQualitySlider.value = graphics_quality;
		}
	}
}
