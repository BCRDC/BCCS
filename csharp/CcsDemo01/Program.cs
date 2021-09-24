using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

using CcsDemo.Configurations;

namespace CcsDemo
{
    class Program
    {

        static Program()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            };

        }

        static void Main(string[] args)
        {
            // HistoryClient.GetMessageHistory();

            NotificationClient.Send();
        }
    }
}
