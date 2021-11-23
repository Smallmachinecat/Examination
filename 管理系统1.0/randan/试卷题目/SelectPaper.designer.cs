namespace randan.修改题目
{
    partial class SelectPaper
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet11 = new ddss.DataSet1();
            this.examinationPaperTableAdapter1 = new ddss.DataSet1TableAdapters.ExaminationPaperTableAdapter();
            this.examinationPaperIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.examinationPaperDateTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.examinationPaperNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.examinationPaperDescriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet11)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(547, 521);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.TableLayoutPanel1_Paint);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.examinationPaperIDDataGridViewTextBoxColumn,
            this.examinationPaperDateTimeDataGridViewTextBoxColumn,
            this.examinationPaperNameDataGridViewTextBoxColumn,
            this.examinationPaperDescriptionDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(53, 123);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.tableLayoutPanel1.SetRowSpan(this.dataGridView1, 3);
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(441, 294);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellDoubleClick);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataMember = "ExaminationPaper";
            this.bindingSource1.DataSource = this.dataSet11;
            // 
            // dataSet11
            // 
            this.dataSet11.DataSetName = "DataSet1";
            this.dataSet11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // examinationPaperTableAdapter1
            // 
            this.examinationPaperTableAdapter1.ClearBeforeFill = true;
            // 
            // examinationPaperIDDataGridViewTextBoxColumn
            // 
            this.examinationPaperIDDataGridViewTextBoxColumn.DataPropertyName = "ExaminationPaperID";
            this.examinationPaperIDDataGridViewTextBoxColumn.HeaderText = "试卷ID";
            this.examinationPaperIDDataGridViewTextBoxColumn.Name = "examinationPaperIDDataGridViewTextBoxColumn";
            this.examinationPaperIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // examinationPaperDateTimeDataGridViewTextBoxColumn
            // 
            this.examinationPaperDateTimeDataGridViewTextBoxColumn.DataPropertyName = "ExaminationPaperDateTime";
            this.examinationPaperDateTimeDataGridViewTextBoxColumn.HeaderText = "出卷时间";
            this.examinationPaperDateTimeDataGridViewTextBoxColumn.Name = "examinationPaperDateTimeDataGridViewTextBoxColumn";
            this.examinationPaperDateTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // examinationPaperNameDataGridViewTextBoxColumn
            // 
            this.examinationPaperNameDataGridViewTextBoxColumn.DataPropertyName = "ExaminationPaperName";
            this.examinationPaperNameDataGridViewTextBoxColumn.HeaderText = "试卷名称";
            this.examinationPaperNameDataGridViewTextBoxColumn.Name = "examinationPaperNameDataGridViewTextBoxColumn";
            this.examinationPaperNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // examinationPaperDescriptionDataGridViewTextBoxColumn
            // 
            this.examinationPaperDescriptionDataGridViewTextBoxColumn.DataPropertyName = "ExaminationPaperDescription";
            this.examinationPaperDescriptionDataGridViewTextBoxColumn.HeaderText = "试卷备注";
            this.examinationPaperDescriptionDataGridViewTextBoxColumn.Name = "examinationPaperDescriptionDataGridViewTextBoxColumn";
            this.examinationPaperDescriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(53, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(441, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "点击试卷进入编辑题目";
            // 
            // SelectPaper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 521);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SelectPaper";
            this.Text = "SelectPaper";
            this.Load += new System.EventHandler(this.SelectPaper_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ddss.DataSet1 dataSet11;
        private System.Windows.Forms.DataGridView dataGridView1;
        private ddss.DataSet1TableAdapters.ExaminationPaperTableAdapter examinationPaperTableAdapter1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn examinationPaperIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn examinationPaperDateTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn examinationPaperNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn examinationPaperDescriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label1;
    }
}