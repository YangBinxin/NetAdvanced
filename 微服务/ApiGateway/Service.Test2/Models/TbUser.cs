using System;
using System.Collections.Generic;

namespace Service.Test2.Models
{
    public partial class TbUser
    {
        public int UId { get; set; }
        public string ULoginName { get; set; }
        public string UPwd { get; set; }
        public string ULookPwd { get; set; }
        public string UTruename { get; set; }
        public string USex { get; set; }
        public DateTime? UBrithday { get; set; }
        public string ULastip { get; set; }
        public string ULastlogintime { get; set; }
        public string UEmail { get; set; }
        public string UTel { get; set; }
        public string UIdcard { get; set; }
        public string UBrithplace { get; set; }
        public string UZip { get; set; }
        public string UAddress { get; set; }
        public string UQuestion { get; set; }
        public string UAnswer { get; set; }
        public string DId { get; set; }
        public int UState { get; set; }
        public int UType { get; set; }
        public int Flag { get; set; }
        public string Remark { get; set; }
    }
}
