using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

using CcsDemo.Configurations;
using System.Threading.Tasks;
using CcsDemo.CCS;

namespace CcsDemo
{
    class Program
    {

        /*
         var client = new MessageClient();

            Task.Run(async () =>
            {
                try
                {
                    //请求数据
                    var ret = await client.SendMessageAsync();



                    Console.WriteLine($"Send Success: {ret}");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Send Error:" + ex.Message);
                    Console.ReadLine();
                }


            }).Wait();
             
             */

        static void Main(string[] args)
        {
            var client = new MessageClient();

            var txtClient = new TxtClient();

            Task.Run(async () =>
            {
                var body = client.GetBody();
                var txtbody = txtClient.GetBody();
                try
                {
                    //请求数据

                   // while (true)
                    {
                        await client.BulkSendAsync(async () =>
                        {
                            var ret = await client.SendMessageAsync(body);

                            Console.WriteLine($"Send Success: {ret}");

                        }, 100001);

                        await client.BulkSendAsync(async () =>
                        {
                            var ret = await txtClient.SendTxtAsync(txtbody);

                            Console.WriteLine($"Send txt Success: {ret}");

                        }, 1);


                        await Task.Delay(1000* 10);

                    }
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Send Error:" + ex.Message);
                    Console.ReadLine();
                }


            }).Wait();
        }

        static void Mainx(string[] args)
        {
            try
            {
                //请求数据
                var requestData = new RequestData();
                requestData.PhoneNumber = new List<string>() { Config._testMobile };
                requestData.ExtendCode = Config._testExtend;
                requestData.MessageBody = new MessageBody();
                requestData.MessageBody.TemplateName = Config._testTemplateName;
                requestData.MessageBody.TemplateParam = new Dictionary<string, string>();

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
                var token = AuthorizationHelper.CreateSASToken(Config._keyValue, Config. _keyName, TimeSpan.FromSeconds(30));
                req.Headers.Add("Authorization", token);

                var webResponse = req.GetResponse();
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
