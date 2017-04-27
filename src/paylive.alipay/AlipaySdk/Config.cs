using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paylive.alipay.AlipaySdk
{
    public  class Config
    {
        //商户id。
        public static string seller_id = "********";

        //应用ID,您的APPID。
        public static string app_id= "*****";

        //商户私钥，您的原始格式RSA私钥
        public static string  merchant_private_key = "********";

        //异步通知地址
        public static string notify_url  =  "http://工程公网访问地址/alipay.trade.wap.pay-PHP-UTF-8/notify_url.php";

        //同步跳转
        public static string return_url  =  "http://mitsein.com/alipay.trade.wap.pay-PHP-UTF-8/return_url.php";

        //编码格式
        public static string charset  =  "UTF-8";

        //签名方式
        public static string sign_type = "RSA2";

        //支付宝网关
        public static string gatewayUrl  =  "https://openapi.alipaydev.com/gateway.do";

        //支付宝公钥,查看地址：https://openhome.alipay.com/platform/keyManage.htm 对应APPID下的支付宝公钥。
        public static string alipay_public_key =
            "*********";
    }
}
