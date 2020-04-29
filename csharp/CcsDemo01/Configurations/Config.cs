using System;
using System.Collections.Generic;
using System.Text;

namespace CcsDemo.Configurations
{
    public class Config
    {
        public readonly static string _endpoint = "https://bluecloudccs.21vbluecloud.com/services/sms/messages?api-version=2018-10-01";


        public readonly static string _txtEndpoint = "http://bluecloudccs.21vbluecloud.com/services/sms/txts?api-version=2018-10-01";
        //测试ccs账号名称
        public readonly static string _testCcsAccount = "internalsms";
        //密钥名称
        public readonly static string _keyName = "full";
        //密钥
        public readonly static string _keyValue = "";
        //测试模板名称
        public readonly static string _testTemplateName = "demo";
        //测试手机号
        public readonly static string _testMobile = "";
        //下发扩展码，两位纯数字
        public readonly static string _testExtend = "08";
    }

    public class HistoryConfig
    {

        private readonly static string _endpoint = "https://bluecloudccs.21vbluecloud.com/services/Sms/aggregations/perMessageV2?continuationToken=&count=10&startTime={0}&endTime={1}&api-version=2018-10-01";


        public static string BuildHistoryUrl(long start, long end)
        {
            return String.Format(_endpoint, start, end);
        }
    }
}
