using CommonDAL;
using CommonDAL.CommonDSTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBLL
{
    public class ExaminationVenue
    {
        /// <summary>
        /// 考试场次编号
        /// </summary>
        public string ExaminationVenueID { get; set; } = "";

        /// <summary>
        /// 考试场次名称
        /// </summary>
        public string ExaminationVenueName { get; set; } = "";

        /// <summary>
        /// 试卷编号
        /// </summary>
        public string ExaminationPaperID { get; set; } = "";
        /// <summary>
        /// 考试时间
        /// </summary>
        public DateTime ExaminationTime { get; set; } = DateTime.MinValue;

        /// <summary>
        /// 是否当场场次
        /// </summary>
        public bool IsCurExaminationVenue { get; set; } = true;

        /// <summary>
        /// 开场密码
        /// </summary>
        public string Password { get; set; } = "";

        /// <summary>
        /// 是否已考过
        /// </summary>
        public bool IsTested { get; set; } = false;

        /// <summary>
        /// 场次类型
        /// </summary>
        public ExaminationVenueTypeEnum VenueType { get; set; }
            = ExaminationVenueTypeEnum.练习;

        /// <summary>
        /// 座位号
        /// </summary>
        public string SeatNo { get; set; } = "";

        /// <summary>
        /// 考生编号
        /// </summary>
        public string ExamineeID { get; set; } = "";

        /// <summary>
        /// 试题序号集合,每位考生的题目序号是随机的。内容来自于试卷详细表ExaminationPaperDetailed的ExaminationItemIndex
        /// </summary>
        public string ExaminationItems { get; set; } = "";

        /// <summary>
        /// 试题序号集合的校验码
        /// </summary>
        public string CheckCode { get; set; } = "";


        static ExaminationVenueTableAdapter examinationVenueAdp = null;
        static ExaminationVenueOrderTableAdapter examinationVenueOrderAdp = null;
        public static ExaminationVenue GetCurExaminationVenue(string CurExamineeID)
        {
            ExaminationVenue examinationVenue = new ExaminationVenue();

            examinationVenueAdp = new ExaminationVenueTableAdapter(Common.SingleInstance<ConnectConfig>.Instance.DBConnection);
            CommonDS.ExaminationVenueDataTable dt =
                examinationVenueAdp.GetData_GetCurExaminationVenue();

            if (dt.Rows.Count != 1)
                throw new Exception("目前没有指定考试场次或指定了多个考试场次！");

            examinationVenue.ExaminationVenueID = dt[0].ExaminationVenueID;
            examinationVenue.ExaminationVenueName = dt[0].ExaminationVenueName;
            examinationVenue.ExaminationPaperID = dt[0].ExaminationPaperID;
            examinationVenue.ExaminationTime = dt[0].ExaminationTime;
            try { examinationVenue.VenueType = (ExaminationVenueTypeEnum)dt[0].VenueType; }
            catch { };

            examinationVenueOrderAdp = new ExaminationVenueOrderTableAdapter(Common.SingleInstance<ConnectConfig>.Instance.DBConnection);
            CommonDS.ExaminationVenueOrderDataTable dt2 =
                examinationVenueOrderAdp.GetData4Cur(CurExamineeID, examinationVenue.ExaminationVenueID);
            if (dt2.Rows.Count != 1)
                throw new Exception("目前场次没有该考生的考试安排！");

            examinationVenue.ExamineeID = dt2[0].ExamineeID;
            examinationVenue.CheckCode = dt2[0].CheckCode;
            examinationVenue.ExaminationItems = dt2[0].ExaminationItems;
            examinationVenue.SeatNo = dt2[0].SeatNo;


            return examinationVenue;
        }
    }
}
