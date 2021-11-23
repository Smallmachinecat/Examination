namespace RS2DSSwapData
{
    partial class FrmRS2DSExportData
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
            this.examinationRoomDataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.btnAllOK = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvRoom = new System.Windows.Forms.DataGridView();
            this.examinationRoomIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCK = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.examinationRoomNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contactInformationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linkmanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvVenue = new System.Windows.Forms.DataGridView();
            this.examinationVenueIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCKVenue = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.examinationVenueNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.examinationTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.venueTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.examinationVenueDataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.examinationRoomDataTableBindingSource)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoom)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVenue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.examinationVenueDataTableBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // examinationRoomDataTableBindingSource
            // 
            this.examinationRoomDataTableBindingSource.DataSource = typeof(CommonBLL.ExaminationRoom);
            // 
            // btnExit
            // 
            this.btnExit.AutoSize = true;
            this.btnExit.Location = new System.Drawing.Point(394, 5);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(112, 38);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnAllOK
            // 
            this.btnAllOK.AutoSize = true;
            this.btnAllOK.Location = new System.Drawing.Point(514, 5);
            this.btnAllOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAllOK.Name = "btnAllOK";
            this.btnAllOK.Size = new System.Drawing.Size(236, 38);
            this.btnAllOK.TabIndex = 4;
            this.btnAllOK.Text = "生成所有考场数据";
            this.btnAllOK.UseVisualStyleBackColor = true;
            this.btnAllOK.Click += new System.EventHandler(this.BtnAllOK_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 512);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.btnOK);
            this.flowLayoutPanel1.Controls.Add(this.btnAllOK);
            this.flowLayoutPanel1.Controls.Add(this.btnExit);
            this.flowLayoutPanel1.Controls.Add(this.btnClear);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 259);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(974, 48);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // btnOK
            // 
            this.btnOK.AutoSize = true;
            this.btnOK.Location = new System.Drawing.Point(758, 5);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(212, 38);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "生成指定考场数据";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnClear
            // 
            this.btnClear.AutoSize = true;
            this.btnClear.Location = new System.Drawing.Point(270, 5);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(116, 38);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "清空日志";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // textBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.textBox1, 2);
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(5, 317);
            this.textBox1.Margin = new System.Windows.Forms.Padding(5);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(974, 190);
            this.textBox1.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvRoom);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 241);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "考场:";
            // 
            // dgvRoom
            // 
            this.dgvRoom.AllowUserToAddRows = false;
            this.dgvRoom.AllowUserToDeleteRows = false;
            this.dgvRoom.AutoGenerateColumns = false;
            this.dgvRoom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoom.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.examinationRoomIDDataGridViewTextBoxColumn,
            this.ColCK,
            this.examinationRoomNameDataGridViewTextBoxColumn,
            this.contactInformationDataGridViewTextBoxColumn,
            this.linkmanDataGridViewTextBoxColumn});
            this.dgvRoom.DataSource = this.examinationRoomDataTableBindingSource;
            this.dgvRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRoom.Location = new System.Drawing.Point(3, 30);
            this.dgvRoom.Name = "dgvRoom";
            this.dgvRoom.RowTemplate.Height = 27;
            this.dgvRoom.Size = new System.Drawing.Size(480, 208);
            this.dgvRoom.TabIndex = 0;
            // 
            // examinationRoomIDDataGridViewTextBoxColumn
            // 
            this.examinationRoomIDDataGridViewTextBoxColumn.DataPropertyName = "ExaminationRoomID";
            this.examinationRoomIDDataGridViewTextBoxColumn.HeaderText = "ExaminationRoomID";
            this.examinationRoomIDDataGridViewTextBoxColumn.Name = "examinationRoomIDDataGridViewTextBoxColumn";
            this.examinationRoomIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // ColCK
            // 
            this.ColCK.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ColCK.FalseValue = "False";
            this.ColCK.HeaderText = "选择";
            this.ColCK.Name = "ColCK";
            this.ColCK.TrueValue = "True";
            this.ColCK.Width = 64;
            // 
            // examinationRoomNameDataGridViewTextBoxColumn
            // 
            this.examinationRoomNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.examinationRoomNameDataGridViewTextBoxColumn.DataPropertyName = "ExaminationRoomName";
            this.examinationRoomNameDataGridViewTextBoxColumn.HeaderText = "考场名称";
            this.examinationRoomNameDataGridViewTextBoxColumn.Name = "examinationRoomNameDataGridViewTextBoxColumn";
            this.examinationRoomNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.examinationRoomNameDataGridViewTextBoxColumn.Width = 135;
            // 
            // contactInformationDataGridViewTextBoxColumn
            // 
            this.contactInformationDataGridViewTextBoxColumn.DataPropertyName = "ContactInformation";
            this.contactInformationDataGridViewTextBoxColumn.HeaderText = "说明";
            this.contactInformationDataGridViewTextBoxColumn.Name = "contactInformationDataGridViewTextBoxColumn";
            this.contactInformationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // linkmanDataGridViewTextBoxColumn
            // 
            this.linkmanDataGridViewTextBoxColumn.DataPropertyName = "Linkman";
            this.linkmanDataGridViewTextBoxColumn.HeaderText = "联系人";
            this.linkmanDataGridViewTextBoxColumn.Name = "linkmanDataGridViewTextBoxColumn";
            this.linkmanDataGridViewTextBoxColumn.ReadOnly = true;
            this.linkmanDataGridViewTextBoxColumn.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvVenue);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(495, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(486, 241);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "考试场次:";
            // 
            // dgvVenue
            // 
            this.dgvVenue.AllowUserToAddRows = false;
            this.dgvVenue.AllowUserToDeleteRows = false;
            this.dgvVenue.AutoGenerateColumns = false;
            this.dgvVenue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVenue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.examinationVenueIDDataGridViewTextBoxColumn,
            this.ColCKVenue,
            this.examinationVenueNameDataGridViewTextBoxColumn,
            this.examinationTimeDataGridViewTextBoxColumn,
            this.venueTypeDataGridViewTextBoxColumn});
            this.dgvVenue.DataSource = this.examinationVenueDataTableBindingSource;
            this.dgvVenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVenue.Location = new System.Drawing.Point(3, 30);
            this.dgvVenue.Name = "dgvVenue";
            this.dgvVenue.RowTemplate.Height = 27;
            this.dgvVenue.Size = new System.Drawing.Size(480, 208);
            this.dgvVenue.TabIndex = 0;
            // 
            // examinationVenueIDDataGridViewTextBoxColumn
            // 
            this.examinationVenueIDDataGridViewTextBoxColumn.DataPropertyName = "ExaminationVenueID";
            this.examinationVenueIDDataGridViewTextBoxColumn.HeaderText = "ExaminationVenueID";
            this.examinationVenueIDDataGridViewTextBoxColumn.Name = "examinationVenueIDDataGridViewTextBoxColumn";
            this.examinationVenueIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // ColCKVenue
            // 
            this.ColCKVenue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ColCKVenue.HeaderText = "选择";
            this.ColCKVenue.Name = "ColCKVenue";
            this.ColCKVenue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColCKVenue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColCKVenue.Width = 87;
            // 
            // examinationVenueNameDataGridViewTextBoxColumn
            // 
            this.examinationVenueNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.examinationVenueNameDataGridViewTextBoxColumn.DataPropertyName = "ExaminationVenueName";
            this.examinationVenueNameDataGridViewTextBoxColumn.HeaderText = "场次名称";
            this.examinationVenueNameDataGridViewTextBoxColumn.Name = "examinationVenueNameDataGridViewTextBoxColumn";
            this.examinationVenueNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.examinationVenueNameDataGridViewTextBoxColumn.Width = 135;
            // 
            // examinationTimeDataGridViewTextBoxColumn
            // 
            this.examinationTimeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.examinationTimeDataGridViewTextBoxColumn.DataPropertyName = "ExaminationTime";
            this.examinationTimeDataGridViewTextBoxColumn.HeaderText = "考试时间";
            this.examinationTimeDataGridViewTextBoxColumn.Name = "examinationTimeDataGridViewTextBoxColumn";
            this.examinationTimeDataGridViewTextBoxColumn.ReadOnly = true;
            this.examinationTimeDataGridViewTextBoxColumn.Width = 135;
            // 
            // venueTypeDataGridViewTextBoxColumn
            // 
            this.venueTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.venueTypeDataGridViewTextBoxColumn.DataPropertyName = "VenueType";
            this.venueTypeDataGridViewTextBoxColumn.HeaderText = "类型";
            this.venueTypeDataGridViewTextBoxColumn.Name = "venueTypeDataGridViewTextBoxColumn";
            this.venueTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.venueTypeDataGridViewTextBoxColumn.Width = 87;
            // 
            // examinationVenueDataTableBindingSource
            // 
            this.examinationVenueDataTableBindingSource.DataSource = typeof(CommonDAL.CommonDS.ExaminationVenueDataTable);
            // 
            // FrmRS2DSExportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 512);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmRS2DSExportData";
            this.Text = "考场离线数据生成工具";
            this.Shown += new System.EventHandler(this.FrmExportData_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.examinationRoomDataTableBindingSource)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoom)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVenue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.examinationVenueDataTableBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnAllOK;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.BindingSource examinationRoomDataTableBindingSource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvRoom;
        private System.Windows.Forms.DataGridView dgvVenue;
        private System.Windows.Forms.BindingSource examinationVenueDataTableBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn examinationVenueIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColCKVenue;
        private System.Windows.Forms.DataGridViewTextBoxColumn examinationVenueNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn examinationTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn venueTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn examinationRoomIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColCK;
        private System.Windows.Forms.DataGridViewTextBoxColumn examinationRoomNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contactInformationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn linkmanDataGridViewTextBoxColumn;
    }
}

