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
using System.Threading;
using Common;
using System.IO;
using System.Collections;
using System.Threading;
using Aspose.Cells;

namespace 数据库操作子窗体
{
    public partial class Exam_room_manage : Form
    {
      //----------------------------------------------------------------------------
        static Exam_room_manage fc = null; //创建一个静态对象
        public static Exam_room_manage GetWindow()
        {  //当子窗体没有开启或者已经释放。就可以创建新窗体
            if (fc == null || fc.IsDisposed)
            {
                fc = new Exam_room_manage();
            }
            return fc;
        }
        


        public string constr = "";
        //----------------------------------------------------------------------------
        public Exam_room_manage()
        {
            InitializeComponent();
        }

        private void Exam_room_manage_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;
            StreamReader sr1 = new StreamReader(@"./constr.txt");

            constr = Common.EncryptCode.DecryptStr(sr1.ReadToEnd());
            sr1.Close();

            try
            {
                Update();
            }
            catch
            {
                MessageBox.Show("数据库中该表不存在");
            }


        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

            // TODO: 这行代码将数据加载到表“dataSet1.ExaminationRoom”中。您可以根据需要移动或删除它。
            //this.examinationRoomTableAdapter.Fill(dataSet1.ExaminationRoom);
            //SqlDataAdapter da = new SqlDataAdapter();
            //da.Update(dataSet1, "ExaminationRoom");
            Thread mythread = null;
            string strlnfo = string.Empty;
            mythread = new Thread(up);
            mythread.Start();
            // TODO: 这行代码将数据加载到表“dataSet1.ExaminationRoom”中。您可以根据需要移动或删除它。
            //this.examinationRoomTableAdapter.Fill(dataSet1.ExaminationRoom);
            //SqlDataAdapter da = new SqlDataAdapter();
            //da.Update(dataSet1, "ExaminationRoom");

        }
        public void up()
        {
            SqlConnection con = new SqlConnection(constr);
            try
            {

                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    examinationRoomTableAdapter.Connection = new SqlConnection(constr);
                    examinationRoomTableAdapter.Update(dataSet11.ExaminationRoom);
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
        public void Update()
        {
            //SqlConnection con = new SqlConnection("Data Source = " + MDIParent1.ip + "; Initial Catalog = Student; Persist Security Info = True; User ID = sa; Password = Admin123; Connection Timeout = 1");
            ////2.创建数据库连接对象
            ////SqlConnection con = new SqlConnection(con);
            ////DataSet1 dataSet1 = new DataSet1();
            //examinationRoomTableAdapter.Connection = con;
            examinationRoomTableAdapter.Connection = new SqlConnection(constr);
            examinationRoomTableAdapter.Fill(dataSet11.ExaminationRoom);
            dataGridView1.DataSource = dataSet11.ExaminationRoom;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            examinationRoomTableAdapter.Connection = new SqlConnection(constr);
            examinationRoomTableAdapter.FillByLinke1(dataSet11.ExaminationRoom, textBox1.Text);
            dataGridView1.DataSource = dataSet11.ExaminationRoom;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("数据不能为空！");
        }
        
        private void Button5_Click(object sender, EventArgs e)
        {
            string files = "";
            
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "表格文件(*.xlsx)|*.xlsx";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                files = dialog.FileName;
            }

            if (files != "")
            {
                Thread mythread = null;
                string strlnfo = string.Empty;
                mythread = new Thread(new ParameterizedThreadStart(progressvalue));
                mythread.Start(files);
            }



        }
        public void progressvalue(object ob)
        {
            ArrayList kcname = new ArrayList();
            Cells cells = null;
            Workbook workbook = new Workbook(ob.ToString());
            cells = workbook.Worksheets[0].Cells;
            string headline = cells[0, 6].StringValue.Trim();
            if (headline == "报名点名称")
            {
                int a=cells.MaxDataRow;
                for(int i = 1; i < a + 1; i++)
                {
                    if (kcname==null)
                    {
                        kcname.Add(cells[i, 6].StringValue.Trim());
                    }
                    else
                    {
                        bool being = true;
                        for(int x = 0; x < kcname.Count; x++)
                        {
                            if(cells[i, 6].StringValue.Trim() == kcname[x].ToString())
                            {

                                being = false;
                            }
                        }
                        if (being == true)
                        {
                            kcname.Add(cells[i, 6].StringValue.Trim());
                        }
                    }
                }
            }
            StreamReader sr2 = new StreamReader(@"./constr.txt");
            constr = Common.EncryptCode.DecryptStr(sr2.ReadToEnd());
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            int num = 1001;
            for(int y = 0; y < kcname.Count; y++)
            {
                string sql = "insert into ExaminationRoom(ExaminationRoomID,ExaminationRoom,NumberMachines,NumberSpareMachines) values ('"+(num+y).ToString()+"','"+kcname[y].ToString()+"',"+100+ "," + 100 + ")";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("导入成功！");
            Button4_Click(null,null);
            con.Close();


        }
    }
}
