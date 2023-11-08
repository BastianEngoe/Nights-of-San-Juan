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

    int currentMusic;

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
        musicSources[currentMusic].Play();
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
        //foreach music in musicSources{

        //}
   }

}
