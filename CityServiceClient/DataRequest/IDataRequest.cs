using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityServiceClient.DataRequest
{
    public interface IDataRequest
    {
        string GetDataString(string requestUrl);
    }
}
