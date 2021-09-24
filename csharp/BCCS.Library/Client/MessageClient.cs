using BCCS.Library.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BCCS.Library.Client
{
    public class MessageClient
    {

        static MessageClient()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            };

        }


        private readonly HttpClient _client = new HttpClient();

        public MessageClient()
        {
            var handler = new HttpClientHandler();

                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
                    {
                        return true;
                    };

                _client = new HttpClient(handler);


        }


        public async Task<string> SendMessageAsync(NotifyRequestData data, SMSCredential credential)
        {
            var token = AuthorizationHelper.CreateSASToken(credential.KeyValue, credential.KeyName, TimeSpan.FromSeconds(30));

            string jsonPayload = JsonConvert.SerializeObject(data);

            var request = new HttpRequestMessage(HttpMethod.Post, $"{credential.Endpoint}/services/sms/messages?api-version=2018-10-01")
            {
                Headers = {
                    { HttpRequestHeader.Accept.ToString(), "application/json" },
                    { "MS-RequestId", Guid.NewGuid().ToString() },
                    {  "Account", credential.AccountName},
                    {  "Authorization", token}
                },
                Content = new StringContent(jsonPayload, Encoding.UTF8)

            };

            var response = await _client.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                return json;
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();

                return json;
            }

            
        }


        public async Task<string> SendOtpMessageAsync(OtpRequestData data, SMSCredential credential)
        {
            var token = AuthorizationHelper.CreateSASToken(credential.KeyValue, credential.KeyName, TimeSpan.FromSeconds(30));

            string jsonPayload = JsonConvert.SerializeObject(data);

            var request = new HttpRequestMessage(HttpMethod.Post, $"{credential.Endpoint}/services/otp/start?api-version=2018-10-01")
            {
                Headers = {
                    { HttpRequestHeader.Accept.ToString(), "application/json" },
                    { "MS-RequestId", Guid.NewGuid().ToString() },
                    {  "Account", credential.AccountName},
                    {  "Authorization", token}
                },
                Content = new StringContent(jsonPayload, Encoding.UTF8)

            };


            var response = await _client.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            return json;
        }


        public async Task BulkSendAsync(Func<Task> tsk, int amount)
        {
            var tsks = (new uint[amount]).Select(e => tsk());
            await Task.WhenAll(tsks);
        }
    }
}
