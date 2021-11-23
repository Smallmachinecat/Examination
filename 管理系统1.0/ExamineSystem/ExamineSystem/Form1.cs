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
using Common;

namespace ExamineSystem
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
            ArrayList kscc = new ArrayList();
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
                    dt.Columns.Add("场次编号", typeof(String));
                    dt.Columns.Add("考场", typeof(String));
                    dt.Columns.Add("座位号", typeof(Int32));
                    dt.Columns.Add("单选题分数", typeof(String));
                    dt.Columns.Add("多选题分数", typeof(String));
                    dt.Columns.Add("判断题分数", typeof(String));
                    dt.Columns.Add("操作题分数", typeof(String));
                    dt.Columns.Add("装配理论题", typeof(String));
                    dt.Columns.Add("装配总分", typeof(String));
                    dt.Columns.Add("钳工总分", typeof(String));
                    dt.Columns.Add("电工总分", typeof(String));
                    dt.Columns.Add("标准分", typeof(String));

                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;
                    dt.Columns[2].ReadOnly = true;
                    dt.Columns[3].ReadOnly = true;
                    dt.Columns[4].ReadOnly = true;
                    dt.Columns[5].ReadOnly = true;
                    dt.Columns[6].ReadOnly = true;
                    dt.Columns[7].ReadOnly = true;
                    dt.Columns[8].ReadOnly = true;
                    dt.Columns[9].ReadOnly = true;
                    dt.Columns[12].ReadOnly = true;


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
                    ArrayList test = new ArrayList();
                    float n1 = 0;
                    float n2 = 0;
                    float n3 = 0;
                    float n4 = 0;
                    float n5 = 0;
                    float count = 0;
                    for (int i = 0; i < Examinee.Count; i++)
                    {
                         n1 = 0;
                         n2 = 0;
                         n3 = 0;
                         n4 = 0;
                         n5 = 0;
                        count = 0;
                        progressBar1.Value += progressBar1.Step;
                        string a = "";
                        string b = "";
                        string c = "";
                        string d = "";
                        string g = "";
                        string h = "";
                        string detail = "";
                        string bnum = "";
                        string eenum = "";

                        string[] choice = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
                        ArrayList option = new ArrayList();
                        ArrayList optionlist = new ArrayList();
                        ArrayList correctlist = new ArrayList();

                        sql = "select BenchworkScore from Examinee where ExamineeID='" + Examinee[i] + "'";
                        cmd.CommandText = sql;
                        read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            bnum= read["BenchworkScore"].ToString();
                        }
                        read.Close();

                        sql = "select ElectricianScore from Examinee where ExamineeID='" + Examinee[i] + "'";
                        cmd.CommandText = sql;
                        read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            eenum = read["ElectricianScore"].ToString();
                        }
                        read.Close();



                        sql = "select ExaminationVenueID from ExaminationVenueOrder where ExamineeID='" + Examinee[i] + "'";
                        cmd.CommandText = sql;
                        read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            a = read["ExaminationVenueID"].ToString();
                        }
                        read.Close();


                        sql = "select ExaminationRoomID from ExaminationVenueOrder where ExamineeID='" + Examinee[i] + "'";
                        cmd.CommandText = sql;
                        read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            b = read["ExaminationRoomID"].ToString();
                        }
                        read.Close();

                        sql = "select SeatNo from ExaminationVenueOrder where ExamineeID='" + Examinee[i] + "'";
                        cmd.CommandText = sql;
                        read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            c = read["SeatNo"].ToString();
                        }
                        read.Close();


                        //分辨考生答案


                        


                        ArrayList ppid = new ArrayList();
                        ArrayList opttt = new ArrayList();

                        //寻找对应答案


                        sql = "select ExaminationPaperDetailedID from AnswerSheetDetailed where ExamineeID='" + Examinee[i] + "'";
                        cmd.CommandText = sql;
                        read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            ppid.Add(read["ExaminationPaperDetailedID"].ToString());
                        }
                        read.Close();
                        if (ppid.Count != 0)
                        {

                            count = 0;
                            n1 = 0;
                            n2 = 0;
                            n3 = 0;
                            n4 = 0;
                            n5 = 0;
                            for (int ppidd = 0; ppidd < ppid.Count; ppidd++)
                            {
                                //题目题干配对选项
                                d = "";
                                sql = "select OptionList from AnswerSheetDetailed where ExaminationPaperDetailedID='" + ppid[ppidd] + "' and ExamineeID='"+Examinee[i]+"'";
                                cmd.CommandText = sql;
                                read = cmd.ExecuteReader();
                                while (read.Read())
                                {
                                    d = EncryptCode.DecryptStr(read["OptionList"].ToString());
                                }
                                read.Close();
                                option = new ArrayList();
                                correctlist = new ArrayList();



                                string score = "";
                                sql = "select Score from ExaminationPaperDetailed where ExaminationPaperDetailedID='" + ppid[ppidd] + "'";
                                cmd.CommandText = sql;
                                read = cmd.ExecuteReader();
                                while (read.Read())
                                {
                                    score = read["Score"].ToString();
                                }
                                read.Close();


                                string type = "";
                                sql = "select ExaminationType from ExaminationPaperDetailed where ExaminationPaperDetailedID='" + ppid[ppidd] + "'";
                                cmd.CommandText = sql;
                                read = cmd.ExecuteReader();
                                while (read.Read())
                                {
                                    type = read["ExaminationType"].ToString();
                                }
                                read.Close();








                                sql = "select Correct from ExaminationOptionDetailed where ExaminationPaperDetailedID='" + ppid[ppidd] + "'";
                                cmd.CommandText = sql;
                                read = cmd.ExecuteReader();
                                while (read.Read())
                                {
                                    option.Add(read["Correct"].ToString());
                                }
                                read.Close();
                                for (int z = 0; z < option.Count; z++)
                                {
                                    if (option[z].ToString() == "True")
                                    {
                                        correctlist.Add(choice[z]);
                                    }
                                }

                                if (type == "1")
                                {
                                    if (d.Length == correctlist.Count)
                                    {
                                        //MessageBox.Show(d + "   1   " + correctlist[0].ToString());
                                        if (d == correctlist[0].ToString())
                                        {
                                            n1 += float.Parse(score);
                                        }
                                    }
                                }




                                bool jiafen = true;
                                ArrayList panduan = new ArrayList();
                                if (type == "2")
                                {
                                    string[] sArray = d.Split('|');
                                    
                                    if (sArray.Length == correctlist.Count)
                                    {
                                        
                                        for (int aix = 0; aix < correctlist.Count; aix++)
                                        {
                                            bool qnmd = false;
                                            for (int aiz = 0; aiz < sArray.Length; aiz++)
                                            {
                                                //MessageBox.Show(ppid[ppidd] + "    " + correctlist[aix].ToString() + "   " + sArray[aiz].ToString());
                                                if (correctlist[aix].ToString() == sArray[aiz].ToString())
                                                {
                                                    qnmd = true;
                                                }
                                            }
                                            panduan.Add(qnmd);
                                            //else
                                            //{
                                            //    //MessageBox.Show(ppid[ppidd] + "   " + sArray[aix].ToString() + "   " + d + "   " + type + "   " + correctlist[aix]);
                                            //}
                                        }
                                        for(int qqq = 0; qqq < panduan.Count; qqq++)
                                        {
                                            if (panduan[qqq].ToString() == "False")
                                            {
                                                jiafen = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        jiafen = false;
                                    }

                                    if (jiafen == true)
                                    {
                                        n2 += int.Parse(score);
                                    }
                                }
                                if (type == "3")
                                {
                                    //MessageBox.Show(ppid[ppidd]+"   "+ d + "   3   " + correctlist[0].ToString());
                                    if (d.Length == correctlist.Count)
                                    {
                                        if (d == correctlist[0].ToString())
                                        {
                                            n3 += float.Parse(score);
                                        }
                                    }
                                }
                                if (type == "4")
                                {
                                    sql = "select Score from AnswerSheetDetailed where ExamineeID='" + Examinee[i] + "'"+ "and ExaminationPaperDetailedID='"+ppid[ppidd]+"'";
                                    cmd.CommandText = sql;
                                    read = cmd.ExecuteReader();
                                    float caozuonum=0;
                                    while (read.Read())
                                    {
                                        caozuonum = float.Parse((read["Score"].ToString()));
                                        
                                    }
                                    n4 += caozuonum;
                                    read.Close();
                                }
                                if (type == "5")
                                {
                                    if (d.Length == correctlist.Count)
                                    {
                                        //MessageBox.Show(d + "   1   " + correctlist[0].ToString());
                                        if (d == correctlist[0].ToString())
                                        {
                                            n5 += float.Parse(score);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            n1 = 0;
                            n2 = 0;
                            n3 = 0;
                            n4 = 0;
                            n5 = 0;
                            count = 0;
                        }
                        count = n1 + n2 + n3 + n4+ n5;
                        float nn2 = 0;
                        if (bnum != "")
                        {
                           nn2 = float.Parse(bnum);
                        }
                        float nn3 = 0;
                        if (eenum != "")
                        {
                            nn3 = float.Parse(eenum);
                        }
                        
                        
                        float nnn = (count+nn2+nn3)/2;
                        if (comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex == -1)
                        {
                            
                            dt.Rows.Add(Examinee[i].ToString(), kcccnum[comboBox1.SelectedIndex].ToString(), "", float.Parse(c), n1, n2, n3, n4,n5, count,bnum,eenum,nnn);
                        }
                        if (comboBox1.SelectedIndex == -1 && comboBox2.SelectedIndex != -1)
                        {
                            dt.Rows.Add(Examinee[i].ToString(), "", kcnum[comboBox2.SelectedIndex].ToString(), float.Parse(c), n1, n2, n3, n4, n5, count, bnum, eenum, nnn);
                        }
                        if (comboBox1.SelectedIndex != -1 && comboBox2.SelectedIndex != -1)
                        {
                            dt.Rows.Add(Examinee[i].ToString(), kcccnum[comboBox1.SelectedIndex].ToString(), kcnum[comboBox2.SelectedIndex].ToString(), float.Parse(c), n1, n2, n3, n4, n5, count, bnum, eenum, nnn);
                        }





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
                    if (k == Colnum - 6)
                    {
                        cells[2 + i, k].PutValue(int.Parse(dt.Rows[i][k].ToString())); //添加数据
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
                    button3.Enabled = true;
                    button3.Text = "导出EXCEL";
                    MessageBox.Show("已存在相同文件，请删除！");
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

        private void Button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount != 0)
            {
                
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    if (dataGridView1[10, i].Value.ToString() != "")
                    {
                        sql = "update Examinee set BenchworkScore =" + float.Parse(dataGridView1[10, i].Value.ToString()) + " where ExamineeID = '" + dataGridView1[0, i].Value.ToString() + "'";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }

                    if (dataGridView1[11, i].Value.ToString() != "")
                    {
                        sql = "update Examinee set ElectricianScore=" + float.Parse(dataGridView1[11, i].Value.ToString()) + " where ExamineeID = '" + dataGridView1[0, i].Value.ToString() + "'";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }

                }
                
            }
            MessageBox.Show("保存成功！");
            Button2_Click(null, null);

        }
    }
}
