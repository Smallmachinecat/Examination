using CommonDAL;
using CommonDAL.CommonDSTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBLL
{
    public class Examinee
    {
        /// <summary>
        /// 考生编号,10 位数字， 0101190001
        /// </summary>
        public string ExamineeID { get; set; } = "";

        /// <summary>
        /// 考生姓名
        /// </summary>
        public string ExamineeName { get; set; } = "";

        /// <summary>
        /// 身份证号，18位字符
        /// </summary>
        public string CardID { get; set; } = "";

        /// <summary>
        /// 联系方式
        /// </summary>
        public string ContactInformation { get; set; } = "";

        /// <summary>
        /// 报名、考场编号
        /// </summary>
        public string ExaminationRoomID { get; set; } = "";

        public ExaminationRoom TheExaminationRoom { get; set; } = new ExaminationRoom();

        ExamineeTableAdapter adp = null;

        public Examinee()
        {
            adp = new ExamineeTableAdapter(Common.SingleInstance<ConnectConfig>.Instance.DBConnection);
        }

        public bool Login()
        {
            bool isSuccess = false;

            CommonDS.ExamineeDataTable dt = adp.GetDataForLogin(ExamineeID, ExamineeName, CardID, ExaminationRoomID);
            if (dt.Rows.Count == 1)
            {
                TheExaminationRoom = ExaminationRoom.GetExaminationRoom(ExaminationRoomID);


                HeartbeatTableAdapter heartbeatTableAdapter = new HeartbeatTableAdapter(Common.SingleInstance<ConnectConfig>.Instance.DBConnection);
                CommonDS.HeartbeatDataTable heartbeatDT = new CommonDS.HeartbeatDataTable();
                heartbeatTableAdapter.FillByExamineeID(heartbeatDT, ExamineeID);

                if (heartbeatDT.Rows.Count < 1) return true;

                if (((ClientStateType)heartbeatDT[0].ClientState & ClientStateType.可以再次登录) != ClientStateType.可以再次登录
                   && (((ClientStateType)heartbeatDT[0].ClientState & ClientStateType.已交卷) == ClientStateType.已交卷
                   || ((ClientStateType)heartbeatDT[0].ClientState & ClientStateType.强制退出) == ClientStateType.强制退出))
                {
                    System.Windows.MessageBox.Show("你已交卷或被强制退出，即将退出程序！" + Environment.NewLine
                        + "或者联系老师，允许你再次登录。");
                    Tools4WPF.Tools.WPFCommonTools.DoEvents();
                    Environment.Exit(0);
                }

                isSuccess = true;
            }

            return isSuccess;
        }
    }
}
