using System;
using System.Collections.Generic;
using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Util;
using Com.Alipay.Domain;
using Com.Alipay.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using paylive.alipay.AlipaySdk;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace paylive.alipay.Controllers
{
    public class AliController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var builder = BuildPayContent();
            var request = new AlipayTradeWapPayRequest();
            request.BizContent = JsonConvert.SerializeObject(builder);


            string apiVersion = null;

            if (!string.IsNullOrEmpty(request.GetApiVersion()))
                apiVersion = request.GetApiVersion();
            else
                apiVersion = "1.0";

            // 添加协议级请求参数
            var txtParams = new AopDictionary(request.GetParameters());

            txtParams.Add("method", request.GetApiName());
            txtParams.Add("version", apiVersion);
            txtParams.Add("app_id", Config.app_id);
            txtParams.Add("format", "json");
            txtParams.Add("timestamp", DateTime.Now);
            txtParams.Add("auth_token", null);
            txtParams.Add("sign_type", Config.sign_type);
            txtParams.Add("terminal_type", request.GetTerminalType());
            txtParams.Add("terminal_info", request.GetTerminalInfo());
            txtParams.Add("prod_code", request.GetProdCode());
            txtParams.Add("charset", Config.charset);


            if (!string.IsNullOrEmpty(request.GetNotifyUrl()))
                txtParams.Add("notify_url", request.GetNotifyUrl());

            // 添加签名参数
            txtParams.Add("sign",
                AlipaySignature.RSASign(txtParams, Config.merchant_private_key, Config.charset, false, Config.sign_type));

            //建立请求
            var sHtmlText = Submit.BuildRequest(txtParams, "get", "确认");


            return View("Index", sHtmlText);
        }

        /// <summary>
        ///     构造支付请求数据
        /// </summary>
        /// <returns>请求数据集</returns>
        private AlipayTradePayContentBuilder BuildPayContent()
        {
            //线上联调时，请输入真实的外部订单号。
            var out_trade_no = "1";

            //扫码枪扫描到的用户手机钱包中的付款条码
            var builder = new AlipayTradePayContentBuilder();

            //收款账号
            builder.seller_id = Config.seller_id;
            //订单编号
            builder.out_trade_no = out_trade_no;
            //支付场景，无需修改
            builder.scene = "bar_code";
            //订单总金额
            builder.total_amount = "1";

            //订单名称
            builder.subject = "1";
            //自定义超时时间
            builder.timeout_express = "2m";
            //订单描述
            builder.body = "";
            //门店编号，很重要的参数，可以用作之后的营销
            builder.store_id = "test store id";
            //操作员编号，很重要的参数，可以用作之后的营销
            builder.operator_id = "test";


            //传入商品信息详情
            var gList = new List<GoodsInfo>();

            var goods = new GoodsInfo();
            goods.goods_id = "304";
            goods.goods_name = "goods#name";
            goods.price = "0.01";
            goods.quantity = "1";
            gList.Add(goods);
            builder.goods_detail = gList;

            return builder;
        }
    }
}