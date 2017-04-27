using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paylive.core
{
    public abstract class Plug
    {
        /// <summary>
        /// MVC页面访问地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Descrpion { get; set; }


        /// <summary>
        /// 浏览器类型
        /// </summary>
        public string Ua { get; set; }

        /// <summary>
        /// 支付过程并返回同步结果
        /// </summary>
        /// <returns></returns>
        public abstract string Run();
    }
}
