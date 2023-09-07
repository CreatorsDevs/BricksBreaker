using System;
using UnityEngine;

[DefaultExecutionOrder(49)]

public class AudioManager : MonoBehaviour
{
    // Array to hold Sound objects for different audio clips
    public Sound[] sounds;

    // Static instance of AudioManager to ensure only one instance exists
    public static AudioManager instance;

    void Awake()
    {
        // Singleton pattern: Only one AudioManager instance allowed
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            // If another instance already exists, destroy this one
            Destroy(gameObject);
            return;
        }

        // Don't destroy AudioManager on scene changes
        DontDestroyOnLoad(gameObject);

        // Initialize audio sources and settings for each Sound object
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        // Play background music when the game starts
        Play("Background Music");
    }

    // Play the specified sound by name
    public void Play (string name)
    {
        // Find the Sound object with the specified name
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        
        // If the Sound object is not found, log a warning and exit
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        // Play the sound using its associated AudioSource
        if (PlayerPrefs.GetInt("muted") != 1)
            s.source.Play();
        else
            { s.source.Play(); AudioListener.pause = true; }
    }
}
