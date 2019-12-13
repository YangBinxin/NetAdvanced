namespace JobWorkForm
{
    partial class AddJobForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.JobName = new System.Windows.Forms.TextBox();
            this.TrigName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CronTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.ClsName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.GroupName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "作业名称：";
            // 
            // JobName
            // 
            this.JobName.Location = new System.Drawing.Point(138, 25);
            this.JobName.Name = "JobName";
            this.JobName.Size = new System.Drawing.Size(182, 21);
            this.JobName.TabIndex = 1;
            this.JobName.Text = "job_test1";
            // 
            // TrigName
            // 
            this.TrigName.Location = new System.Drawing.Point(138, 104);
            this.TrigName.Name = "TrigName";
            this.TrigName.Size = new System.Drawing.Size(182, 21);
            this.TrigName.TabIndex = 3;
            this.TrigName.Text = "trigger_test1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "触发器名称：";
            // 
            // CronTxt
            // 
            this.CronTxt.Location = new System.Drawing.Point(138, 142);
            this.CronTxt.Name = "CronTxt";
            this.CronTxt.Size = new System.Drawing.Size(182, 21);
            this.CronTxt.TabIndex = 5;
            this.CronTxt.Text = "0/3 * * * * ?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "触发器表达式：";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(138, 214);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ClsName
            // 
            this.ClsName.Location = new System.Drawing.Point(138, 179);
            this.ClsName.Name = "ClsName";
            this.ClsName.Size = new System.Drawing.Size(182, 21);
            this.ClsName.TabIndex = 8;
            this.ClsName.Text = "Job_Test1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "对应类名称：";
            // 
            // GroupName
            // 
            this.GroupName.Location = new System.Drawing.Point(138, 65);
            this.GroupName.Name = "GroupName";
            this.GroupName.Size = new System.Drawing.Size(182, 21);
            this.GroupName.TabIndex = 10;
            this.GroupName.Text = "group_test1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(79, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "组名称：";
            // 
            // AddJobForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 247);
            this.Controls.Add(this.GroupName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ClsName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.CronTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TrigName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.JobName);
            this.Controls.Add(this.label1);
            this.Name = "AddJobForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddJobForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox JobName;
        private System.Windows.Forms.TextBox TrigName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CronTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox ClsName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox GroupName;
        private System.Windows.Forms.Label label5;
    }
}