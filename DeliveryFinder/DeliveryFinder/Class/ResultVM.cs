using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DeliveryFinder
{
    public class ResultVM
    {
        SearchResult Result;
        public string ItemName => Result.itemName;
        public string ReciverName => Result.reciverName;
        public string ReciverAddr => Result.reciverAddr;
        public string SenderName => Result.senderName;
        public int HttpResult => Result.httpResult;
        public List<TrackingDetail> Details => Result.trackingDetail;


        public ResultVM(SearchResult searchResult)
        {
            Result = searchResult;
        }
    }
}
