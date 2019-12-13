using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCommon
{
    public class Test
    {
        public static string HelloWorld()
        {
            return DbHelper.GetScalar("select U_TRUENAME from TB_USER").ToString();
        }
    }
}
