using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace paylive.Console.DbContext
{
    /// <summary>
    /// 发短信记录表
    /// </summary>
    [Table("SmsQu")]
    public class SmsQu
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 发送的消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public string Receivers { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Completed { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
