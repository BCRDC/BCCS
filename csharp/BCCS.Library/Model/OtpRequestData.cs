using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCCS.Library.Model
{
    /// <summary>
    /// 
    /// </summary>
   public class OtpRequestData
    {
        [JsonProperty("candidateSig")]
        public string CandidateSig { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; } = "sms";

        [JsonProperty("codeLength")]
        public string CodeLength { get; set; }

        [JsonProperty("expireTime")]
        public string ExpireTime { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("templateName")]
        public string TemplateName { get; set; }
    }
}
