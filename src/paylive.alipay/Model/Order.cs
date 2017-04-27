using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paylive.alipay.Model
{
    public class Order
    {
        public string productCode = "QUICK_WAP_PAY";
        public string body = "购买测试商品0.01元";
        public string subject = "测试";
        public string out_trade_no = "2017224183319262";
        public string total_amount = "0.01";
        public string timeout_express = "1m";
    }
}
