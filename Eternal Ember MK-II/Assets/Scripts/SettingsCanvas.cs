using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;
using DuloGames.UI;
using System.Linq;

public class SettingsCanvas : MonoBehaviour {
    //Graphics
    public UIWindow settingsWindow;
    Resolution currentRes;
    bool isFullScreen;
    public Toggle fullScreenToggle, bloomToggle, aaToggle;
    public UISelectField resolutionSelectField;
    List<string> resolutionOptions;
    public UISliderExtended graphicsQualitySlider;
    public PostProcessingProfile postProfile;

    //Audio
    AudioControl aControl;
    public Slider volumeSlider, aMVSlider, aSVSlider, aSpSlider;
    public Toggle muteAllToggle;
	// Use this for initialization
	void Start () {
        aControl = AudioControl.aControl;

        currentRes = Screen.currentResolution;
        isFullScreen = Screen.fullScreen;

        resolutionOptions = new List<string>();
        foreach (Resolution r in Screen.resolutions)
        {
            resolutionOptions.Add(r.ToString());
        }

        resolutionSelectField.options = resolutionOptions;
        fullScreenToggle.isOn = isFullScreen;
        resolutionSelectField.SelectOption(currentRes.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnApply ()
    {
        StartCoroutine(ApplicationProcess());
    }

    IEnumerator ApplicationProcess ()
    {
        //Set resolution if needed
        string selectedRes = resolutionSelectField.options[resolutionSelectField.selectedOptionIndex];

        if (selectedRes == Screen.currentResolution.ToString())
        {
            print("ner1");
            string rpString = selectedRes;
            rpString = rpString.Replace(" ", "");
            print(selectedRes + " -> " + rpString);
            string[] alteredSelected = rpString.Split(new char[] { 'x', '@' });

            int w = int.Parse(alteredSelected[0]);
            int h = int.Parse(alteredSelected[1]);
            foreach (string s in alteredSelected)
            {
                print(s);
            }
            print("w: " + w + " h: " + h);
            StartCoroutine(ScreenChange(w, h, fullScreenToggle.isOn));
            //Screen.SetResolution(w, h, fullScreenToggle.isOn);
            //Screen.SetResolution(1920, 1080, true);
            print("Set res to " + selectedRes + " and fullscreen mode = " + fullScreenToggle.isOn);
        }

        yield return null;

        int q = QualitySettings.GetQualityLevel();

        if (graphicsQualitySlider.value == 0 && q != 1)
        {
            QualitySettings.SetQualityLevel(1);
        }
        else if (graphicsQualitySlider.value == 1 && q != 3)
        {
            QualitySettings.SetQualityLevel(3);
        }
        else if (graphicsQualitySlider.value == 2 && q != 5)
        {
            QualitySettings.SetQualityLevel(5, true);
        }

        yield return null;

        if (aaToggle.isOn != postProfile.antialiasing.enabled)
        {
            postProfile.antialiasing.enabled = aaToggle.isOn;
        }

        if (bloomToggle.isOn != postProfile.bloom.enabled)
        {
            postProfile.bloom.enabled = bloomToggle.isOn;
        }

        yield return null;
        
        if (muteAllToggle.isOn)
        {
            aControl.SetMuteAll(true);
        } 
        else
        {
            aControl.SetMuteAll(false);

            aControl.SetMainVolume(volumeSlider.value);
            aControl.SetAMusicVolume(aMVSlider.value);
            aControl.SetASoundVolume(aSVSlider.value);
            aControl.SetSpeechVolume(aSpSlider.value);
            print("Main volume: " + aControl.GetMainVolume());
        }
        
        yield break;
    }

    private IEnumerator ScreenChange(int w, int h, bool fullscreen)
    {
        Screen.fullScreen = fullscreen;

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        Screen.SetResolution(w, h, Screen.fullScreen);
        yield break;
    }

    public void SkipSongButton ()
    {
        aControl.NextSong();
    }
}
