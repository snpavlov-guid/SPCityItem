using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityServiceClient.CityWeather
{
    public interface ICityWeatherClient
    {
        string GetCityWeatherCSV(Position coords);
    }
}
