using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Mini_projet_reseaux
{
    internal class PageServer
    {
        private static void Main(string[] args)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("A more recent Windows version is required to use the HttpListener class.");
                return;
            }

            // Create a listener.
            HttpListener listener = new HttpListener();

            // Trap Ctrl-C and exit 
            Console.CancelKeyPress += delegate
            {
                listener.Stop();
                System.Environment.Exit(0);
            };

            // Add the prefixes.
            if (args.Length != 0)
            {
                foreach (string s in args)
                {
                    listener.Prefixes.Add(s);
                }
            }
            else
            {
                Console.WriteLine("Syntax error: the call must contain at least one web server url as argument");
            }
            listener.Start();
            foreach (string s in args)
            {
                Console.WriteLine("Listening for connections on " + s);
            }

            string jsonString = File.ReadAllText("../../urls.json");

            // Désérialiser le JSON en un objet
            var jsonDoc = JsonDocument.Parse(jsonString);

            // Obtenir le tableau de sites
            var sitesArray = jsonDoc.RootElement.GetProperty("sites").EnumerateArray();

            // Transformer le tableau en liste de chaînes de caractères
            List<string> websitesToCall = new List<string>();
            foreach (var site in sitesArray)
            {
                websitesToCall.Add(site.GetString());
            }

            var webRequests = new WebRequests(websitesToCall);

            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }

                HttpListenerResponse response = context.Response;

                if (request.Url.ToString() == "http://localhost:8080/serverStat")
                {
                    String server = webRequests.getMostFrequentServer();
                    String allServersUsage = webRequests.getServersUsage();
                    string responseString = "<!DOCTYPE html><html><head><title>Serveurs</title><style>h1{text-align:center;}</style></head><body><h1>Voici le serveur le plus utilise</h1><h2>" + server + "</h2><p><h3>Frequence d'utilisation</h3>" + allServersUsage + "</p></body></html>\r\n";
                    ;
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();

                }

                else if (request.Url.ToString() == "http://localhost:8080/ageStat")
                {
                    double av= webRequests.getAverageAge();
                    double standardDev = webRequests.getStandardDeviation();
                    string responseString = "<!DOCTYPE html><html><head><title>Ages</title><style>h1{text-align:center;}</style></head><body><h1>Age moyen</h1><h2>" + av + "</h2><p><h1>Ecart type</h1>" + standardDev + "</p></body></html>\r\n";
                    ;
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();

                }

                else if (request.Url.ToString() == "http://localhost:8080/xssStat")
                {
                    String xss = webRequests.getMostFrequentXssProtection();
                    String xssUsage = webRequests.getXssProtectUsage();
                    string responseString = "<!DOCTYPE html><html><head><title>Xss</title><style>h1{text-align:center;}</style></head><body><h1>Les statistiques liees au cross site scripting</h1><h2> Le type header xss-protection le plus present </h2>" + xss + "<h2>Et son utilisation </h2>"+xssUsage+"</p></body></html>\r\n";
                    ;
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();

                }

                else if (request.Url.ToString() == "http://localhost:8080/cacheControlStat")
                {
                    String cc = webRequests.getMostFrequentCacheControl();
                    String ccUsage = webRequests.getCacheControlUsage();
                    string responseString = "<!DOCTYPE html><html><head><title>Cache control</title><style>h1{text-align:center;}</style></head><body><h1>Les statistiques liees au cache control</h1><h2> Le type header cache control le plus present </h2>" + cc + "<h2>Et son utilisation </h2>" + ccUsage + "</p></body></html>\r\n";
                    ;
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();

                }


                // Obtain a response object.
                else { 
                    string filePath = "../../index.html";
                    string fileContents = File.ReadAllText(filePath);

                    //string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(fileContents);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();
                }

            }
            // Httplistener neither stop ...
            // listener.Stop();

            
        }
    }
}

    