using CcsDemo.Configurations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CcsDemo.CCS
{
    public class MessageClient
    {
        private readonly HttpClient _client = new HttpClient();


        public  async Task<string> SendMessageAsync()
        {

            var requestData = new RequestData();
            requestData.PhoneNumber = new List<string>() { Config._testMobile };
            requestData.ExtendCode = Config._testExtend;
            requestData.MessageBody = new MessageBody();
            requestData.MessageBody.TemplateName = Config._testTemplateName;
            requestData.MessageBody.TemplateParam = new Dictionary<string, string>();

            var token = AuthorizationHelper.CreateSASToken(Config._keyValue, Config._keyName, TimeSpan.FromSeconds(30));


            string jsonPayload = JsonConvert.SerializeObject(requestData);

            var request = new HttpRequestMessage(HttpMethod.Post, $"{Config._endpoint}")
            {
                Headers = {
                    { HttpRequestHeader.Accept.ToString(), "application/json" },
                    { "MS-RequestId", Guid.NewGuid().ToString() },
                    {  "Account", Config._testCcsAccount},
                    {  "Authorization", token}
                },
                Content = new StringContent(jsonPayload, Encoding.UTF8)
                
            };


            var response = await _client.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return json;
        }



        public async Task<string> SendMessageAsync(RequestData data)
        {
            var token = AuthorizationHelper.CreateSASToken(Config._keyValue, Config._keyName, TimeSpan.FromSeconds(30));

            string jsonPayload = JsonConvert.SerializeObject(data);

            var request = new HttpRequestMessage(HttpMethod.Post, $"{Config._endpoint}")
            {
                Headers = {
                    { HttpRequestHeader.Accept.ToString(), "application/json" },
                    { "MS-RequestId", Guid.NewGuid().ToString() },
                    {  "Account", Config._testCcsAccount},
                    {  "Authorization", token}
                },
                Content = new StringContent(jsonPayload, Encoding.UTF8)

            };


            var response = await _client.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return json;
        }


        public async Task<string> SendMessageAsync(string json)
        {
            var token = AuthorizationHelper.CreateSASToken(Config._keyValue, Config._keyName, TimeSpan.FromSeconds(30));


            var request = new HttpRequestMessage(HttpMethod.Post, $"{Config._endpoint}")
            {
                Headers = {
                    { HttpRequestHeader.Accept.ToString(), "application/json" },
                    { "MS-RequestId", Guid.NewGuid().ToString() },
                    {  "Account", Config._testCcsAccount},
                    {  "Authorization", token}
                },
                Content = new StringContent(json, Encoding.UTF8)

            };


            var response = await _client.SendAsync(request);

            var ret = await response.Content.ReadAsStringAsync();

            return ret;
        }


        public string GetBody()
        {
            var requestData = new RequestData();
            requestData.PhoneNumber = new List<string>() { Config._testMobile };
            requestData.ExtendCode = Config._testExtend;
            requestData.MessageBody = new MessageBody();
            requestData.MessageBody.TemplateName = Config._testTemplateName;
            requestData.MessageBody.TemplateParam = new Dictionary<string, string>();

            string jsonPayload = JsonConvert.SerializeObject(requestData);
            return jsonPayload;
        }


        public async Task BulkSendAsync(Func<Task> tsk, int amount)
        {
            var tsks = (new uint[amount]).Select(e => tsk());
            await Task.WhenAll(tsks);
        }
    }
}
