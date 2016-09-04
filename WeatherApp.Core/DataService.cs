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

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new System.Uri(queryString));
            request.ContentType = "application/json";
            request.Method = "GET";

            var response = request.GetResponseAsync();

            response.ContinueWith(t =>
            {
                var stream = t.Result.GetResponseStream();

                var streamReader = new StreamReader(stream);
                string responseText = streamReader.ReadToEnd();

                dynamic data = JsonConvert.DeserializeObject(responseText);

                TCS.SetResult(data);
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            response.ContinueWith(t =>
            {
                TCS.SetException(new System.Exception(t.Exception.InnerException.Message));

            }, TaskContinuationOptions.OnlyOnFaulted);

            return TCS.Task;
        }
    }
}
