using CommonBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace RS2DSSwapData
{
    public partial class FrmDS2RSImportData : Form
    {
        public FrmDS2RSImportData()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void BtnMove_Click(object sender, EventArgs e)
        {
            for (int i = listBox1.SelectedItems.Count; i > 0; i--)
            {
                listBox1.Items.Remove(listBox1.SelectedItems[i - 1]);
            }
        }

        private void BtnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "答案数据包|*.dat";

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            listBox1.Items.AddRange(openFileDialog.FileNames);
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = listBox1.Items.Count; i > 0; i--)
                {
                    if (importData(listBox1.Items[i].ToString()))
                    {
                        listBox1.Items.Remove(listBox1.Items[i - 1]);

                        textBox1.Text = " 已导入完毕：" + listBox1.Items[i].ToString() + Environment.NewLine
                              + textBox1.Text;
                    }
                    else
                    {
                        textBox1.Text = " 已导入失败：" + listBox1.Items[i].ToString() + Environment.NewLine
                              + textBox1.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool importData(string fn)
        {
            bool isSuccess = false;

            RS2DSExportData rS2DSExportData = null;
            XmlSerializer Xmlserializer = new XmlSerializer(typeof(RS2DSExportData));
            using (FileStream fs = new FileStream(fn, FileMode.Open))
            {
                rS2DSExportData = (RS2DSExportData)Xmlserializer.Deserialize(fs);
            }

            //文件名时间等
            if (!fn.EndsWith(rS2DSExportData.RoomDataFileName))
            {
                MessageBox.Show("文件被破坏！请重新下载导入！"
                    , CommonBLL.ConnectConfig.Caption
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Warning);
                return false;
            }

            //MD5检查
            try
            {
                string filePath = fn + ".MD5";
                if (File.Exists(filePath))
                {
                    string Text = File.ReadAllText(filePath);

                    string dd = Common.Tools.GetFileMD5(fn);

                    isSuccess = dd == Text;
                }
                else
                {
                    MessageBox.Show("MD5文件不存在:" + filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (!isSuccess) return false;

            isSuccess = rS2DSExportData.DSImportDataFromDS();


            return isSuccess;
        }
    }
}
