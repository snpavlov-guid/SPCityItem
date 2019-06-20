using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityList.Code
{
    public static class AppProperties
    {
        public const string CityWeatherServiceUrl = "http://gmtinterview.azurewebsites.net";
        public const int RetryCount = 3;
        public const int RetrtInterval = 500; //ms

        public const string CityDocsLibName = "CityDocuments";

        public const string CityWeatherDocRefFormat = "{0} weather";
    }

    public static class CityItemFields
    {
        public const string DocumentRef = "DocumentRef";
        public const string Latitude = "Latitude";
        public const string Longitude = "Longitude";
    }

}
