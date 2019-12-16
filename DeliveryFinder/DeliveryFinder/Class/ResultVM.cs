using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryFinder.Class
{
    public class ResultVM
    {
        SmartDelivery DeliveryData = new SmartDelivery();
        public ResultVM(string company, string invoice)
        {
            SearchResult Result = DeliveryData.InvoiceSearch(company, invoice);
        }
    }
}
