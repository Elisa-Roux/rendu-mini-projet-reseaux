using System;
using System.Collections;
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

        //Opérations de traitement des données

        public String getMostFrequentServer()
        {
            List<String> servers = getHeaderKeys("Server");
            return this.stats.getMostFrequent(servers);
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


        public String getServersUsage()
        {
            List<String> all = getHeaderKeys("Server");

            Dictionary<string, int> myDict = new Dictionary<string, int>();

            foreach (string s in all)
            {
                if (myDict.ContainsKey(s))
                    myDict[s]++;
                else
                    myDict[s] = 1;
            }

            string htmlList = "<ul>";

            foreach (KeyValuePair<string, int> pair in myDict)
            {
                htmlList += "<li>" + pair.Key + " : " + pair.Value.ToString() + "</li>";
            }

            htmlList += "</ul>";

            return htmlList;
        }
    }
}
