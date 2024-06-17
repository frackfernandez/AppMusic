using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CrossCutting.DTO;

namespace Service.Implementations
{
    public class ServiceWeather : IServiceWeather
    {
        public string GetWeather()
        {
            var lat = "-12.0432";
            var lon = "-77.0282";
            var APIkey = "";
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={APIkey}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var responseWeather = client.GetStringAsync(url).Result;

                    var respWeather = JObject.Parse(responseWeather).GetValue("weather").ToString();

                    var respWeatherDes = JsonConvert.DeserializeObject<List<OpenWeather>>(respWeather);

                    return respWeatherDes[0].main;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                    return "error";
                }
            }
        }
        public string GetTemp()
        {
            var lat = "-12.0432";
            var lon = "-77.0282";
            var APIkey = "";
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={APIkey}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var responseWeather = client.GetStringAsync(url).Result;

                    var responseMain = JObject.Parse(responseWeather).GetValue("main").ToString();
                    var responseTemp = JObject.Parse(responseMain).GetValue("temp").ToString();

                    var celsius = Convert.ToDouble(responseTemp) - 273.15;

                    var celsiusR = Math.Round(celsius,2);

                    return celsiusR.ToString();
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                    return "error";
                }
            }
        }
    }
}
