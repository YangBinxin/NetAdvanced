using JobWorkForm.Tools;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JobWorkForm
{
    /// <summary>
    /// 作业调度Quartz.net，winform控制
    ///     添加工作时，请在JobTaskCls中添加对应类，实现IJob接口，生成的dll文件拷贝在项目文件下的debug文件夹下。反射取出dll文件中的类型。
    /// </summary>
    public partial class Form1 : Form
    {
        QuartzHelper qq = new QuartzHelper();
        public Form1()
        {
            InitializeComponent();
            BindData();
        }

        /// <summary>
        /// 绑定列表数据
        /// </summary>
        public void BindData()
        {
            string sql = @" select job.JOB_NAME,job.JOB_GROUP,trig.TRIGGER_NAME,CRON_EXPRESSION from QRTZ_JOB_DETAILS job
  inner join QRTZ_TRIGGERS trig on job.SCHED_NAME = trig.SCHED_NAME and job.JOB_GROUP = trig.JOB_GROUP
  left join QRTZ_CRON_TRIGGERS cron on cron.TRIGGER_NAME = trig.TRIGGER_NAME and cron.TRIGGER_GROUP = trig.TRIGGER_GROUP";
            var dt = DbHelper.GetDateTable(sql);
            dataGridView1.DataSource = dt;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddJob_Click(object sender, EventArgs e)
        {
            AddJobForm jobForm = new AddJobForm();
            if (jobForm.ShowDialog() == DialogResult.OK)
            {
                BindData();
            }
        }

        /// <summary>
        /// 开启任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startJob_Click(object sender, EventArgs e)
        {
            qq.StartJob();
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopJob_Click(object sender, EventArgs e)
        {
            qq.StopJob();
        }

        /// <summary>
        /// 退出关闭前，结束作业任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            qq.StopJob();
        }

        /// <summary>
        /// 删除作业
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delJob_Click(object sender, EventArgs e)
        {
            CallAsync();
        }
        private async void CallAsync()
        {
            string jobName = dataGridView1.CurrentRow.Cells["JOB_NAME"].Value.ToString();
            string groupName = dataGridView1.CurrentRow.Cells["JOB_GROUP"].Value.ToString();

            //异步返回值
            bool result = await qq.DelJob(jobName, groupName);
            if(result)
                BindData();
        }

        /// <summary>
        /// 恢复选中作业
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resumeSelBtn_Click(object sender, EventArgs e)
        {
            string jobName = dataGridView1.CurrentRow.Cells["JOB_NAME"].Value.ToString();
            string groupName = dataGridView1.CurrentRow.Cells["JOB_GROUP"].Value.ToString();
            qq.ResumeSelJob(jobName, groupName);
        }

        /// <summary>
        /// 暂停选中作业
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pauseSelBtn_Click(object sender, EventArgs e)
        {
            string jobName = dataGridView1.CurrentRow.Cells["JOB_NAME"].Value.ToString();
            string groupName = dataGridView1.CurrentRow.Cells["JOB_GROUP"].Value.ToString();
            qq.PauseSelJob(jobName, groupName);
        }
    }
}
