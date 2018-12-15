using UnityEngine;
using System;
using UnityEngine.Audio;

public class Audiomanager : MonoBehaviour {

    private string[] bgm = { "bgm_aridWasteland", "bgm_barrenAmbience", "bgm_nordicLandscape", "bgm_openExploring"};
    private int songChoice = 0;
    private float bgmDelay = 0.5f;

    public Sound[] sounds;
    public static Audiomanager instance;

	// Use this for initialization
	void Awake () {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

		foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
	}

    private void Start()
    {
        PickBGM();
    }

    // Looks for specific sound in array and plays it
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + " not be found.");
            return;
        }
        else
        {
            s.source.Play();
        }
    }

    // Looks for sound and stops it
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + " not be found.");
            return;
        }
        else
        {
            s.source.Stop();
        }
    }

    // Randomly selects a song
    private void PickBGM()
    {
        songChoice = UnityEngine.Random.Range(0, bgm.Length);
        StartBgm();
    }

    // Starts the song loop
    private void StartBgm()
    {
        Play(bgm[songChoice]);

        Invoke("StartBgm", GetSongLength() + bgmDelay);

        songChoice++;

        if (songChoice >= bgm.Length)
        {
            songChoice = 0;
        }
    }

    // Gets length of current song
    private float GetSongLength()
    {
        float songLength = 0;

        for(int i = 0; i < sounds.Length; i++)
        {
            if (bgm[songChoice] == sounds[i].name)
            {
                songLength = sounds[i].clip.length;
            }
        }
        return songLength;
    }
}
