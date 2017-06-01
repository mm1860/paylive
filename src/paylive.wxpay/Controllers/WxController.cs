using System;
using System.Net;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using paylive.core;
using paylive.wxpay.Services;
using QRCoder;
using WxPayAPI;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace wifidogAuthServer.Controllers
{
    public class WxController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return Content("wechat pay!");
        }

        public async Task<IActionResult> NativePay()
        {
            Log.Info(GetType().ToString(), "page load");
            var nativePay = new NativePay();
            var url = await nativePay.GetPayUrl("123456789");
            var imageUrl = "MakeQrCode?data=" + UrlEncoder.Default.Encode(url);
            return View("NativePay", imageUrl);
        }

        public FileContentResult MakeQrCode(string data)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new BitmapByteQRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);
            return File(qrCodeImage, "image/gif");
        }



        /// <summary>
        /// openid用于调用统一下单接口
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// access_token用于获取收货地址js函数入口参数
        /// </summary>
        public string access_token { get; set; }

        /**
        * 
        * 网页授权获取用户基本信息的全部过程
        * 详情请参看网页授权获取用户基本信息：http://mp.weixin.qq.com/wiki/17/c0f37d5704f0b64713d5d2c37b468d75.html
        * 第一步：利用url跳转获取code
        * 第二步：利用code去获取openid和access_token
        * 
        */
        public async Task<IActionResult> JsApiPay(string code = "")
        {
            if (!string.IsNullOrEmpty(code))
            {
                //获取code码，以获取openid和access_token
                Log.Debug(this.GetType().ToString(), "Get code : " + code);
                await GetOpenidAndAccessTokenFromCodeAsync(code);
            }
            else
            {
                //构造网页授权获取code的URL
                string host = Request.Host.Value;
                string path = Request.Path;
                string redirect_uri = WebUtility.UrlEncode("http://" + host + path);
                WxPayData authorizedata = new WxPayData();
                authorizedata.SetValue("appid", WxPayConfig.APPID);
                authorizedata.SetValue("redirect_uri", redirect_uri);
                authorizedata.SetValue("response_type", "code");
                authorizedata.SetValue("scope", "snsapi_base");
                authorizedata.SetValue("state", "STATE" + "#wechat_redirect");
                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?" + authorizedata.ToUrl();
                Log.Debug(this.GetType().ToString(), "Will Redirect to URL : " + url);
                try
                {
                    //触发微信返回code码         
                    Response.Redirect(url);//Redirect函数会抛出ThreadAbortException异常，不用处理这个异常
                }
                catch (Exception ex)
                {
                }
            }

            //获取openid失败
            if (string.IsNullOrEmpty(openid)){
                Log.Debug(this.GetType().ToString(), "Get openid : " + openid);
                return View("JsApiPay", string.Empty);
            }
                

            //jsapipay统一下单
            WxPayData data = new WxPayData();
            data.SetValue("body", "test");
            data.SetValue("attach", "test");
            data.SetValue("out_trade_no", WxPayApi.GenerateOutTradeNo());
            data.SetValue("total_fee", 100);
            data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            data.SetValue("goods_tag", "test");
            data.SetValue("trade_type", "JSAPI");
            data.SetValue("openid", openid);

            WxPayData result = await WxPayApi.UnifiedOrder(data);
            if (!result.IsSet("appid") || !result.IsSet("prepay_id") || result.GetValue("prepay_id").ToString() == "")
            {
                Log.Error(this.GetType().ToString(), "UnifiedOrder response error!");
                //统一下单失败
                return View("JsApiPay", string.Empty);
            }

            WxPayData jsApiParam = new WxPayData();
            jsApiParam.SetValue("appId", result.GetValue("appid"));
            jsApiParam.SetValue("timeStamp", WxPayApi.GenerateTimeStamp());
            jsApiParam.SetValue("nonceStr", WxPayApi.GenerateNonceStr());
            jsApiParam.SetValue("package", "prepay_id=" + result.GetValue("prepay_id"));
            jsApiParam.SetValue("signType", "MD5");
            jsApiParam.SetValue("paySign", jsApiParam.MakeSign());

            string parameters = jsApiParam.ToJson();
            Log.Debug(this.GetType().ToString(), "JsApiPay parameters : " + parameters);
            return View("JsApiPay", parameters);
        }

        /**
	    * 
	    * 通过code换取网页授权access_token和openid的返回数据，正确时返回的JSON数据包如下：
	    * {
	    *  "access_token":"ACCESS_TOKEN",
	    *  "expires_in":7200,
	    *  "refresh_token":"REFRESH_TOKEN",
	    *  "openid":"OPENID",
	    *  "scope":"SCOPE",
	    *  "unionid": "o6_bmasdasdsad6_2sgVt7hMZOPfL"
	    * }
	    * 其中access_token可用于获取共享收货地址
	    * openid是微信支付jsapi支付接口统一下单时必须的参数
        * 更详细的说明请参考网页授权获取用户基本信息：http://mp.weixin.qq.com/wiki/17/c0f37d5704f0b64713d5d2c37b468d75.html
        * @失败时抛异常WxPayException
	    */
        public async Task GetOpenidAndAccessTokenFromCodeAsync(string code)
        {
            try
            {
                //构造获取openid及access_token的url
                WxPayData data = new WxPayData();
                data.SetValue("appid", WxPayConfig.APPID);
                data.SetValue("secret", WxPayConfig.APPSECRET);
                data.SetValue("code", code);
                data.SetValue("grant_type", "authorization_code");
                string url = "https://api.weixin.qq.com/sns/oauth2/access_token?" + data.ToUrl();

                //请求url以获取数据
                string result = await HttpService.Get(url);
                Log.Debug(this.GetType().ToString(), "GetOpenidAndAccessTokenFromCode response : " + result);

                var jd = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonData>(result);

                if (jd.errcode == 0)
                {
                    access_token = jd.access_token;

                    //获取用户openid
                    openid = jd.openid;
                }


                Log.Debug(this.GetType().ToString(), "Get openid : " + openid);
                Log.Debug(this.GetType().ToString(), "Get access_token : " + access_token);
            }
            catch (Exception ex)
            {
                Log.Error(this.GetType().ToString(), ex.ToString());
            }
        }

        /**
        * 
        * 支付完成交易通知
        * 
        */
        public async Task Nodify()
        {
            ResultNotify resultNotify = new ResultNotify(this.HttpContext);
            await resultNotify.ProcessNotifyAsync();
        }
    }
}