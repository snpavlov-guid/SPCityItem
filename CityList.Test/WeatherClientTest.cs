using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CityList.Test.Properties;
using System.Text;
using CityServiceClient.CityWeather;
using CityList.Test.Code;

namespace CityList.Test
{
    [TestClass]
    public class WeatherClientTest
    {
        [TestMethod]
        public void WeatherCSVHeaderTest()
        {
            var cityWeatherClient = new CityWeatherClient(new TestDataRequest(Resources.Weather_RespTest_01), "");

            var csvContent = cityWeatherClient.GetCityWeatherCSV(new Position());

            var matches = Validations.WeatherHeaderExp.Matches(csvContent);

            Assert.AreEqual(1, matches.Count);

        }

        [TestMethod]
        public void WeatherSingleLineTest()
        {
            var cityWeatherClient = new CityWeatherClient(new TestDataRequest(Resources.Weather_RespTest_01), "");

            var csvContent = cityWeatherClient.GetCityWeatherCSV(new Position());

            var matches = Validations.WeatherLineExp.Matches(csvContent);

            Assert.AreEqual(1, matches.Count);

        }

        [TestMethod]
        public void WeatherEmptyRespTest()
        {
            var cityWeatherClient = new CityWeatherClient(new TestDataRequest(""), "");

            var csvContent = cityWeatherClient.GetCityWeatherCSV(new Position());

            Assert.IsNull(csvContent);
        }

        [TestMethod]
        public void WeatherEmptyModelTest()
        {
            var cityWeatherClient = new CityWeatherClient(new TestDataRequest("{}"), "");

            var csvContent = cityWeatherClient.GetCityWeatherCSV(new Position());

            var matches = Validations.WeatherHeaderExp.Matches(csvContent);

            Assert.AreEqual(1, matches.Count);

            matches = Validations.WeatherLineExp.Matches(csvContent);

            Assert.AreEqual(0, matches.Count);
        }
    }
}
