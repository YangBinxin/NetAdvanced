using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Mvc.Models
{
    public class ZouNi
    {
        [Key]
        public int A { get; set; }
        public string B { get; set; }
    }
}
