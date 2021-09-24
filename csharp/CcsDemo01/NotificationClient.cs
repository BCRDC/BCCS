using CcsDemo.Configurations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CcsDemo
{
    class NotificationClient
    {


        public static void Send()
        {
            try
            {
                //请求数据
                var requestData = new RequestData();
                requestData.PhoneNumber = new List<string>() { Config._testMobile };
                requestData.ExtendCode = Config._testExtend;
                requestData.MessageBody = new MessageBody();
                requestData.MessageBody.TemplateName = Config._testTemplateName;
                requestData.MessageBody.TemplateParam = new Dictionary<string, string>()
                {
                    { "username", "1"},
                    { "time", DateTime.UtcNow.AddHours(8).ToLongTimeString()},
                    { "amount", "222"}
                };

                var req = (HttpWebRequest)WebRequest.Create(Config._endpoint);
                req.ContentType = "application/json";
                req.Method = "POST";
                string jsonPayload = JsonConvert.SerializeObject(requestData);
                var data = Encoding.UTF8.GetBytes(jsonPayload);
                req.ContentLength = data.Length;
                using (var stream = req.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                //ccs account name
                req.Headers.Add("Account", Config._testCcsAccount);

                //获取token
                var token = AuthorizationHelper.CreateSASToken(Config._keyValue, Config._keyName, TimeSpan.FromSeconds(30));
                req.Headers.Add("Authorization", token);

                var webResponse = req.GetResponse();
                using (var stream = webResponse.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string text = reader.ReadToEnd();
                    Console.WriteLine(text);
                }

                Console.WriteLine("Send Success!");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Send Error:" + ex.Message);
                Console.ReadLine();
            }
        }
    }
}
