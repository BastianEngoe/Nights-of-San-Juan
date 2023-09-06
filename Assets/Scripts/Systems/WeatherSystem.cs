using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    public RainList currentRain;
    private rainSort currentRainSort;

    public List<rainSort> rainSorts = new List<rainSort>();
    
    public ParticleSystem rainParticleSystem;
    public ParticleSystem cloudParticleSystem;
    void Start()
    {
        //Set the current RainSort
        currentRainSort = rainSorts[(int)currentRain];
        startRain(currentRainSort);
    }

    public void startRain(rainSort rain)
    {
        if (rain.haveCloud)
        {
            cloudParticleSystem.Play();
        }
        else {cloudParticleSystem.Stop();}

        if (rain.intensity != 0)
        {
            rainParticleSystem.Play();
            rainParticleSystem.emissionRate = rain.intensity * 416 * 1.5f;
            if (rain.sound != null)
            {
            
            }
        }
        else
        {
            rainParticleSystem.Stop();
        }
    }

    public enum RainList
    {
        none,
        cloudy,
        drizzle,
        rain
    }

    [System.Serializable]
    public class rainSort
    {
        public string name;
        public int intensity;
        public AudioClip sound;
        public int volume;
        public bool haveCloud = true;
    }
}
