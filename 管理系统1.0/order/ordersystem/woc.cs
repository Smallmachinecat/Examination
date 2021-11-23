using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using System.Threading;
using Common;
using System.Reflection;
using Aspose.Cells;

namespace ordersystem
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        public Form1(SqlConnection conn)
        {
            con = conn;
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
        }
        ArrayList kcnum = new ArrayList();
        ArrayList kcccnum = new ArrayList();
        SqlCommand cmd = new SqlCommand();
        string add = "";
        string ade = "";
        String sql;
        String constr = "";
        
        SqlDataReader read;
        private void Form1_Load(object sender, EventArgs e)
        {
            connection(con);
        }
        public void connection(SqlConnection con)
        {
            try
            {
                //考试场次名称
                con.Open();

                cmd.Connection = con;
                sql = "select ExaminationVenueName from ExaminationVenue";
                cmd.CommandText = sql;

                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    comboBox1.Items.Add(read["ExaminationVenueName"]);
                }
                read.Close();
                //考试场次编号
                sql = "select ExaminationVenueID from ExaminationVenue";
                cmd.CommandText = sql;
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    kcccnum.Add(read["ExaminationVenueID"]);
                }
                read.Close();



                //考场名称
                sql = "select ExaminationRoom from ExaminationRoom";
                cmd.CommandText = sql;
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    comboBox2.Items.Add(read["ExaminationRoom"]);
                }
                read.Close();
                //考场编号
                sql = "select ExaminationRoomID from ExaminationRoom";
                cmd.CommandText = sql;
                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    kcnum.Add(read["ExaminationRoomID"]);
                }
                read.Close();


            }
            catch
            {
                timer1.Start();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"./constr.txt");
            constr = Common.EncryptCode.DecryptStr(sr.ReadToEnd());
            sr.Close();
            con = new SqlConnection(constr);
            try
            {
                con.Open();
                timer1.Stop();
                MessageBox.Show("已连接！软件即将重启！");
                this.Close();
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
            catch
            {

                comboBox1.Items.Clear();
                timer1.Stop();
                MessageBox.Show("无法连接到数据库！");

            }

        }
        System.Data.DataTable dt;
        private void Button2_Click(object sender, EventArgs e)
        {
            Thread mythread = null;
            mythread = new Thread(new ThreadStart(select));
            mythread.Start();
            

        }



        public void select()
        {
            this.BeginInvoke(new System.Action(() =>
            {
                button2.Enabled = false;
            }));


            

            ArrayList Examinee = new ArrayList();
            this.BeginInvoke(new System.Action(() =>
            {
                if (comboBox1.SelectedItem == null && comboBox2.SelectedItem == null)
                {
                    MessageBox.Show("请选择查询条件！");
                    button2.Enabled = true;
                }
                else
                {
                    dt = new System.Data.DataTable("manage");
                    dt.Columns.Add("考生编号", typeof(String));
                    dt.Columns.Add("考生名称", typeof(String));
                    dt.Columns.Add("身份证号", typeof(String));
                    dt.Columns.Add("考试地点", typeof(String));
                    dt.Columns.Add("考场号", typeof(String));
                    dt.Columns.Add("考场名称", typeof(String));
                    dt.Columns.Add("座位号", typeof(Int32));
                    dt.Columns.Add("考试时间", typeof(String));


                    if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
                    {
                        sql = "select ExamineeID from ExaminationVenueOrder where " + add + " and " + ade;
                    }
                    else
                    {
                        MessageBox.Show("请选中两项数据！");
                        button2.Enabled = true;
                        return;
                    }
                    cmd.CommandText = sql;
                    read = cmd.ExecuteReader();

                    while (read.Read())
                    {
                        string adad = read["ExamineeID"].ToString();
                        Examinee.Add(adad);
                    }
                    read.Close();



                    progressBar1.Maximum = Examinee.Count;//设置最大长度值
                    progressBar1.Value = 0;
                    progressBar1.Step = 1;



                    for (int i = 0; i < Examinee.Count; i++)
                    {

                        progressBar1.Value += progressBar1.Step;
                        string a = "";
                        string b = "";
                        string c = "";
                        string d = "";
                        string e = "";
                        string f = "";
                        string g = "";
                        string h = "";
                        string j = "";
                        string k = "";
                        sql = "select ExaminationVenueID from ExaminationVenueOrder where ExamineeID='" + Examinee[i] + "'";
                        cmd.CommandText = sql;
                        read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            a = read["ExaminationVenueID"].ToString();//考场号
                        }
                        read.Close();


                        sql = "select ExaminationRoomID from ExaminationVenueOrder where ExamineeID='" + Examinee[i] + "'";
                        cmd.CommandText = sql;
                        read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            b = read["ExaminationRoomID"].ToString();//考试地点编号
                        }
                        read.Close();



                        sql = "select SeatNo from ExaminationVenueOrder where ExamineeID='" + Examinee[i] + "'";
                        cmd.CommandText = sql;
                        read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            c = read["SeatNo"].ToString();//座位号
                        }
                        read.Close();

                        sql = "select ExamineeName from Examinee where ExamineeID='" + Examinee[i] + "'";
                        cmd.CommandText = sql;
                        read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            d = read["ExamineeName"].ToString();//考生名称
                        }
                        read.Close();

                        sql = "select CardID from Examinee where ExamineeID='" + Examinee[i] + "'";
                        cmd.CommandText = sql;
                        read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            e = read["CardID"].ToString();//考生身份证
                        }
                        read.Close();

                        sql = "select ExaminationRoom from ExaminationRoom where ExaminationRoomID='" + b + "'";
                        cmd.CommandText = sql;
                        read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            f = read["ExaminationRoom"].ToString();//考试地点
                        }
                        read.Close();

                        sql = "select ExaminationVenueName from ExaminationVenue where ExaminationVenueID='" + a + "'";
                        cmd.CommandText = sql;
                        read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            g = read["ExaminationVenueName"].ToString();//考场名称
                        }
                        read.Close();

                        sql = "select ExaminationTime from ExaminationVenue where ExaminationVenueID='" + a + "'";
                        cmd.CommandText = sql;
                        read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            h = read["ExaminationTime"].ToString();//考试时间
                        }
                        read.Close();



                        dt.Rows.Add(Examinee[i].ToString(),d,e,f,a,g,int.Parse(c),h);





                    }
                    dataGridView1.DataSource = dt;
                    button2.Enabled = true;
                    progressBar1.Value = 0;


                }
            }));



        }



        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            add = "ExaminationVenueID='" + kcccnum[comboBox1.SelectedIndex].ToString() + "'";
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ade = "ExaminationRoomID='" + kcnum[comboBox2.SelectedIndex].ToString() + "'";
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("cmd.exe", "/cshutdown -s -t 0");
        }




        private void Button3_Click_1(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button3.Text = "正在导出";
            if (dt != null)
            {
                string file = "";
                System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                dialog.Description = "请选择Excel所在文件夹";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    file = dialog.SelectedPath;
                }
                else
                {
                    button3.Enabled = true;
                    button3.Text = "导出EXCEL";
                }
                if (file != "")
                {
                    Thread mythread = null;
                    string strlnfo = string.Empty;
                    mythread = new Thread(new ParameterizedThreadStart(output));
                    mythread.Start(file);
                }


            }
            else
            {
                MessageBox.Show("无信息可导出！");
                button3.Enabled = true;
                button3.Text = "导出EXCEL";
            }


        }

        public void output(object filename)
        {
            string file = filename.ToString();
            DataTabletoExcel(dt, file);
        }
        public void DataTabletoExcel(System.Data.DataTable tmpDataTable, string strFileName)
        {

            /////先得到datatable的行数
            //int rowNum = tmpDataTable.Rows.Count;
            /////列数
            //int columnNum = tmpDataTable.Columns.Count;
            /////声明一个应用程序类实例
            //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            ////xlApp.DefaultFilePath = "";  ///默认文件路径，将其设置路径后发现没什么变化。导出excel的路径还是在参数strFileName里设置
            ////xlApp.DisplayAlerts = true;
            ////xlApp.SheetsInNewWorkbook = 1;///返回或设置 Microsoft Excel 自动插入到新工作簿中的工作表数目。Long 类型，可读写。设置为2之后没发现什么区别
            ////创建一个新工作簿
            //Microsoft.Office.Interop.Excel.Workbook xlBook = xlApp.Workbooks.Add();
            /////在工作簿中得到sheet。
            //_Worksheet oSheet = (_Worksheet)xlBook.Worksheets[1];
            ////将DataTable中的数据导入Excel中

            //for (int xx = 0; xx < tmpDataTable.Columns.Count; xx++)
            //{
            //    xlApp.Cells[1, xx + 1] = tmpDataTable.Columns[xx].ColumnName;
            //    xlApp.ActiveSheet.Columns[xx + 1].ColumnWidth = 20;
            //}

            //for (int i = 0; i < rowNum; i++)
            //{

            //    for (int j = 0; j < columnNum; j++)
            //    {
            //        ///excel中的列是从1开始的
            //        xlApp.Cells[i + 2, j + 1] = tmpDataTable.Rows[i][j].ToString();
            //    }
            //}
            /////保存,路径一块穿进去。否则回到一个很奇妙的地方，貌似是system32里 temp下....
            //string nname = "";
            //string funame = "";
            //this.BeginInvoke(new System.Action(() =>
            //{
            //    funame = @strFileName + "/" + comboBox1.Items[comboBox1.SelectedIndex].ToString() + " " + comboBox2.Items[comboBox2.SelectedIndex].ToString();
            //    oSheet.SaveAs(funame);

            //    xlApp.Quit();
            //    MessageBox.Show("导出成功！");
            //    button3.Enabled = true;
            //    button3.Text = "导出EXCEL";
            //}));

            string funame = "";
            string title = "";
            this.BeginInvoke(new System.Action(() =>
            {
                title = comboBox1.Items[comboBox1.SelectedIndex].ToString() + " " + comboBox2.Items[comboBox2.SelectedIndex].ToString();
                funame = @strFileName + "/" + comboBox1.Items[comboBox1.SelectedIndex].ToString() + " " + comboBox2.Items[comboBox2.SelectedIndex].ToString() + ".xlsx";

            }));


            Workbook book = new Workbook(); //创建工作簿
            Worksheet sheet = book.Worksheets[0]; //创建工作表
            Cells cells = sheet.Cells; //单元格
                                       //创建样式
            Aspose.Cells.Style style = book.Styles[book.Styles.Add()];
            style.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 左边界线  
            style.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 右边界线  
            style.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 上边界线  
            style.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = Aspose.Cells.CellBorderType.Thin; //应用边界线 下边界线   
            style.HorizontalAlignment = TextAlignmentType.Center; //单元格内容的水平对齐方式文字居中
            style.Font.Name = "宋体"; //字体
                                    //style1.Font.IsBold = true; //设置粗体
            style.Font.Size = 15; //设置字体大小
                                  //style.ForegroundColor = System.Drawing.Color.FromArgb(153, 204, 0); //背景色
                                  //style.Pattern = Aspose.Cells.BackgroundType.Solid;  

            int Colnum = dt.Columns.Count;//表格列数 
            int Rownum = dt.Rows.Count;//表格行数 
                                       //生成行 列名行 

            cells[0, 0].PutValue(title);
            cells[0, 0].SetStyle(style);
            cells.Merge(0, 0, 1, Colnum);




            for (int i = 0; i < Colnum; i++)
            {
                cells[1, i].PutValue(dt.Columns[i].ColumnName); //添加表头
                cells[1, i].SetStyle(style); //添加样式
            }
            //生成数据行 
            for (int i = 0; i < Rownum; i++)
            {
                for (int k = 0; k < Colnum; k++)
                {
                    if (k == Colnum - 2)
                    {
                        cells[2 + i, k].PutValue(int.Parse( dt.Rows[i][k].ToString())); //添加数据
                        cells[2 + i, k].SetStyle(style); //添加样式
                    }
                    else
                    {
                        cells[2 + i, k].PutValue(dt.Rows[i][k].ToString()); //添加数据
                        cells[2 + i, k].SetStyle(style); //添加样式
                    }
                    
                }
            }
            sheet.AutoFitColumns(); //自适应宽

            
            this.BeginInvoke(new System.Action(() =>
            {
                try
                {
                    book.Save(funame);
                    MessageBox.Show("导出成功！");
                    button3.Enabled = true;
                    button3.Text = "导出EXCEL";
                }
                catch
                {
                    MessageBox.Show("已存在相同文件，请删除！");
                    button3.Enabled = true;
                    button3.Text = "导出EXCEL";
                }
                
            }));

            GC.Collect();





        }
        DataGridViewPrint dprint;
        bool aaa=true;
        private void Button4_Click(object sender, EventArgs e)
        {
            aaa = true;
            if (dt != null)
            {
                string[] a = new string[] { "1", "1" };
                dprint = new DataGridViewPrint();
                dprint.Printer(dataGridView1, printDocument1, a);
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    int xxx = 0;
                    while (aaa)
                    {
                        xxx++;

                        
                        DialogResult dr = MessageBox.Show("正在打印第"+xxx+"张", "打印", messButton);
                        if (dr == DialogResult.OK)//如果点击“确定”按钮

                        {
                            printDocument1.Print();

                        }

                        else//如果点击“取消”按钮

                        {
                            aaa = false;

                        }

                    }
                    
                }
            }
            else
            {
                MessageBox.Show("无数据可打印！");
            }


        }


        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            aaa= dprint.Print(e.Graphics, comboBox1.Items[comboBox1.SelectedIndex].ToString(), comboBox2.Items[comboBox2.SelectedIndex].ToString());
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PrintPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
    }
}
