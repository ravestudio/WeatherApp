﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using WeatherApp.Core;


namespace WeatherApp
{
    [Activity(Label = "WeatherApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.GetWeatherButton);

            button.Click += async delegate
            {
                EditText ZipCodeEditText = FindViewById<EditText>(Resource.Id.ZipCodeEdit);

                Weather weather = null;

                string errMsg = string.Empty;

                try
                {
                    weather = await Core.Core.GetWeather(ZipCodeEditText.Text);
                }
                catch(Exception ex)
                {
                    errMsg = ex.Message;
                }

                if (weather != null)
                {
                    FindViewById<TextView>(Resource.Id.ResultsTitle).Text = weather.Title;
                    FindViewById<TextView>(Resource.Id.TempText).Text = weather.Temperature;
                    FindViewById<TextView>(Resource.Id.WindText).Text = weather.Wind;
                    FindViewById<TextView>(Resource.Id.VisibilityText).Text = weather.Visibility;
                    FindViewById<TextView>(Resource.Id.HumidityText).Text = weather.Humidity;
                    FindViewById<TextView>(Resource.Id.SunriseText).Text = weather.Sunrise;
                    FindViewById<TextView>(Resource.Id.SunsetText).Text = weather.Sunset;

                    button.Text = "Search Again";
                }
                else
                {
                    errMsg = string.IsNullOrEmpty(errMsg) ? "Couldn't find any results" : errMsg;

                    FindViewById<TextView>(Resource.Id.ResultsTitle).Text = errMsg;
                }

            };

        }
    }
}

