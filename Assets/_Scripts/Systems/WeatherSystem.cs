using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WeatherSystem : MonoBehaviour
{
    public WeatherList currentWeather;
    private weatherSort currentWeatherSort;

    public List<weatherSort> weatherSorts = new List<weatherSort>();
    
    public ParticleSystem rainParticleSystem;
    public ParticleSystem cloudParticleSystem;
    public ParticleSystem lightningParticleSystem;
    void Start()
    {
        //Set the current RainSort
        currentWeatherSort = weatherSorts[(int)currentWeather];
        startWeather(currentWeatherSort);
    }

    public void changeWeatherRandom()
    {
        int randomNumber;
        randomNumber = Random.Range(0, weatherSorts.Count);
        startWeather(weatherSorts[randomNumber]);
    }
    
    public void startWeather(weatherSort weather)
    {
        var rainEmission = rainParticleSystem.emission;
        var lightningEmission = lightningParticleSystem.emission;
        if (weather.haveCloud)
        {
            cloudParticleSystem.Play();
        }
        else {cloudParticleSystem.Stop();}

        if (weather.intensity != 0)
        {
            rainParticleSystem.Play();
            rainEmission.rateOverTime = weather.intensity * 416 * 1.5f;
            if (weather.sound != null)
            {
                
            }
        }
        else
        {
            rainParticleSystem.Stop();
        }

        if (weather.haveLightning)
        {
            lightningParticleSystem.Play();
            lightningEmission.rateOverTime = weather.lightIntensity;
        }
        else
        {
            lightningParticleSystem.Stop();
        }
    }

    public enum WeatherList
    {
        none,
        cloudy,
        drizzle,
        rain,
        lightning,
        storm
    }

    [System.Serializable]
    public class weatherSort
    {
        public string name;
        public int intensity;
        public int lightIntensity;
        public AudioClip sound;
        public int volume;
        public bool haveCloud = true;
        public bool haveLightning = true;
    }
}
