using CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    internal interface IApplicationWeather
    {
        List<Weather> ReadWeather();
        void CreateWeather(string code, string description);
        void UpdateWeather(int id, string code, string description);
        void DeleteWeather(int id);
        Weather GetWeather(int id);
    }
}
