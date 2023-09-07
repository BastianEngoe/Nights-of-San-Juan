using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public float weatherShiftTimer = 900f; //900 seconds is 15 minutes.
    private float timer = 0f;

    public WeatherSystem weather;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= weatherShiftTimer)
        {
            weather.changeWeatherRandom();
            timer = 0f;
        }
    }
}
