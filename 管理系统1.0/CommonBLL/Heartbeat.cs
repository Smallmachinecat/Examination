using CommonDAL;
using CommonDAL.CommonDSTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBLL
{
    public class Heartbeat
    {
        /// <summary>
        /// 
        /// </summary>
        public int HeartbeatID { get; set; } = 0;

        /// <summary>
        /// 客户机IP
        /// </summary>
        public string ClientIP { get; set; } = "";

        /// <summary>
        /// 客户机名称
        /// </summary>
        public string ClientName { get; set; } = "";

        /// <summary>
        /// 考生编号
        /// </summary>
        public string ExamineeID { get; set; } = "";

        /// <summary>
        /// 考生姓名
        /// </summary>
        public string ExamineeName { get; set; } = "";

        /// <summary>
        /// 发送心跳包的时间
        /// </summary>
        public DateTime HeartbeatDateTime { get; set; } = DateTime.MinValue;

        private ClientStateType clientState = ClientStateType.未知;
        /// <summary>
        /// 客户机状态
        /// </summary>
        public ClientStateType ClientState
        {
            get
            {
                return clientState;
            }
            set
            {
                clientState = value;

                this.Send();
            }
        }

        ///// <summary>
        ///// 是否强制客户机退出
        ///// </summary>
        //public bool IsQuit { get; set; } = false;

        /// <summary>
        /// 发送心跳包
        /// </summary>
        /// <returns>成功与否</returns>
        public bool Send()
        {
            HeartbeatTableAdapter heartbeatTableAdapter = new HeartbeatTableAdapter(
              Common.SingleInstance<ConnectConfig>.Instance.DBConnection);
            CommonDS.HeartbeatDataTable heartbeatDT = new CommonDS.HeartbeatDataTable();

            heartbeatTableAdapter.FillByClientIPExamineeID(heartbeatDT, this.ClientIP, this.ExamineeID);

            if (heartbeatDT.Count < 1)
            {
                CommonDS.HeartbeatRow dr = heartbeatDT.NewHeartbeatRow();
                heartbeatDT.AddHeartbeatRow(dr);

                dr.ClientIP = this.ClientIP;
                dr.ClientName = this.ClientName;
                dr.ExamineeID = this.ExamineeID;
                dr.ExamineeName = this.ExamineeName;
            }
            else
            {
                if (((ClientStateType)heartbeatDT[0].ClientState & ClientStateType.可以再次登录) != ClientStateType.可以再次登录
                   && (((ClientStateType)heartbeatDT[0].ClientState & ClientStateType.已交卷) == ClientStateType.已交卷
                   || ((ClientStateType)heartbeatDT[0].ClientState & ClientStateType.强制退出) == ClientStateType.强制退出))
                {
                    System.Windows.MessageBox.Show("你已交卷或被强制退出，即将退出程序！");
                    Tools4WPF.Tools.WPFCommonTools.DoEvents();
                    Environment.Exit(0);
                }
            }

            heartbeatDT[0].ClientState = (int)this.ClientState;
            heartbeatDT[0].HeartbeatDateTime = DateTime.Now;

            try
            {
                return heartbeatTableAdapter.Update(heartbeatDT) > 0;
            }
            catch (Exception ex) { return false; }
        }

    }

    //https://www.cnblogs.com/sunchong/p/4442352.html
    /// <summary>
    /// 客户机状态
    /// </summary>
    [FlagsAttribute]
    public enum ClientStateType
    {
        未知 = 0,
        登录 = 2,
        考试中 = 4,
        已交卷 = 8,
        强制退出 = 16,
        可以再次登录 = 32
    }
}
