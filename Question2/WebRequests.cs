using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Mini_projet_reseaux
{
    internal class WebRequests
    {
        HttpClient client = new HttpClient();

        List<String> websitesToCall;

        Dictionary<String, Dictionary<String, String>> allHeaders;

        Stats stats;



        public WebRequests(List<String> websitesToCall)
        {
            this.websitesToCall = websitesToCall;
            this.allHeaders = storeAllHeaders();
            this.stats = new Stats();
        }

        public Dictionary<String, Dictionary<String, String>> storeAllHeaders()
        {
            var allHeaders = new Dictionary<String, Dictionary<String, String>>();

            foreach (var website in websitesToCall)
            {
                
                var headers = new Dictionary<String, String>();
                var request = new HttpRequestMessage(HttpMethod.Head, website);
                
                
                var response = client.SendAsync(request).Result;
                

                foreach (var header in response.Headers)
                {
                    headers.Add(header.Key, header.Value.First());
                }

                allHeaders.Add(website,headers);
            }

            return allHeaders;
        }

        public List<String> getHeaderKeys(string key)
        {
            List<String> list = new List<String>();
            foreach (var siteKey in this.allHeaders)
            { 
                if (siteKey.Value.TryGetValue(key, out var vals))
                {
                    list.Add(vals);
                }
            }
            return list;
        }

        public Double getAverageAge()
        {
            List<String> l = getHeaderKeys("age");

            List<double> convertedList = new List<double>();

            foreach(var key in l)
            {
                Console.WriteLine(Double.Parse(key));
                convertedList.Add(Double.Parse(key));
            }
            if (l.Count > 0)
                return this.stats.getAverage(convertedList);
            else
                return 0;
        }

        public Double getStandardDeviation()
        {
            List<String> l = getHeaderKeys("age");

            List<double> convertedList = convertToDouble(l);
            if (l.Count > 0)
                return this.stats.getAverage(convertedList);
            else
                return 0;
        }

        public List<Double> convertToDouble(List<String> list)
        {
            List<double> convertedList = new List<double>();

            foreach (var key in list)
            {
                Console.WriteLine(Double.Parse(key));
                convertedList.Add(Double.Parse(key));
            }

            return convertedList;
        }







    }
}
