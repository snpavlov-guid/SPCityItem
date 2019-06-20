using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityServiceClient.CityWeather
{
    public class Position
    {
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }

        public override string ToString()
        {
            return $"{Lat.ToString(CultureInfo.InvariantCulture)},{Lon.ToString(CultureInfo.InvariantCulture)}";
        }
    }

    public class WeatherData
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class MainData
    {
        public double Temp { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
    }

    public class WindData
    {
        public double Speed { get; set; }
        public double Deg { get; set; }
    }

    public class CloudsData
    {
        public int All { get; set; }
    }

    public class SystemData
    {
        public int Type { get; set; }
        public int Id { get; set; }
        public double Message { get; set; }
        public string Country { get; set; }

        public long Sunrise { get; set; }
        public long Sunset { get; set; }
     }

    public class CityWeatherModel
    {
        public long Id { get; set; }
        public long Dt { get; set; }
        public int Cod { get; set; }
        public string Name { get; set; }
        public string Base { get; set; }
        public double Visibility { get; set; }

        public Position Coord { get; set; }
        public WeatherData[] Weather { get; set; }
        public MainData Main { get; set; }
        public WindData Wind { get; set; }
        public CloudsData Clouds { get; set; }
        public SystemData Sys { get; set; }
    }
}
