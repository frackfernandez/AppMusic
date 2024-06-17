using Business.Interfaces;
using CrossCutting;
using Infrastructure.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class ApplicationWeather : IApplicationWeather
    {
        RepositoryWeather repWeather = new RepositoryWeather();

        public void CreateWeather(string code, string description)
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
        public List<Weather> ReadWeather()
        {
            var list = repWeather.ReadWeather();

            return list;
        }
        public void UpdateWeather(int id, string code, string description)
        {
            repWeather.UpdateWeather(id, code, description);
        }
    }
}
