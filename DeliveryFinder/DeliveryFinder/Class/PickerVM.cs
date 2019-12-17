using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryFinder
{
    public class PickerVM
    {
        SmartDelivery DeliveryInfo = new SmartDelivery();
        public List<string> CompanyList => DeliveryInfo.GetCompanyValues;
        public PickerVM()
        {
        }
    }
}
