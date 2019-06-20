using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using CityServiceClient.CityWeather;
using CityList.Code;
using CityServiceClient.Common;
using CityServiceClient.DataRequest;

namespace CityList.EventReceivers.CityItemEventReceivers
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class CityItemEventReceivers : SPItemEventReceiver
    {
        ICityWeatherClient _cityWeatherClient;

        public CityItemEventReceivers()
        {
            _cityWeatherClient = new CityWeatherClient(
                new DataRequest(AppProperties.RetryCount, AppProperties.RetrtInterval),
                AppProperties.CityWeatherServiceUrl);
        }


        /// <summary>
        /// An item is being added.
        /// </summary>
        public override void ItemAdding(SPItemEventProperties properties)
        {
            base.ItemAdding(properties);

            var result = CityItemWorker.UpdateCityWeatherData(_cityWeatherClient, properties.Web, null, properties.AfterProperties);

            if (!result.Result)
            {
                properties.Status = SPEventReceiverStatus.CancelWithError;
                properties.ErrorMessage = result.Message;
            }

        }

        /// <summary>
        /// An item is being updated.
        /// </summary>
        public override void ItemUpdating(SPItemEventProperties properties)
        {
            base.ItemUpdating(properties);

            var itemDocRef = new SPFieldUrlValue((string)properties.ListItem[CityItemFields.DocumentRef]);

            var result = CityItemWorker.UpdateCityWeatherData(_cityWeatherClient, properties.Web, itemDocRef.Url, properties.AfterProperties);

            if (!result.Result)
            {
                properties.Status = SPEventReceiverStatus.CancelWithError;
                properties.ErrorMessage = result.Message;
            }

        }


        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
        }

        /// <summary>
        /// An item was updated.
        /// </summary>
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
        }

    }
}