﻿using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace iprep
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static async Task<AIPDB_Check_Root> AbuseIPDBCheck(string ip)
        {
            HttpResponseMessage resp; //--init response message obj

            //--set default headers for the httpclient. Might want to change this to be set by req
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "IPRep v1.0");

            //--build request uri
            string uri = "https://api.abuseipdb.com/api/v2/check?ipAddress=" + ip + "&maxAgeInDays=90&verbose=";

            //--init request message obj
            var req = new HttpRequestMessage(HttpMethod.Get, uri);
            req.Headers.Add("Key", "GET_YOUR_OWN");

            //--Send request async through httpclient
            resp = await client.SendAsync(req);
            var responseBody = await resp.Content.ReadAsStringAsync();

            AIPDB_Check_Root repositories = JsonConvert.DeserializeObject<AIPDB_Check_Root>(responseBody);

            return repositories;
        }

        public static async Task Main(string[] args)
        {
            string ip = null;
            string attr = null;

            if (args.Length != 0)
            {
                ip = args[0];
                if (args.Length > 1)
                {
                    attr = args[1];
                }
            }

            var repositories = await AbuseIPDBCheck(ip);

            switch (attr)
            {
                case "country":
                    Console.WriteLine(repositories.data.countryName);
                    break;
                case "confidence":
                    Console.WriteLine(repositories.data.abuseConfidenceScore);
                    break;
                case null:
                    Console.WriteLine(repositories.data);
                    break;
            }
            
        }
    }
}
