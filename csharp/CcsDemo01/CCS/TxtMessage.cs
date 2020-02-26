using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CcsDemo.CCS
{

    public class TxtMessage
    {
        /// <summary>
        /// 接收手机号
        /// </summary>
        [JsonProperty("phoneNumber")]
        public List<string> PhoneNumber { get; set; }
        /// <summary>
        /// 下发扩展码。2 位纯数字
        /// </summary>
        [JsonProperty("extend")]
        public string ExtendCode { get; set; }

        [JsonProperty("messageBody")]
        public TextBody MessageBody { get; set; }
    }

    public class TextBody
    {
        [JsonProperty(PropertyName = "candidateSig", Required = Required.Default)]
        public string CandidateSig { get; set; }

        [JsonProperty(PropertyName = "text", Required = Required.Always)]
        public string Text { get; set; }
    }
}
