using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using System.Threading;

namespace JobClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunProgram().GetAwaiter().GetResult();
            QuartzHelper qq = new QuartzHelper();
            Console.WriteLine("============================================启动任务===============================================");
            qq.StartJob();

            Thread.Sleep(5000);
            Console.WriteLine("============================================增加新的任务，job_test1===============================================");
            qq.AddJob();

            Thread.Sleep(5000);
            Console.WriteLine("============================================线程睡眠结束，结束任务================================================");
            qq.StopJob();
            Console.ReadKey();
        }

        private static async Task RunProgram()
        {
            try
            {
                var properties = new NameValueCollection();
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

                ISchedulerFactory factory = new StdSchedulerFactory(properties);
                IScheduler scheduler = await factory.GetScheduler();

                await scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                ITrigger trigger1 = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithCronSchedule("0/3 * * * * ?")
                    .Build();

                // Tell quartz to schedule the job using our trigger
                await scheduler.ScheduleJob(job, trigger1);

                //await Task.Delay(TimeSpan.FromSeconds(10));
                //await scheduler.Shutdown();

            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }
    }
}
