using CityServiceClient.CityWeather;
using CityServiceClient.Common;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityList.Code
{
    public static class CityItemWorker
    {
        static readonly Encoding CsvEncoding = Encoding.UTF8;

        public static SPFieldUrlValue SaveCityDocument(SPWeb web, string cityName, string csvContent)
        {
            var libname = AppProperties.CityDocsLibName;
            var filename = $"{cityName}.csv";

            var doclib = (SPDocumentLibrary)web.Lists.TryGetList(libname);

            if (doclib == null) throw new ApplicationException(String.Format("Couldn't find library '{0}'", doclib));

            var content = CsvEncoding.GetBytes(csvContent);

            var result = new SPFieldUrlValue()
            {
                Description = string.Format(AppProperties.CityWeatherDocRefFormat, cityName),

                Url = Utilities.UploadFileIntoLibrary(doclib, filename, CsvEncoding.GetBytes(csvContent)),
            };

            return result;

        }

        public static  ServiceResult UpdateCityWeatherData(ICityWeatherClient cityWeatherClient, 
            SPWeb web, string docUrl, SPItemEventDataCollection cityItemProps)
        {
            var result = new ServiceResult();

            try
            {
                var itemName = (string)cityItemProps["Title"];

                var position = new Position()
                {
                    Lat = Decimal.Parse((string)cityItemProps[CityItemFields.Latitude]),
                    Lon = Decimal.Parse((string)cityItemProps[CityItemFields.Longitude]),
                };

                var csvContent = cityWeatherClient.GetCityWeatherCSV(position);

                //check doc hash
                if (string.IsNullOrEmpty(docUrl) || 
                    csvContent.GetHashCodeSafe() != GetCityItemWeatherContent(web, docUrl).GetHashCodeSafe())
                {
                    cityItemProps[CityItemFields.DocumentRef] = SaveCityDocument(web, itemName, csvContent);
                }

                result.Result = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            return result;

        }

        public static string GetCityItemWeatherContent(SPWeb web, string docurl)
        {
            var content = Utilities.GetFileContent(web, docurl);

            return CsvEncoding.GetString(content);
        }

    }
}
