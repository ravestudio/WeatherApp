using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace WeatherApp.Core
{
    public class DataService
    {
        public static Task<dynamic> getDataFromService(string queryString)
        {
            TaskCompletionSource<dynamic> TCS = new TaskCompletionSource<dynamic>();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(queryString);

            var response = request.GetResponseAsync();

            response.ContinueWith(t =>
            {
                var stream = t.Result.GetResponseStream();

                var streamReader = new StreamReader(stream);
                string responseText = streamReader.ReadToEnd();

                dynamic data = JsonConvert.DeserializeObject(responseText);

                TCS.SetResult(data);
            });

            return TCS.Task;
        }
    }
}
