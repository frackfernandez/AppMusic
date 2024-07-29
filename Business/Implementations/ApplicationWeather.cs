using Business.Interfaces;
using CrossCutting;
using CrossCutting.Enums;
using Infrastructure.Implementations;
using Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Business.Implementations
{
    public class ApplicationWeather : IApplicationWeather
    {
        private readonly IRepositoryWeather repWeather;

        public ApplicationWeather()
        {
            repWeather = new RepositoryWeather();
        }
        public void CreateWeather(Code code, string description)
        {
            repWeather.CreateWeather(code, description);
        }
        public void DeleteWeather(int id)
        {
            repWeather.DeleteWeather(id);
        }
        public Weather GetWeather(int id)
        {
            var weather = repWeather.GetWeather(id);

            return weather;
        }
        public Weather GetWeather(string code)
        {
            var weather = repWeather.GetWeather(code);

            return weather;
        }
        public List<Weather> ReadWeather()
        {
            var list = repWeather.ReadWeather();

            return list;
        }
        public void UpdateWeather(int id, Code code, string description)
        {
            repWeather.UpdateWeather(id, code, description);
        }
    }
}
