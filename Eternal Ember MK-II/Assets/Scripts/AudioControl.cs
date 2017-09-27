using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour {

    public static AudioControl aControl;
    public AudioSource ambientMusic, ambientSound, speech;
    public AudioMixer mixer;
    float v, vAM, vAS, vSp;

    public AudioClip[] ambienceClips;
    private void Awake()
    {
        aControl = this;
    }
    // Use this for initialization
    void Start () {
        SetAMusicVolume(100);
        SetASoundVolume(100);
        SetSpeechVolume(100);
        SetMainVolume(100);
	}
	
	// Update is called once per frame
	void Update () {
		if (!ambientMusic.isPlaying)
        {
            NextSong();
        }
	}

    public void NextSong ()
    {
        AudioClip rClip = ambienceClips[Random.Range(0, ambienceClips.Length)];
        while (rClip == ambientMusic.clip)
        {
            rClip = ambienceClips[Random.Range(0, ambienceClips.Length)];
        }
        ambientMusic.clip = rClip;
        ambientMusic.Play();
    }
    public void SetMainVolume (float value)
    {

        v = value;
        float tV = v / 100;
        //print("V: " + v);
        ambientMusic.volume = (tV + vAM) / 2;
        ambientSound.volume = (tV + vAS) / 2;
        speech.volume = (tV + vSp) / 2;
    }

    public void SetAMusicVolume (float value)
    {
        vAM = value / 100;
        SetMainVolume(v);
    }

    public void SetASoundVolume (float value)
    {
        vAS = value / 100;
        SetMainVolume(v);
    }

    public void SetSpeechVolume (float value)
    {
        vSp = value / 100;
        SetMainVolume(v);
    }
    public float GetMainVolume ()
    {
        return v * 100;
    }
    public void SetMuteAll (bool state)
    {
        ambientMusic.mute = state;
        ambientSound.mute = state;
        speech.mute = state;
    }
}
