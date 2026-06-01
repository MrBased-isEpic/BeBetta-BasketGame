using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    
    #endregion
    
    [Range(0f,1f)]
    public float masterVolume = 1;
    
    private AudioSource[] sources;
    private Coroutine musicFading;

    private void Start()
    {
        sources = GetComponents<AudioSource>();
    }

    public void SetMasterVolume(float volume)
    {
        int musicSourceIndex = sources[0].isPlaying ? 0 : 1;
        AudioSource newSource = sources[musicSourceIndex];
        
        newSource.volume = 1;
        
        masterVolume = volume;
        
        newSource.volume = volume * masterVolume;
    }
    
    public void PlayMusic(AudioClip clip, float volume = 1)
    {
        int newSourceIndex = sources[0].isPlaying ? 1 : 0;
        AudioSource newSource = sources[newSourceIndex];
        newSource.clip = clip;
        newSource.volume = volume * masterVolume;

        newSource.Play();
        
        musicFading = StartCoroutine(
            FadeInto(sources[newSourceIndex == 0 ? 1 : 0], newSource, 1));
    }

    public void PlaySFX(AudioClip clip, float volume = 1)
    {
        AudioSource newSource = GetIdleSource();
        
        newSource.clip = clip;
        newSource.volume = volume * masterVolume;
        newSource.Play();
    }

    private AudioSource GetIdleSource()
    {
        for (var index = 2; index < sources.Length; index++)
        {
            AudioSource source = sources[index];
            
            if (!source.isPlaying) return source;
        }
        sources[2].Stop();
        
        return sources[2];
    }
    

    private IEnumerator FadeInto(AudioSource playingSource, AudioSource newSource, float fadeTime)
    {
        if(playingSource != newSource)
            StartCoroutine(Animations.LerpVolume(playingSource, 0, fadeTime));
        
        float targetVolume = newSource.volume;
        newSource.volume = 0;
        
        yield return StartCoroutine(Animations.LerpVolume(newSource, targetVolume, fadeTime));

        if (playingSource != newSource)
        {
            playingSource.Stop();
            playingSource.volume = 1;
        }

        musicFading = null;
    }

}
