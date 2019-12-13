using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Test1.Model
{
    public class ReposeCommon
    {
        /// <summary>
        /// 状态码 
        /// </summary>
        public ResponseStatu code { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public string responseTime
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// 数据总量
        /// </summary>
        public string total { get; set; }
    }

    public enum ResponseStatu
    {
        success = 0,
        fail = 1
    }
}
