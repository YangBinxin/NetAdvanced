using JobWorkForm.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JobWorkForm
{
    public partial class AddJobForm : Form
    {
        public AddJobForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 添加job
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string jobName = JobName.Text;
            string groupNmae = GroupName.Text;
            string trigName = TrigName.Text;
            string cron = CronTxt.Text;
            string clsName = ClsName.Text;

            QuartzHelper qq = new QuartzHelper();
            qq.AddJob(jobName, groupNmae, trigName, cron, clsName);
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
