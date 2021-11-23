using CommonBLL;
using CommonDAL;
using CommonDAL.CommonDSTableAdapters;
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
    public partial class FrmRS2DSExportData : Form
    {
        public FrmRS2DSExportData()
        {
            InitializeComponent();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        string constr = "";
        private void FrmExportData_Shown(object sender, EventArgs e)
        {
            StreamReader sr2 = new StreamReader(@"./constr.txt");
            constr = Common.EncryptCode.DecryptStr(sr2.ReadToEnd());
            sr2.Close();

            CommonDS.ExaminationRoomDataTable examinationRooms = new CommonDS.ExaminationRoomDataTable();
            ExaminationRoomTableAdapter examinationRoomTableAdapter
                = new ExaminationRoomTableAdapter(new System.Data.SqlClient.SqlConnection(constr));

            examinationRoomTableAdapter.Fill(examinationRooms);

            //examinationRoomDataTableBindingSource.DataSource = examinationRooms;
            examinationRoomDataTableBindingSource.DataSource =
                CollectionHelper.CollectionHelper.ConvertTo<ExaminationRoom>(examinationRooms);


            CommonDS.ExaminationVenueDataTable examinationVenues = new CommonDS.ExaminationVenueDataTable();
            ExaminationVenueTableAdapter examinationVenueTableAdapter
                = new ExaminationVenueTableAdapter(new System.Data.SqlClient.SqlConnection(constr));

            examinationVenueTableAdapter.Fill(examinationVenues);

            examinationVenueDataTableBindingSource.DataSource = examinationVenues;
        }

        bool isAll = false;
        private void BtnOK_Click(object sender, EventArgs e)
        {
            isAll = false;
            CopyData2DS();
        }

        private void BtnAllOK_Click(object sender, EventArgs e)
        {
            isAll = true;
            CopyData2DS();
        }

        private void CopyData2DS()
        //20190531
        {
            List<string> Venueids = new List<string>();

            foreach (DataGridViewRow v in dgvVenue.Rows)
            {
                if (v.Cells[1].Value != null && v.Cells[1].Value.ToString().ToUpper() == "True".ToUpper())
                {
                    Venueids.Add(v.Cells[0].Value.ToString());
                }
            }
            if (Venueids.Count < 1)
            {
                MessageBox.Show("请选择场次！");
                return;
            }

            FolderBrowserDialog sfd = new FolderBrowserDialog();
            sfd.ShowNewFolderButton = true;
            if (sfd.ShowDialog() != DialogResult.OK)
                return;


            foreach (DataGridViewRow v in dgvRoom.Rows)
            {
                if (isAll
                    || (v.Cells[1].Value != null && v.Cells[1].Value.ToString().ToUpper() == "True".ToUpper())
                    )
                {
                    ExaminationRoom dr = (ExaminationRoom)v.DataBoundItem;

                    RS2DSExportData rs = new RS2DSExportData();
                    rs.CurRoom = dr;
                    rs.RSExportData2DS(Venueids, constr);

                    string fn = Path.Combine(sfd.SelectedPath, rs.RoomDataFileName);
                    XmlSerializer Xmlserializer = new XmlSerializer(typeof(RS2DSExportData));
                    using (FileStream fs = File.Create(fn))
                    {
                        Xmlserializer.Serialize(fs, rs);
                    }


                    string cc = Common.Tools.GetFileMD5(fn);
                    File.WriteAllText(fn + ".MD5", cc);//写入新文件


                    textBox1.Text = dr.ExaminationRoomName + " 已生成完毕！" + Environment.NewLine
                        + textBox1.Text;

                }

            }
        }
    }
}


