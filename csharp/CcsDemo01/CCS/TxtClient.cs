using CcsDemo.Configurations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CcsDemo.CCS
{
    public class TxtClient
    {
        private readonly HttpClient _client = new HttpClient();


        public async Task<string> SendTxtAsync(string json)
        {
            var token = AuthorizationHelper.CreateSASToken(Config._keyValue, Config._keyName, TimeSpan.FromSeconds(30));


            var request = new HttpRequestMessage(HttpMethod.Post, $"{Config._txtEndpoint}")
            {
                Headers = {
                    { HttpRequestHeader.Accept.ToString(), "application/json" },
                    { "MS-RequestId", Guid.NewGuid().ToString() },
                    { "Account", Config._testCcsAccount},
                    { "Authorization", token}
                },
                Content = new StringContent(json, Encoding.UTF8)

            };


            var response = await _client.SendAsync(request);

            var ret = await response.Content.ReadAsStringAsync();

            return ret;
        }


        public string GetBody()
        {
            var requestData = new TxtMessage();
            requestData.PhoneNumber = new List<string>() { "18321676517" };
            requestData.ExtendCode = Config._testExtend;
            requestData.MessageBody = new TextBody();
            requestData.MessageBody.CandidateSig = "上海蓝云";
            requestData.MessageBody.Text = "nihao xxx";

            string jsonPayload = JsonConvert.SerializeObject(requestData);
            return jsonPayload;
        }
    }
}
