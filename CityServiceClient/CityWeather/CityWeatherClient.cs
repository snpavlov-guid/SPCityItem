using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using CityServiceClient.Common;
using CityServiceClient.Workers;
using CityServiceClient.DataRequest;

namespace CityServiceClient.CityWeather
{
    public class CityWeatherClient : ICityWeatherClient
    {
        private IDataRequest _dataRequest;
        private string _serviceBaseUrl;


        public CityWeatherClient(IDataRequest dataRequest, string serviceBaseUrl)
        {
            _dataRequest = dataRequest;
            _serviceBaseUrl = serviceBaseUrl;

        }

        #region city weather CSV

        protected string GetCVSContect(IList<CityWeatherModel> models)
        {
            var sb = new StringBuilder();

            using(var writer = new StringWriter(sb))
            {
                CsvExportWorker.ExportCsvFile(writer, models, GetWeatherValues, GetWeatherHeader);

                writer.Flush();
            }

            return sb.ToString();
        }

        string[] GetWeatherHeader()
        {
            return new[] { "Date", "Sunrise", "Sunset", "Minimum Temperature", "Maximum Temperature", "Pressure", "Humidity", "Wind Speed" };
        }

        string[] GetWeatherValues(CityWeatherModel model)
        {
            return new[]
            {  model.Dt.FromUnixTime().ToString("yyyy-MM-dd"),
               model.Sys?.Sunrise.FromUnixTime().ToString("HH:mm:ss"),
               model.Sys?.Sunset.FromUnixTime().ToString("HH:mm:ss"),
               model.Main?.Temp_min.ToString(CultureInfo.InvariantCulture),
               model.Main?.Temp_max.ToString(CultureInfo.InvariantCulture),
               model.Main?.Pressure.ToString(CultureInfo.InvariantCulture),
               model.Main?.Humidity.ToString(CultureInfo.InvariantCulture),
               model.Wind?.Speed.ToString(CultureInfo.InvariantCulture),
            };
        }

        #endregion

        #region ICityWeatherClient

        public string GetCityWeatherCSV(Position coords)
        {
            var requestUrl = $"{_serviceBaseUrl}/?position={coords.ToString()}";

            var jsonData = _dataRequest.GetDataString(requestUrl);

            if (string.IsNullOrEmpty(jsonData)) return null;

            var json = new JavaScriptSerializer().Deserialize<CityWeatherModel>(jsonData);

            if (json == null) return null;

            var csvContent = GetCVSContect(new List<CityWeatherModel> { json });

            return csvContent;
        }

        #endregion
    }
}
