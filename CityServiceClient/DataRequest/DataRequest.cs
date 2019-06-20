using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CityServiceClient.DataRequest
{
    public class DataRequest : IDataRequest
    {
        private int _retry = 1;
        private int _retryInterval = 0;

        public DataRequest(int retry = 3, int retryInterval = 500)
        {
            _retry = retry;
            _retryInterval = retryInterval;

        }

        #region Request

        protected async Task<string> GetResponse(string requestUrl)
        {
            var client = new HttpClient();

            var result = await client.GetAsync(requestUrl);

            return await result.Content.ReadAsStringAsync();
        }

        protected async Task<string> GetRetriedResponse(string requestUrl)
        {
            var ntry = 0;
            var response = "";

            while (ntry < _retry)
            {
                try
                {
                    response = await GetResponse(requestUrl);

                    break;
                }
                catch
                {
                    ntry++;

                    if (ntry == _retry) throw;

                    await Task.Delay(_retryInterval * ntry);
                }

            }

            return response;

        }

        #endregion

        #region IDataRequest
        public string GetDataString(string requestUrl)
        {
            return GetRetriedResponse(requestUrl).Result;
        }

        #endregion
    }
}
