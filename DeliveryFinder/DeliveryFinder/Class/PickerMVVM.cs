using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryFinder
{
    public class PickerMVVM
    {
        SmartDelivery DeliveryInfo = new SmartDelivery();
        public List<string> CompanyList => DeliveryInfo.GetCompanyValues;
        public PickerMVVM()
        {
        }
    }
}
