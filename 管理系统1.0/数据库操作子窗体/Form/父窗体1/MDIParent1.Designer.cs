namespace 数据库操作子窗体
{
    partial class MDIParent1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIParent1));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据维护ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.考点信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.考生信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.管理ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.考试场次ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.管理ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.生成秩序表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.考试信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.管理ToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.试卷出题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.随机出题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.人工出题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.考生数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.离线生成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据收集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.成绩查询打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("menuStrip.BackgroundImage")));
            this.menuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统ToolStripMenuItem,
            this.数据维护ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1584, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.系统ToolStripMenuItem.Text = "系统";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(135, 26);
            this.设置ToolStripMenuItem.Text = "设置IP";
            this.设置ToolStripMenuItem.Click += new System.EventHandler(this.设置ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(135, 26);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 数据维护ToolStripMenuItem
            // 
            this.数据维护ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.考点信息ToolStripMenuItem,
            this.考生信息ToolStripMenuItem,
            this.考试场次ToolStripMenuItem,
            this.考试信息ToolStripMenuItem,
            this.试卷出题ToolStripMenuItem,
            this.考生数据ToolStripMenuItem});
            this.数据维护ToolStripMenuItem.Name = "数据维护ToolStripMenuItem";
            this.数据维护ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.数据维护ToolStripMenuItem.Text = "数据维护";
            // 
            // 考点信息ToolStripMenuItem
            // 
            this.考点信息ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.管理ToolStripMenuItem});
            this.考点信息ToolStripMenuItem.Name = "考点信息ToolStripMenuItem";
            this.考点信息ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.考点信息ToolStripMenuItem.Text = "考点信息";
            // 
            // 管理ToolStripMenuItem
            // 
            this.管理ToolStripMenuItem.Name = "管理ToolStripMenuItem";
            this.管理ToolStripMenuItem.Size = new System.Drawing.Size(122, 26);
            this.管理ToolStripMenuItem.Text = "管理";
            this.管理ToolStripMenuItem.Click += new System.EventHandler(this.管理ToolStripMenuItem_Click);
            // 
            // 考生信息ToolStripMenuItem
            // 
            this.考生信息ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.管理ToolStripMenuItem1});
            this.考生信息ToolStripMenuItem.Name = "考生信息ToolStripMenuItem";
            this.考生信息ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.考生信息ToolStripMenuItem.Text = "考生信息";
            // 
            // 管理ToolStripMenuItem1
            // 
            this.管理ToolStripMenuItem1.Name = "管理ToolStripMenuItem1";
            this.管理ToolStripMenuItem1.Size = new System.Drawing.Size(122, 26);
            this.管理ToolStripMenuItem1.Text = "管理";
            this.管理ToolStripMenuItem1.Click += new System.EventHandler(this.管理ToolStripMenuItem1_Click);
            // 
            // 考试场次ToolStripMenuItem
            // 
            this.考试场次ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.管理ToolStripMenuItem2,
            this.生成秩序表ToolStripMenuItem});
            this.考试场次ToolStripMenuItem.Name = "考试场次ToolStripMenuItem";
            this.考试场次ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.考试场次ToolStripMenuItem.Text = "考试场次";
            this.考试场次ToolStripMenuItem.Click += new System.EventHandler(this.考试场次ToolStripMenuItem_Click);
            // 
            // 管理ToolStripMenuItem2
            // 
            this.管理ToolStripMenuItem2.Name = "管理ToolStripMenuItem2";
            this.管理ToolStripMenuItem2.Size = new System.Drawing.Size(167, 26);
            this.管理ToolStripMenuItem2.Text = "管理";
            this.管理ToolStripMenuItem2.Click += new System.EventHandler(this.管理ToolStripMenuItem2_Click_1);
            // 
            // 生成秩序表ToolStripMenuItem
            // 
            this.生成秩序表ToolStripMenuItem.Name = "生成秩序表ToolStripMenuItem";
            this.生成秩序表ToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.生成秩序表ToolStripMenuItem.Text = "生成秩序表";
            this.生成秩序表ToolStripMenuItem.Click += new System.EventHandler(this.生成秩序表ToolStripMenuItem_Click);
            // 
            // 考试信息ToolStripMenuItem
            // 
            this.考试信息ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.管理ToolStripMenuItem3});
            this.考试信息ToolStripMenuItem.Name = "考试信息ToolStripMenuItem";
            this.考试信息ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.考试信息ToolStripMenuItem.Text = "题库管理";
            this.考试信息ToolStripMenuItem.Click += new System.EventHandler(this.考试信息ToolStripMenuItem_Click);
            // 
            // 管理ToolStripMenuItem3
            // 
            this.管理ToolStripMenuItem3.Name = "管理ToolStripMenuItem3";
            this.管理ToolStripMenuItem3.Size = new System.Drawing.Size(122, 26);
            this.管理ToolStripMenuItem3.Text = "管理";
            this.管理ToolStripMenuItem3.Click += new System.EventHandler(this.管理ToolStripMenuItem3_Click);
            // 
            // 试卷出题ToolStripMenuItem
            // 
            this.试卷出题ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.随机出题ToolStripMenuItem,
            this.人工出题ToolStripMenuItem});
            this.试卷出题ToolStripMenuItem.Name = "试卷出题ToolStripMenuItem";
            this.试卷出题ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.试卷出题ToolStripMenuItem.Text = "组卷";
            // 
            // 随机出题ToolStripMenuItem
            // 
            this.随机出题ToolStripMenuItem.Name = "随机出题ToolStripMenuItem";
            this.随机出题ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.随机出题ToolStripMenuItem.Text = "随机组卷";
            this.随机出题ToolStripMenuItem.Click += new System.EventHandler(this.随机出题ToolStripMenuItem_Click);
            // 
            // 人工出题ToolStripMenuItem
            // 
            this.人工出题ToolStripMenuItem.Name = "人工出题ToolStripMenuItem";
            this.人工出题ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.人工出题ToolStripMenuItem.Text = "人工组卷";
            this.人工出题ToolStripMenuItem.Click += new System.EventHandler(this.人工出题ToolStripMenuItem_Click);
            // 
            // 考生数据ToolStripMenuItem
            // 
            this.考生数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.离线生成ToolStripMenuItem,
            this.数据收集ToolStripMenuItem,
            this.成绩查询打印ToolStripMenuItem});
            this.考生数据ToolStripMenuItem.Name = "考生数据ToolStripMenuItem";
            this.考生数据ToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.考生数据ToolStripMenuItem.Text = "考生数据";
            // 
            // 离线生成ToolStripMenuItem
            // 
            this.离线生成ToolStripMenuItem.Name = "离线生成ToolStripMenuItem";
            this.离线生成ToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.离线生成ToolStripMenuItem.Text = "考场数据导出";
            this.离线生成ToolStripMenuItem.Click += new System.EventHandler(this.离线生成ToolStripMenuItem_Click);
            // 
            // 数据收集ToolStripMenuItem
            // 
            this.数据收集ToolStripMenuItem.Name = "数据收集ToolStripMenuItem";
            this.数据收集ToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.数据收集ToolStripMenuItem.Text = "考生答卷导入";
            this.数据收集ToolStripMenuItem.Click += new System.EventHandler(this.数据收集ToolStripMenuItem_Click);
            // 
            // 成绩查询打印ToolStripMenuItem
            // 
            this.成绩查询打印ToolStripMenuItem.Name = "成绩查询打印ToolStripMenuItem";
            this.成绩查询打印ToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.成绩查询打印ToolStripMenuItem.Text = "成绩查询打印";
            this.成绩查询打印ToolStripMenuItem.Click += new System.EventHandler(this.成绩查询打印ToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 899);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(1584, 26);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(129, 20);
            this.toolStripStatusLabel.Text = "数据库连接状态：";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(33, 20);
            this.toolStripStatusLabel1.Text = "      ";
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // MDIParent1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1584, 925);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MDIParent1";
            this.Text = "数据库操作系统2.0";
            this.Load += new System.EventHandler(this.MDIParent1_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据维护ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 考点信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 考生信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 管理ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 考试场次ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 管理ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 生成秩序表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 考试信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 管理ToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 试卷出题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 随机出题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 人工出题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 考生数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 离线生成ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据收集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 成绩查询打印ToolStripMenuItem;
    }
}



