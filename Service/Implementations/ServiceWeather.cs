using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using CrossCutting.DTO;

namespace Service.Implementations
{
    public class ServiceWeather : IServiceWeather
    {
        private readonly string lat = "-12.0432";
        private readonly string lon = "-77.0282";
        private readonly string APIkey = "";

        public string GetWeather()
        {
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
                    return "";
                }
            }
        }
        public string GetTemp()
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={APIkey}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var responseWeather = client.GetStringAsync(url).Result;
                    var responseMain = JObject.Parse(responseWeather).GetValue("main").ToString();
                    var responseTemp = JObject.Parse(responseMain).GetValue("temp").ToString();

                    var celsius = Convert.ToDouble(responseTemp) - 273.15; // Kelvin a Celsius
                    var celsiusR = Math.Round(celsius,2);

                    return celsiusR.ToString();
                }
                catch (HttpRequestException e)
                {
                    return "";
                }
            }
        }
    }
}
