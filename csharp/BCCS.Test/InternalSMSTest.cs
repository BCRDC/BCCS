using BCCS.Library.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace BCCS.Test
{
    public class InternalSMSTest
    {
        private readonly ITestOutputHelper output;

        public InternalSMSTest(ITestOutputHelper outputHelper)
        {
            output = outputHelper;
        }

        private readonly MessageClient _client = new MessageClient();

        public readonly string _endpoint = "https://bccsprod-bak.chinaeast2.cloudapp.chinacloudapi.cn";

        //测试ccs账号名称
        public readonly string _testCcsAccount = "internalsms";
        //密钥名称
        public readonly string _keyName = "full";
        //密钥
        public readonly string _keyValue = "";
        //测试手机号
        public readonly string _testMobile = "18321676517";
        //下发扩展码，两位纯数字
        public readonly string _testExtend = "08";

        [Fact]
        public async Task SendSMSAsync()
        {
            var ret = await _client.SendMessageAsync(new Library.Model.NotifyRequestData
            {
                ExtendCode = "08",
                PhoneNumber = new List<string> { _testMobile },
                MessageBody = new Library.Model.MessageBody
                {
                    TemplateName = "jiwagtest",
                    TemplateParam = new Dictionary<string, string>
                    {
                        { "username", DateTime.UtcNow.AddHours(8).ToLongTimeString()},
                    }
                }

            }, new Library.Model.SMSCredential
            {
                AccountName = _testCcsAccount,
                Endpoint = _endpoint,
                KeyName = _keyName,
                KeyValue = _keyValue
            });

            output.WriteLine(ret);
            Assert.NotEmpty(ret);
        }


        [Fact]
        public async Task SendOtpAsync()
        {
            var ret = await _client.SendOtpMessageAsync(new Library.Model.OtpRequestData
            {
                CodeLength = "6",
                ExpireTime = "60",
                PhoneNumber = _testMobile,
                TemplateName = "azureadapply"

            }, new Library.Model.SMSCredential
            {
                AccountName = _testCcsAccount,
                Endpoint = _endpoint,
                KeyName = _keyName,
                KeyValue = _keyValue
            });

            output.WriteLine(ret);
            Assert.NotEmpty(ret);
        }

    }
}
