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

        public void GetCompanyLists()
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
            SearchResult.httpResult = (int)response.StatusCode;

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
                {
                    SearchResult.trackingDetail.Add(new TrackingDetail()
                    {
                        transKind = i["trans_kind"].InnerText,
                        transTelno = i["trans_telno"].InnerText,
                        transTime = i["trans_time"].InnerText,
                        transWhere = i["trans_where"].InnerText,
                        manName = i["manName"].InnerText,
                        level = Convert.ToInt32(i["level"].InnerText)
                    });
                }
                SearchResult.trackingDetail.OrderBy(x => x.level);
            }
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
        public List<TrackingDetail> trackingDetail = new List<TrackingDetail>();
        public int httpResult;
    }
    public class TrackingDetail
    {
        public string manName;
        public string transKind;
        public string transTelno;
        public string transTime;
        public string transWhere;
        public int level;
    }
}
