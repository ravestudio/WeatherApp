using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using WeatherApp.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherApp.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void GetWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            Weather weather = null;
            string errMsg = string.Empty;
            try
            {
                weather = await Core.Core.GetWeather(ZipCodeEdit.Text);
            }
            catch(Exception ex)
            {
                errMsg = ex.Message;

            }

            if (weather != null)
            {
                ResultsTitle.Text = weather.Title;
                TempText.Text = weather.Temperature;
                WindText.Text = weather.Wind;
                VisibilityText.Text = weather.Visibility;
                HumidityText.Text = weather.Humidity;
                SunriseText.Text = weather.Sunrise;
                SunsetText.Text = weather.Sunset;

                GetWeatherButton.Content = "Search Again";

            }
            else
            {
                errMsg = string.IsNullOrEmpty(errMsg) ? "Couldn't find any results" : errMsg;

                ResultsTitle.Text = errMsg;
            }
        }

    }
}

