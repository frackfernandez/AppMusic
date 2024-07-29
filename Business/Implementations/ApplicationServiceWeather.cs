using Business.Interfaces;
using Service.Implementations;
using Service.Interfaces;

namespace Business.Implementations
{
    public class ApplicationServiceWeather : IApplicationServiceWeather
    {
        private readonly IServiceWeather serWeather;

        public ApplicationServiceWeather()
        {
            serWeather = new ServiceWeather();
        }

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
