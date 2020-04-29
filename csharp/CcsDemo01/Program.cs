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
        
        static void Main(string[] args)
        {
            HistoryClient.GetMessageHistory();
        }
    }
}
