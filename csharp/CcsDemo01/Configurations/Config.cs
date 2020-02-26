using System;
using System.Collections.Generic;
using System.Text;

namespace CcsDemo.Configurations
{
    public class Config
    {
        public readonly static string _endpoint = "http://cefdevcluster.chinaeast2.cloudapp.chinacloudapi.cn/services/sms/messages?api-version=2018-10-01";

        public readonly static string _txtEndpoint = "http://cefdevcluster.chinaeast2.cloudapp.chinacloudapi.cn/services/sms/txts?api-version=2018-10-01";
        //测试ccs账号名称
        public readonly static string _testCcsAccount = "loadtestAcc";
        //密钥名称
        public readonly static string _keyName = "full";
        //密钥
        public readonly static string _keyValue = "M7Pd8DkxeuAMJ3/i6SZ6byEStqpZsrKrc7Had8vnJdQ=";
        //测试模板名称
        public readonly static string _testTemplateName = "loadtest";
        //测试手机号
        public readonly static string _testMobile = "13700000000";
        //下发扩展码，两位纯数字
        public readonly static string _testExtend = "08";
    }
}
