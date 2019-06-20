using CityServiceClient.DataRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityList.Test.Code
{
    public class TestDataRequest : IDataRequest
    {
        string _testContent;
        public TestDataRequest(string testContent)
        {
            _testContent = testContent;
        }

        public string GetDataString(string requestUrl)
        {
            return _testContent;
        }
    }
}
