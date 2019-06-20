using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CityList.Test.Code
{
    public static class Validations
    {
        public static readonly Regex WeatherLineExp =
            new Regex(@"^(\d{4}-\d{2}-\d{2});(\d{2}:\d{2}:\d{2});(\d{2}:\d{2}:\d{2});([+-]?\d+(.\d+)?);([+-]?\d+(.\d+)?);([+-]?\d+(.\d+)?);([+-]?\d+(.\d+)?)\r?$", 
                RegexOptions.Multiline | RegexOptions.IgnoreCase);

        public static readonly Regex WeatherHeaderExp =
            new Regex(@"^Date;Sunrise;Sunset;Minimum Temperature;Maximum Temperature;Pressure;Humidity;Wind Speed\r?$",
                RegexOptions.Multiline | RegexOptions.IgnoreCase);

    }
}
