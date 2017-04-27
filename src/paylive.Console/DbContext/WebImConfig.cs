using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace paylive.Console.DbContext
{
    /// <summary>
    /// 飞信发送基本配置表
    /// </summary>
    [Table("WebImConfig")]
    public class WebImConfig
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 当前用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 当前身份标示
        /// </summary>
        public string Ssid { get; set; }
    }
}
