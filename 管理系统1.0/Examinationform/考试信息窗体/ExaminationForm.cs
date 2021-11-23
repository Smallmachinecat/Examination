using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Common;

namespace 数据库操作子窗体
{
    public partial class ExaminationForm : Form
    {
        //----------------------------------------------------------------------------
        static ExaminationForm fc = null; //创建一个静态对象
        public static ExaminationForm GetWindow()
        {  //当子窗体没有开启或者已经释放。就可以创建新窗体
            if (fc == null || fc.IsDisposed)
            {
                fc = new ExaminationForm();
            }
            return fc;
        }
        //----------------------------------------------------------------------------
        public ExaminationForm()
        {
            InitializeComponent();
        }
        public string constr = "";
        private void ExaminationForm_Load(object sender, EventArgs e)
        {
            StreamReader sr2 = new StreamReader(@"./constr.txt");
            constr = Common.EncryptCode.DecryptStr(sr2.ReadToEnd());
            sr2.Close();
            // TODO: 这行代码将数据加载到表“dataSet1.Examination”中。您可以根据需要移动或删除它。
            this.WindowState = FormWindowState.Maximized;
            this.examinationTableAdapter.Connection = new SqlConnection(constr);
            this.examinationTableAdapter.Fill(this.dataSet1.Examination);

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constr);
            try
            {

                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    examinationTableAdapter.Connection = new SqlConnection(constr);
                    examinationTableAdapter.Update(dataSet1.Examination);
                    MessageBox.Show("更新成功");
                }
                else
                {
                    MessageBox.Show("数据库连接失败");
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("更新失败");
            }
            finally
            {
                con.Close();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
