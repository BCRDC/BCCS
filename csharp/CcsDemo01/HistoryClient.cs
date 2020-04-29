using CcsDemo.Configurations;
using CcsDemo.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CcsDemo
{
    internal class HistoryClient
    {

        public HistoryClient()
        {

        }


        public static void GetMessageHistory()
        {
            var now = DateTimeOffset.UtcNow;

            var stattime = now.AddMinutes(-5).ToUnixTimeSeconds();
            var endTime = now.ToUnixTimeSeconds();


            var req = (HttpWebRequest)WebRequest.Create(HistoryConfig.BuildHistoryUrl(stattime, endTime));
            req.ContentType = "application/json";
            req.Method = "GET";
            req.ContentLength = 0;

            //ccs account name
            req.Headers.Add("Account", Config._testCcsAccount);

            //获取token
            var token = AuthorizationHelper.CreateSASToken(Config._keyValue, Config._keyName, TimeSpan.FromSeconds(30));
            req.Headers.Add("Authorization", token);

            var webResponse = req.GetResponse();

            using (var stream = webResponse.GetResponseStream())
            {
               var json =  JSON.StreamAsJson(stream);
                Console.WriteLine(json);
            }
                Console.WriteLine("Send Success!");
        }
    }
}
