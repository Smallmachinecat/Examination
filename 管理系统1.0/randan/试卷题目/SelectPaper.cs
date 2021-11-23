using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace randan.修改题目
{
    public partial class SelectPaper : Form
    {
        static public string selectPaper="";
        static SelectPaper fc = null;
        string connstr = "";
        public static SelectPaper GetWindow()
        {  //当子窗体没有开启或者已经释放。就可以创建新窗体
            if (fc == null || fc.IsDisposed)
            {
                fc = new SelectPaper();
            }
            return fc;
        }

        public static string PunchSunLei()
        {
            return "";
        }


        public SelectPaper()
        {
            InitializeComponent();
        }

        private void BindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void SelectPaper_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"./constr.txt");
            connstr = Common.EncryptCode.DecryptStr(sr.ReadToEnd());
            sr.Close();

            try
            {

                this.examinationPaperTableAdapter1.Connection = new SqlConnection(connstr);
                dataGridView1.DataSource = dataSet11.ExaminationPaper;
                this.examinationPaperTableAdapter1.Fill(this.dataSet11.ExaminationPaper);
            }
            catch
            {
                MessageBox.Show("出现错误");
            }

        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label1.Text = "选择的是" + dataGridView1.CurrentRow.Cells[0].Value.ToString();
            selectPaper= dataGridView1.CurrentRow.Cells[0].Value.ToString(); 
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditPaper editPaper = new EditPaper();
            editPaper.Show();
            this.Close();
        }
    }
}
