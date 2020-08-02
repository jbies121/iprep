﻿using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text.Json;

namespace iprep
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static async Task<List<AIPDB_Check_Root>> AbuseIPDBCheck()
        {
            HttpResponseMessage resp; //--init response message obj
            
            //--set default headers for the httpclient. Might want to change this to be set by req
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "IPRep v1.0");

            //--init request message obj
            var req = new HttpRequestMessage(HttpMethod.Get, "https://api.abuseipdb.com/api/v2/check?ipAddress=118.25.6.39&maxAgeInDays=90&verbose=");
            req.Headers.Add("Key", "KEY_GOES_HERE");

            //--Send request async through httpclient
            resp = await client.SendAsync(req);
            var responseBody = await resp.Content.ReadAsStringAsync();

            //string strresp = "\"" + resp.ToString() + "\"";
            var repositories = JsonSerializer.Deserialize<List<AIPDB_Check_Root>>(responseBody);
            return repositories;

            //--Used when it was just an HttpClient obj without HttpRequestMessage obj. Second line for Json Deserialization
            //var streamTask = client.GetStreamAsync("https://api.abuseipdb.com/api/v2/check");
            //var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
            //return repositories;
        }

        public static async Task Main(string[] args)
        {
            var repositories = await AbuseIPDBCheck();

            foreach (var repo in repositories)
            {
                Console.WriteLine(repo.data);
                Console.WriteLine();
            }
        }
    }
}
