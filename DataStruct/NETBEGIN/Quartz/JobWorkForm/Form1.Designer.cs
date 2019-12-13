namespace JobWorkForm
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.AddJob = new System.Windows.Forms.Button();
            this.delJob = new System.Windows.Forms.Button();
            this.stopJob = new System.Windows.Forms.Button();
            this.startJob = new System.Windows.Forms.Button();
            this.JOB_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRIGGER_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CRON_EXPRESSION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JOB_GROUP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resumeSelBtn = new System.Windows.Forms.Button();
            this.pauseSelBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JOB_NAME,
            this.TRIGGER_NAME,
            this.CRON_EXPRESSION,
            this.JOB_GROUP});
            this.dataGridView1.Location = new System.Drawing.Point(13, 42);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(561, 277);
            this.dataGridView1.TabIndex = 0;
            // 
            // AddJob
            // 
            this.AddJob.Location = new System.Drawing.Point(13, 13);
            this.AddJob.Name = "AddJob";
            this.AddJob.Size = new System.Drawing.Size(75, 23);
            this.AddJob.TabIndex = 1;
            this.AddJob.Text = "添加作业";
            this.AddJob.UseVisualStyleBackColor = true;
            this.AddJob.Click += new System.EventHandler(this.AddJob_Click);
            // 
            // delJob
            // 
            this.delJob.Location = new System.Drawing.Point(100, 13);
            this.delJob.Name = "delJob";
            this.delJob.Size = new System.Drawing.Size(75, 23);
            this.delJob.TabIndex = 2;
            this.delJob.Text = "删除作业";
            this.delJob.UseVisualStyleBackColor = true;
            this.delJob.Click += new System.EventHandler(this.delJob_Click);
            // 
            // stopJob
            // 
            this.stopJob.Location = new System.Drawing.Point(274, 13);
            this.stopJob.Name = "stopJob";
            this.stopJob.Size = new System.Drawing.Size(90, 23);
            this.stopJob.TabIndex = 3;
            this.stopJob.Text = "停止所有作业";
            this.stopJob.UseVisualStyleBackColor = true;
            this.stopJob.Click += new System.EventHandler(this.stopJob_Click);
            // 
            // startJob
            // 
            this.startJob.Location = new System.Drawing.Point(187, 13);
            this.startJob.Name = "startJob";
            this.startJob.Size = new System.Drawing.Size(75, 23);
            this.startJob.TabIndex = 4;
            this.startJob.Text = "开启作业";
            this.startJob.UseVisualStyleBackColor = true;
            this.startJob.Click += new System.EventHandler(this.startJob_Click);
            // 
            // JOB_NAME
            // 
            this.JOB_NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.JOB_NAME.DataPropertyName = "JOB_NAME";
            this.JOB_NAME.HeaderText = "作业名称";
            this.JOB_NAME.Name = "JOB_NAME";
            this.JOB_NAME.ReadOnly = true;
            // 
            // TRIGGER_NAME
            // 
            this.TRIGGER_NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TRIGGER_NAME.DataPropertyName = "TRIGGER_NAME";
            this.TRIGGER_NAME.HeaderText = "触发器名称";
            this.TRIGGER_NAME.Name = "TRIGGER_NAME";
            this.TRIGGER_NAME.ReadOnly = true;
            // 
            // CRON_EXPRESSION
            // 
            this.CRON_EXPRESSION.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CRON_EXPRESSION.DataPropertyName = "CRON_EXPRESSION";
            this.CRON_EXPRESSION.HeaderText = "触发条件";
            this.CRON_EXPRESSION.Name = "CRON_EXPRESSION";
            this.CRON_EXPRESSION.ReadOnly = true;
            // 
            // JOB_GROUP
            // 
            this.JOB_GROUP.DataPropertyName = "JOB_GROUP";
            this.JOB_GROUP.HeaderText = "组名称";
            this.JOB_GROUP.Name = "JOB_GROUP";
            this.JOB_GROUP.ReadOnly = true;
            // 
            // resumeSelBtn
            // 
            this.resumeSelBtn.Location = new System.Drawing.Point(376, 13);
            this.resumeSelBtn.Name = "resumeSelBtn";
            this.resumeSelBtn.Size = new System.Drawing.Size(90, 23);
            this.resumeSelBtn.TabIndex = 5;
            this.resumeSelBtn.Text = "恢复选中作业";
            this.resumeSelBtn.UseVisualStyleBackColor = true;
            this.resumeSelBtn.Click += new System.EventHandler(this.resumeSelBtn_Click);
            // 
            // pauseSelBtn
            // 
            this.pauseSelBtn.Location = new System.Drawing.Point(478, 13);
            this.pauseSelBtn.Name = "pauseSelBtn";
            this.pauseSelBtn.Size = new System.Drawing.Size(90, 23);
            this.pauseSelBtn.TabIndex = 6;
            this.pauseSelBtn.Text = "暂停选中作业";
            this.pauseSelBtn.UseVisualStyleBackColor = true;
            this.pauseSelBtn.Click += new System.EventHandler(this.pauseSelBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 331);
            this.Controls.Add(this.pauseSelBtn);
            this.Controls.Add(this.resumeSelBtn);
            this.Controls.Add(this.startJob);
            this.Controls.Add(this.stopJob);
            this.Controls.Add(this.delJob);
            this.Controls.Add(this.AddJob);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "定时任务";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button AddJob;
        private System.Windows.Forms.Button delJob;
        private System.Windows.Forms.Button stopJob;
        private System.Windows.Forms.Button startJob;
        private System.Windows.Forms.DataGridViewTextBoxColumn JOB_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRIGGER_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn CRON_EXPRESSION;
        private System.Windows.Forms.DataGridViewTextBoxColumn JOB_GROUP;
        private System.Windows.Forms.Button resumeSelBtn;
        private System.Windows.Forms.Button pauseSelBtn;
    }
}

