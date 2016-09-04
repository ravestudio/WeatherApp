using System.Threading.Tasks;

namespace WeatherApp.Core
{
    public class Core
    {
        public static Task<Weather> GetWeather(string zipCode)
        {

            TaskCompletionSource<Weather> TCS = new TaskCompletionSource<Weather>();

            

            string queryString =
                "https://query.yahooapis.com/v1/public/yql?q=select+*+from+weather.forecast+where+woeid+in+(select+woeid+from+geo.places(1)+where+text='" +
                 zipCode + "')&format=json";

        

            Task<dynamic> task = DataService.getDataFromService(queryString);

            task.ContinueWith(t =>
            {
                dynamic weatherOverview = t.Result["query"]["results"]["channel"];

                if ((string)weatherOverview["description"] != "Yahoo! Weather Error")
                {
                    Weather weather = new Weather();

                    weather.Title = (string)weatherOverview["description"];

                    dynamic wind = weatherOverview["wind"];
                    weather.Temperature = (string)wind["chill"];
                    weather.Wind = (string)wind["speed"];

                    dynamic atmosphere = weatherOverview["atmosphere"];
                    weather.Humidity = (string)atmosphere["humidity"];
                    weather.Visibility = (string)atmosphere["visibility"];

                    dynamic astronomy = weatherOverview["astronomy"];
                    weather.Sunrise = (string)astronomy["sunrise"];
                    weather.Sunset = (string)astronomy["sunset"];

                    TCS.SetResult(weather);
                }
                else
                {
                    TCS.SetResult(null);
                }

            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            task.ContinueWith(t =>
            {
                TCS.SetException(new System.Exception(t.Exception.InnerException.Message));

            }, TaskContinuationOptions.OnlyOnFaulted);

            return TCS.Task;
        }
    }
}
