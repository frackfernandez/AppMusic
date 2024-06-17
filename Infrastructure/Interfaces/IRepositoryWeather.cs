using CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    internal interface IRepositoryWeather
    {
        List<Weather> ReadWeather();
        void CreateWeather(string code, string description);
        void UpdateWeather(int id, string code, string description);
        void DeleteWeather(int id);
        Weather GetWeather(int id);
    }
}
