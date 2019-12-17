using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryFinder
{
    public class ResultVM
    {
        SmartDelivery DeliveryData = new SmartDelivery();
        SearchResult Result;

        string ItemName { get; set; }
        string ReciverName { get; set; }
        string ReciverAddr { get; set; }
        string SenderName { get; set; }
        int HttpResult { get; set; }
        List<TrackingDetail> Details { get; set; }


        public ResultVM(string company, string invoice)
        {
            Result = DeliveryData.InvoiceSearch(company, invoice);

            ItemName = Result.itemName;
            ReciverName = Result.reciverName;
            ReciverAddr = Result.reciverAddr;
            SenderName = Result.senderName;
            HttpResult = Result.httpResult;
            Details = Result.trackingDetail;
        }
    }
}
