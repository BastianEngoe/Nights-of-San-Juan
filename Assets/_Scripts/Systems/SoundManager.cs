using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AudioClips
{

}
public class SoundManager : MonoBehaviour
{
    
    public static SoundManager instance = null;
    [SerializeField] private AudioClip[] soundEffects = new AudioClip[22];
    private AudioSource myAudioSource;
    [SerializeField] private GameObject musicSource;
    private AudioSource[] musicSources = new AudioSource[4];

    private float musicVolume, soundVolume;
    [SerializeField] private float minIntervTime=50, maxIntervTime=120;
    private float timer=0;

    private bool playingMusic =false;

    int currentMusic=0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        musicSources = musicSource.GetComponents<AudioSource>();
        currentMusic = 0;
    }

    private void Update()
    {
        if (playingMusic && timer <= 0&& !musicSources[currentMusic].isPlaying)
        {
            musicSources[currentMusic].Play();
            timer= Random.Range(minIntervTime, maxIntervTime);
        }
        else if(playingMusic && timer>0 && !musicSources[currentMusic].isPlaying) {
         timer-=Time.deltaTime;
        }
    }

    public void PlayMusicOnRandomInterv()
    {
        playingMusic = true;
    }
    
    public void StopMusic()
    {
        musicSources[currentMusic].Stop();
        playingMusic=false;
        timer=0;
    }

    public void PlaySound(AudioClips sound, float volume = -1)
    {
        if (volume != -1)
            myAudioSource.PlayOneShot(soundEffects[(int)sound], volume);
        else
            myAudioSource.PlayOneShot(soundEffects[(int)sound]);
    }

   public void ChangeSoundsVolume(float newVolume)
   {
        soundVolume = newVolume;
        myAudioSource.volume=soundVolume;
   }
    public void ChangeMusicVolume(float newVolume)
   {
        musicVolume = newVolume;
        foreach (AudioSource audioSource in musicSources)
        {
            audioSource.volume = newVolume;
        }
    }

}
