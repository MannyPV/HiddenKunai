using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Static Instance
    private static AudioManager instance;
    public static AudioManager Instance 
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }

            return instance;
        }

        private set
        {
            instance = value;
        }
    }
    #endregion

    #region Fields
    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;

    private bool firstMusicSourceIsPlaying;
    #endregion

    private void Awake()
    {
        // dont destroy this instance
        DontDestroyOnLoad(this.gameObject);

        // create audio sources
        musicSource = this.gameObject.AddComponent<AudioSource>();
        musicSource2 = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();

        // Loop the music tracks
        musicSource.loop = true;
        musicSource2.loop = true;
    }

    public void PlayMusic(AudioClip musicClip)                                                  // function to use when playing music on start
    {
        // determine which source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;

        activeSource.clip = musicClip;
        activeSource.volume = 1;
        activeSource.Play();
    }       
    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)               // function to use when fading between tracks
    {
        // determine which source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;

        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }
    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)        // function to use when cross fading between tracks
    {
        // determine which source is active
        AudioSource activeSource = (firstMusicSourceIsPlaying) ? musicSource : musicSource2;
        AudioSource newSource = (firstMusicSourceIsPlaying) ? musicSource2 : musicSource;

        // swap the source
        firstMusicSourceIsPlaying = !firstMusicSourceIsPlaying;

        // Set the fields of the audio source, thens start the coroutine to crossfade
        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSource, newSource, transitionTime));
    }


    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        // make sure source is active and playing
        if (!activeSource.isPlaying)
            activeSource.Play();
        
        float t = 0.0f;

        // fade out
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (1 - (t / transitionTime));
            yield return null;
        }

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        // fade in
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime);
            yield return null;
        }
    }
    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    { 
        float t = 0.0f;

        // fade out original source as new one fades in
        for (t = 0.0f; t <= transitionTime; t += Time.deltaTime)
        { 
            original.volume = (1 - (t / transitionTime));
            newSource.volume = (t / transitionTime);
            yield return null;
        }
        
        original.Stop();
    }


    public void PlaySFX(AudioClip clip)                 // function to use when playing any sound effects during gameplay events
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlaySFX(AudioClip clip, float volume)   // PlaySFX overload function for volume control during gameplay
    {
        sfxSource.PlayOneShot(clip, volume);
    } 

    public void SetMusicVolume(float volume)            // music volume control fucntion for UI options menu
    {
        musicSource.volume = volume;
        musicSource2.volume = volume;
    }             
    public void SetSFXVolume(float volume)              // SFX volume control fucntion for UI options menu
    {
        sfxSource.volume = volume;
    }                

    
}

