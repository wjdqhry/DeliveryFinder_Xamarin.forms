using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace DeliveryFinder
{
    public class SmartDelivery
    {
        private Dictionary<string, string> DataList = new Dictionary<string, string>();
        private RestClient restClient;
        public List<string> GetCompanyValues => DataList.Select(x => x.Key).ToList();

        public SmartDelivery()
        {
            restClient = new RestClient(DeliveryAPIData.baseUrl);
            GetCompanyLists();
        }

        private void GetCompanyLists()
        {
            var request = new RestRequest("/api/v1/companylist", Method.GET);
            request.AddQueryParameter("t_key", DeliveryAPIData.key);

            var response = restClient.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response.Content);

                DataList = doc["Companies"].ChildNodes.Cast<XmlNode>()
                    .Select(s => new KeyValuePair<string, string>(s["Name"].InnerText, s["Code"].InnerText)).ToDictionary(s => s.Key, s => s.Value);
            }
        }

        public SearchResult InvoiceSearch(string code, string invoice)
        {
            SearchResult SearchResult = new SearchResult();
            var request = new RestRequest("/api/v1/trackingInfo", Method.GET);

            request.AddQueryParameter("t_key", DeliveryAPIData.key);
            request.AddQueryParameter("t_code", DataList[code]);
            request.AddQueryParameter("t_invoice", invoice);

            var response = restClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response.Content);
                var subDoc = doc["tracking_info"];
                SearchResult.itemName = subDoc["item_name"].InnerText;
                SearchResult.reciverAddr = subDoc["reciver_addr"].InnerText;
                SearchResult.reciverName = subDoc["reciver_name"].InnerText;
                SearchResult.senderName = subDoc["sender_name"].InnerText;

                XmlNodeList trackingDetails = subDoc.GetElementsByTagName("tracking_details");
                foreach (XmlNode i in trackingDetails)
                    SearchResult.trackingDetails.Add(new Dictionary<string, string>{{ "배송 분류", i["trans_kind"].InnerText }, { "전화번호", i["trans_telno"].InnerText }
                                                                         , { "배송 시간", i["trans_time"].InnerText}, { "배송 위치", i["trans_where"].InnerText } });
                
            }
            SearchResult.ResultStatus = response.StatusCode;

            return SearchResult;
        }
    }
    public class SearchResult
    {
        //private string invoiceNum;
        public string itemName;
        public string reciverName;
        public string reciverAddr;
        public string senderName;
        public List<Dictionary<string, string>> trackingDetails = new List<Dictionary<string, string>>();
        public HttpStatusCode ResultStatus;
    }
}
