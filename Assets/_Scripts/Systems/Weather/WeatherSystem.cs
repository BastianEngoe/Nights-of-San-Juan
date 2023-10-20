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
        //Set the current Weather based on whats selected in the WeatherList enum in the inspector.
        currentWeatherSort = weatherSorts[(int)currentWeather];
        startWeather(currentWeatherSort);
    }

    public void changeWeatherRandom() //Changes the weather randomly, called from the WeatherController script.
    {
        int randomNumber;
        randomNumber = Random.Range(0, weatherSorts.Count);
        startWeather(weatherSorts[randomNumber]);
    }
    
    public void startWeather(weatherSort weather) //Need to input weather type to use function, have to use weatherSort[listNumber] to choose. Not the easiest way but it works.
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

    public enum WeatherList //All the weather options that will show up in inspector. They have to correspond in the same order as the weatherSort list.
    {
        none,
        cloudy,
        drizzle,
        rain,
        lightning,
        storm
    }

    [System.Serializable]
    public class weatherSort //Class used to define what is in the weather. Fill all these in the inspector when you add a new one to the weatherSort list.
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
