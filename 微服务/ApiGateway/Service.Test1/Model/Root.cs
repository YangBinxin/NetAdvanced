using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Test1.Model
{
    public class Tmall_exchange_receive_get_response
    {
        /// <summary>
        /// 
        /// </summary>
        public string has_next { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int page_results { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total_results { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string request_id { get; set; }
    }

    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public Tmall_exchange_receive_get_response tmall_exchange_receive_get_response { get; set; }
    }
}
