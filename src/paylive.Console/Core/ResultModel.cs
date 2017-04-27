using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace paylive.Console.Core
{
    public class ResultModel<T>
    {
        public string rc { get; set; }
        public T rv { get; set; }
    }

    /// <summary>
    /// 添加好友
    /// </summary>
    public class BuddyBody
    {
        public string uid { get; set; }
    }

    /// <summary>
    /// 心跳包
    /// </summary>
    public class ConnectBody
    {
        public string DataType { get; set; }
        public BuddyBody Data { get; set; }
    }
}
