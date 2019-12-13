using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JobWorkForm.Tools
{
    public class QuartzHelper
    {
        public static NameValueCollection properties = new NameValueCollection();
        public static ISchedulerFactory factory = null;
        public static IScheduler scheduler = null;
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

        /// <summary>
        /// 开始job
        /// </summary>
        public async void StartJob()
        {
            try
            {
                scheduler = await factory.GetScheduler();
                await scheduler.Start();
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
            catch (Exception ex)
            {
                await Console.Error.WriteLineAsync(ex.ToString());
            }
        }

        /// <summary>
        /// 暂停选中job
        /// </summary>
        public async void PauseSelJob(string jobName, string groupName)
        {
            try
            {
                scheduler = await factory.GetScheduler();
                JobKey jk = new JobKey(jobName, groupName);
                await scheduler.PauseJob(jk);
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
            catch (Exception ex)
            {
                await Console.Error.WriteLineAsync(ex.ToString());
            }
        }

        /// <summary>
        /// 恢复选中job
        /// </summary>
        public async void ResumeSelJob(string jobName, string groupName)
        {
            try
            {
                scheduler = await factory.GetScheduler();
                JobKey jk = new JobKey(jobName, groupName);
                await scheduler.ResumeJob(jk);
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
            catch (Exception ex)
            {
                await Console.Error.WriteLineAsync(ex.ToString());
            }
        }

        /// <summary>
        /// 停止job
        /// </summary>
        public async void StopJob()
        {
            try
            {
                scheduler = await factory.GetScheduler();
                await scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
            catch (Exception ex)
            {
                await Console.Error.WriteLineAsync(ex.ToString());
            }
        }

        /// <summary>
        /// 删除 job
        /// </summary>
        public async Task<bool> DelJob(string jobName,string groupName)
        {
            try
            {
                scheduler = await factory.GetScheduler();
                JobKey jk = new JobKey(jobName, groupName);
                bool result = await scheduler.DeleteJob(jk);
                return result;
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
            catch (Exception ex)
            {
                await Console.Error.WriteLineAsync(ex.ToString());
            }
            return false;
        }

        /// <summary>
        /// 添加job
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <param name="triggerName"></param>
        /// <param name="cron"></param>
        /// <param name="clsName"></param>
        public async void AddJob(string jobName, string groupName, string triggerName, string cron, string clsName)
        {
            try
            {
                scheduler = await factory.GetScheduler();

                Type cls = null;
                Assembly ass = Assembly.LoadFrom("JobTaskCls.dll");
                Type[] mytypes = ass.GetTypes();
                foreach (Type t in mytypes)
                {
                    if (t.Name == clsName)
                    {
                        cls = t;
                    }
                }

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create(cls)
                    .WithIdentity(jobName, groupName)
                    .Build();

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(triggerName, groupName)
                    .StartNow()
                    .WithCronSchedule(cron)
                    .Build();

                // Tell quartz to schedule the job using our trigger
                await scheduler.ScheduleJob(job, trigger);
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
            catch (Exception ex)
            {
                await Console.Error.WriteLineAsync(ex.ToString());
            }
        }
    }
}
