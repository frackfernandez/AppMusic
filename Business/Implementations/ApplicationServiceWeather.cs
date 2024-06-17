using Business.Interfaces;
using Service.Implementations;

namespace Business.Implementations
{
    public class ApplicationServiceWeather : IApplicationServiceWeather
    {
        ServiceWeather serWeather = new ServiceWeather();

        public string GetTemp()
        {
            var temp = serWeather.GetTemp();

            return temp;
        }
        public string GetWeather()
        {
            var weather = serWeather.GetWeather();

            return weather;
        }
    }
}
