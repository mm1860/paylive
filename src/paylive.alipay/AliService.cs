using System;
using paylive.core;

namespace paylive.alipay
{
    public class AliService : Plug
    {
        public AliService()
        {
            Url = "/ali/index";
            Ua = "alipay";
            Descrpion = "alipay";
        }

        public override string Run()
        {
            throw new NotImplementedException();
        }
    }
}