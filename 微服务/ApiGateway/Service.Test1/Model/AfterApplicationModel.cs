#region 文件描述
/******************************************************************
** Copyright @ 苏州瑞泰信息技术有限公司 All rights reserved. 
** 创建人   :Joseph xing
** 创建时间 :2019-09-01
** 说明     :售后申请Model
******************************************************************/
#endregion
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Service.Test1.Model
{
    public class AfterApplicationModel
    {

    }
    public class one
    {
        /// <summary>
        /// 
        /// </summary>
        public int typeId { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public string type { get; set; }
    }

    public class two
    {
        /// <summary>
        /// 
        /// </summary>
        public int ApprovalMemberType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TeacherLeaveDuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TeacherLeaveDurationType { get; set; }
        /// <summary>
        /// 条件1
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int typeId { get; set; }
    }

    public class three
    {
        /// <summary>
        /// 
        /// </summary>
        public string ApprovalMemberRoleID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ApprovalMemberList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ApprovalMemberType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ApprovalMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IsApprovalMySelf { get; set; }
        /// <summary>
        /// 审批人
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int typeId { get; set; }
    }

    public class four
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> CcMemberList { get; set; }
        /// <summary>
        /// 抄送人
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int typeId { get; set; }
    }

    public class five
    {
        /// <summary>
        /// 
        /// </summary>
        public string ApprovalMemberRoleID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ApprovalMemberList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ApprovalMemberType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ApprovalMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IsApprovalMySelf { get; set; }
        /// <summary>
        /// 审批人
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int typeId { get; set; }
    }

    public class six
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> CcMemberList { get; set; }
        /// <summary>
        /// 抄送人
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int typeId { get; set; }
    }

    public class Root2
    {
        /// <summary>
        /// 
        /// </summary>
        public one _one { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public two _two { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public three _three { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public four _four { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public five _five { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public six _six { get; set; }
    }
}
