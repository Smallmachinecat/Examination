using DistributedPointDAL.DistributedPointDSTableAdapters;
using System.Data.SqlClient;
using static DistributedPointDAL.DistributedPointDS;

namespace DistributedPointDAL
{
    public class ExamineeProcessor
    {
        ExamineeTableAdapter adp = null;

        public ExamineeDataTable ExamineeDT = new ExamineeDataTable();

        public ExamineeProcessor(SqlConnection CurConnection = null)
        {
            //adp.Fill(this.ExamineeDT);
            if (CurConnection != null)
                adp = new ExamineeTableAdapter(CurConnection);
            else
                adp = new ExamineeTableAdapter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ExamineeRoomID">学校编号</param>
        public ExamineeDataTable GetExamineeDataTable(string ExamineeRoomID)
        {
            adp.FillByExaminationRoomID(this.ExamineeDT,ExamineeRoomID);
            return ExamineeDT;
        }

        public int Save()
        {
            return adp.Update(this.ExamineeDT);
        }

        public void Cancel()
        {
            this.ExamineeDT.RejectChanges();
        }

        public ExamineeRow AddNewExamineeRow()
        {
            ExamineeRow dr = this.ExamineeDT.NewExamineeRow();
            this.ExamineeDT.AddExamineeRow(dr);
            return dr;
        }

        public ExamineeRow FindExaminee(string CardID, string ExamineeID
            , string ExamineeName, string ExaminationRoomID)
        {
            ExamineeRow dr = null;
            if (ExamineeDT == null || ExamineeDT.Rows.Count == 0)
            {
                ExamineeDataTable dt = adp.GetDataForLogin(ExamineeID, ExamineeName, CardID, ExaminationRoomID);
                if (dt.Rows.Count == 1)
                    dr = dt[0];
            }
            else
            {
                dr = ExamineeDT.FindByExamineeID(ExamineeID);
            }

            if (dr != null
                && dr.CardID == CardID
                && dr.ExaminationRoomID == ExaminationRoomID
                && dr.ExamineeName == ExamineeName)
                return dr;
            else
                return null;
        }
    }
}

