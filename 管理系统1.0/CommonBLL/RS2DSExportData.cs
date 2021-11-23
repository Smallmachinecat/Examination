using CommonDAL;
using CommonDAL.CommonDSTableAdapters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBLL
{
    public class RS2DSDataPacket
    {
        public RS2DSExportData Data = new RS2DSExportData();

        public int Checkcode = 0;

        public int GetCheckcode()
        {
            int checkcode = Data.GetHashCode();
            return checkcode;
        }

    }
    public class RS2DSExportData
    {
        public CommonDS.ExaminationRoomDataTable ExaminationRoomDT
        { get; set; } = new CommonDS.ExaminationRoomDataTable();

        public CommonDS.ExamineeDataTable ExamineeDT
        { get; set; } = new CommonDS.ExamineeDataTable();

        public CommonDS.ExaminationPaperDataTable ExaminationPaperDT
        { get; set; } = new CommonDS.ExaminationPaperDataTable();

        public CommonDS.ExaminationPaperDetailedDataTable ExaminationPaperDetailedDT
        { get; set; } = new CommonDS.ExaminationPaperDetailedDataTable();

        public CommonDS.ExaminationOptionDetailedDataTable ExaminationOptionDetailedDT
        { get; set; } = new CommonDS.ExaminationOptionDetailedDataTable();

        public CommonDS.AnswerSheetDetailedDataTable AnswerSheetDetailedDT
        { get; set; } = new CommonDS.AnswerSheetDetailedDataTable();

        public CommonDS.ExaminationVenueDataTable ExaminationVenueDT
        { get; set; } = new CommonDS.ExaminationVenueDataTable();

        public CommonDS.ExaminationVenueOrderDataTable ExaminationVenueOrderDT
        { get; set; } = new CommonDS.ExaminationVenueOrderDataTable();


        public bool SaveDataRS2DS(string RoomID, List<string> Venues, string connectstr)
        {
            SqlConnection sqlConnection = new SqlConnection(connectstr);
            //AnswerSheetDetailedTableAdapter answerSheetDetailedTableAdapter = new AnswerSheetDetailedTableAdapter(sqlConnection);
            ExaminationOptionDetailedTableAdapter examinationOptionDetailedTableAdapter = new ExaminationOptionDetailedTableAdapter(sqlConnection);
            ExaminationPaperDetailedTableAdapter examinationPaperDetailedTableAdapter = new ExaminationPaperDetailedTableAdapter(sqlConnection);
            ExaminationPaperTableAdapter examinationPaperTableAdapter = new ExaminationPaperTableAdapter(sqlConnection);
            ExaminationRoomTableAdapter examinationRoomTableAdapter = new ExaminationRoomTableAdapter(sqlConnection);
            ExaminationVenueOrderTableAdapter examinationVenueOrderTableAdapter = new ExaminationVenueOrderTableAdapter(sqlConnection);
            ExaminationVenueTableAdapter examinationVenueTableAdapter = new ExaminationVenueTableAdapter(sqlConnection);
            ExamineeTableAdapter examineeTableAdapter = new ExamineeTableAdapter(sqlConnection);

            examinationRoomTableAdapter.FillByExaminationRoomID(ExaminationRoomDT, RoomID);
            examineeTableAdapter.FillByExaminationRoomID(ExamineeDT, RoomID);


            foreach (string VenueID in Venues)
            {
                CommonDS.ExaminationVenueDataTable dt
                    = examinationVenueTableAdapter.GetDataByExaminationVenueID(VenueID);
                foreach (CommonDS.ExaminationVenueRow venuedr in dt)
                {
                    ExaminationVenueDT.ImportRow(venuedr);


                    CommonDS.ExaminationVenueOrderDataTable examinationVenueOrders
                        = examinationVenueOrderTableAdapter.GetData4DS(VenueID, RoomID);
                    foreach (CommonDS.ExaminationVenueOrderRow dr in examinationVenueOrders)
                    {
                        ExaminationVenueOrderDT.ImportRow(dr);
                    }

                    CommonDS.ExaminationPaperDataTable examinationPapers
                     = examinationPaperTableAdapter.GetDataByExaminationPaperID(venuedr.ExaminationPaperID);
                    foreach (CommonDS.ExaminationPaperRow dr in examinationPapers)
                    {
                        ExaminationPaperDT.ImportRow(dr);
                    }

                    CommonDS.ExaminationPaperDetailedDataTable examinationPaperDetaileds
                     = examinationPaperDetailedTableAdapter.GetDataByExaminationPaperID(venuedr.ExaminationPaperID);
                    foreach (CommonDS.ExaminationPaperDetailedRow dr in examinationPaperDetaileds)
                    {
                        ExaminationPaperDetailedDT.ImportRow(dr);
                    }

                    CommonDS.ExaminationOptionDetailedDataTable examinationOptionDetailed
                     = examinationOptionDetailedTableAdapter.GetDataByExaminationPaperID(venuedr.ExaminationPaperID);
                    foreach (CommonDS.ExaminationOptionDetailedRow dr in examinationOptionDetailed)
                    {
                        ExaminationOptionDetailedDT.ImportRow(dr);
                    }
                }
            }


            return true;
        }
    }
}
