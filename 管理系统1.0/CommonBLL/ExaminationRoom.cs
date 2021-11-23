using CommonDAL;
using CommonDAL.CommonDSTableAdapters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBLL
{
    public class ExaminationRoom
    {
        /// <summary>
        /// 报名、考场编号
        /// </summary>
        public string ExaminationRoomID { get; set; } = "";

        /// <summary>
        /// 考场名称
        /// </summary>
        public string ExaminationRoomName { get; set; } = "";

        /// <summary>
        /// 可用机器数量
        /// </summary>
        public int NumberMachines { get; set; } = 0;

        /// <summary>
        /// 备用机器数量
        /// </summary>
        public int NumberSpareMachines { get; set; } = 0;

        /// <summary>
        /// 联系方式
        /// </summary>
        public string ContactInformation { get; set; } = "";

        /// <summary>
        /// 联系人
        /// </summary>
        public string Linkman { get; set; } = "";

        static ExaminationRoomTableAdapter adp = new ExaminationRoomTableAdapter(
                Common.SingleInstance<ConnectConfig>.Instance.DBConnection);// = null;

        public static ExaminationRoom GetExaminationRoom(string ExaminationRoomID)
        {
            adp = new ExaminationRoomTableAdapter(
                Common.SingleInstance<ConnectConfig>.Instance.DBConnection);

            CommonDS.ExaminationRoomDataTable dt = adp.GetDataByExaminationRoomID(ExaminationRoomID);

            if (dt.Rows.Count > 0)
            {
                ExaminationRoom the = new ExaminationRoom();
                the.ExaminationRoomID = dt[0].ExaminationRoomID;
                the.ExaminationRoomName = dt[0].ExaminationRoomName;
                the.Linkman = dt[0].Linkman;
                the.NumberMachines = dt[0].NumberMachines;
                the.NumberSpareMachines = dt[0].NumberSpareMachines;
                the.ContactInformation = dt[0].ContactInformation;

                return the;
            }
            else
                return null;
        }

        public static ObservableCollection<ExaminationRoom> GetAllExaminationRooms()
        {
            CommonDS.ExaminationRoomDataTable dt = adp.GetData();
            ObservableCollection<ExaminationRoom> oc = new ObservableCollection<ExaminationRoom>();

            List<ExaminationRoom> ls = CollectionHelper.CollectionHelper.ConvertTo<ExaminationRoom>(dt).ToList();

            foreach (ExaminationRoom er in ls)
            {
                oc.Add(er);
            }

            return oc;
        }
    }
}
