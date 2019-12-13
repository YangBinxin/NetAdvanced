using JobTaskCls;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JobClient
{
    public class QuartzHelper
    {
        NameValueCollection properties = new NameValueCollection();
        ISchedulerFactory factory = null;
        IScheduler scheduler = null;
        public QuartzHelper()
        {
            properties["quartz.scheduler.instanceName"] = "RemoteServerSchedulerClient";

            // 设置线程池
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "5";
            properties["quartz.threadPool.threadPriority"] = "Normal";

            // 远程输出配置
            properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
            properties["quartz.scheduler.exporter.port"] = "7788";
            properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";
            properties["quartz.scheduler.exporter.channelType"] = "tcp";

            //存储类型
            properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            //表明前缀
            properties["quartz.jobStore.tablePrefix"] = "QRTZ_";
            //驱动类型
            properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz";
            //数据源名称
            properties["quartz.jobStore.dataSource"] = "default";
            properties["quartz.serializer.type"] = "binary";
            //连接字符串
            properties["quartz.dataSource.default.connectionString"] = "Data Source=DESKTOP-98FEVNO;Initial Catalog=Db_new;User ID=sa;pwd=ybx571944791";
            //sqlserver版本
            properties["quartz.dataSource.default.provider"] = "SqlServer";

            factory = new StdSchedulerFactory(properties);

        }

        public async void StartJob()
        {
            scheduler = await factory.GetScheduler();
            await scheduler.Start();
        }

        public async void StopJob()
        {
            scheduler = await factory.GetScheduler();
            await scheduler.Shutdown();
        }

        public async void AddJob()
        {
            scheduler = await factory.GetScheduler();

            Type cls = null;
            Assembly ass = Assembly.LoadFrom("JobTaskCls.dll");
            Type[] mytypes = ass.GetTypes();
            foreach (Type t in mytypes)
            {
                if (t.Name == "Job_Test1")
                {
                    cls = t;
                }
            }

            Console.WriteLine("反射获取类型");
            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create(cls)
                .WithIdentity("job_test1", "group_test1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger_test1", "group_test1")
                .StartNow()
                .WithCronSchedule("0/3 * * * * ?")
                .Build();

            // Tell quartz to schedule the job using our trigger
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
