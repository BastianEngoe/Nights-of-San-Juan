using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeatherController : MonoBehaviour
{
    public float weatherShiftTimer = 900f; //900 seconds is 15 minutes.
    private float timer = 0f;
    private int weatherIntensity = 1;
    public bool isWeatherRandom = false;

    private int randomNumber;

    public WeatherSystem weather;

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= weatherShiftTimer) 
        {
            if (isWeatherRandom)
            {
                weather.changeWeatherRandom();
                timer = 0f;
            }
            else
            {
                randomNumber = Random.Range(0, 100);
                //Debug.Log("Random number: " + randomNumber);
                if (randomNumber >= 35)
                {
                    if (weatherIntensity < weather.weatherSorts.Count -1)
                    {
                        weatherIntensity++;
                        timer = 0f;
                    }
                    else timer = 0f;
                }
                else
                {
                    if (weatherIntensity > 0)
                    {
                        weatherIntensity--;
                        timer = 0f;
                    }
                    else timer = 0f;
                }
                
                weather.startWeather(weather.weatherSorts[weatherIntensity]);
                Debug.Log("Weather intensity is now " + weatherIntensity);
            }
        }
    }
}
