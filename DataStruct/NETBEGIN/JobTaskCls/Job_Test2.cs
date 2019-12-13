using JobTaskCls.Tools;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTaskCls
{
    public class Job_Test2 : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string sql = "insert into QRTZ_CONTENT_DETAILS(Guid_Obj,CreateTime,Content,JobName,TriggerName) values(@Guid_Obj,@CreateTime,@Content,@JobName,@TriggerName) ";
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Guid_Obj",SqlDbType.VarChar,36){Value = Guid.NewGuid().ToString("N")},
                    new SqlParameter("@CreateTime",SqlDbType.DateTime){Value = DateTime.Now},
                    new SqlParameter("@Content",SqlDbType.NVarChar,500){Value = "This is Job_Test2"},
                    new SqlParameter("@JobName",SqlDbType.VarChar,50){Value = context.JobDetail.Key.Name},
                    new SqlParameter("@TriggerName",SqlDbType.VarChar,50){Value = context.Trigger.Key.Name}
                };
            DbHelper.ExexuteCommand(sql, parameters);
            await Console.Out.WriteLineAsync("This is Job_Test2");
        }
    }
}
