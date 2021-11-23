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
    /// <summary>
    /// 考生试卷，每个考生试卷的内容相同，但试题顺序不相同
    /// </summary>
    public class ExaminationPaper
    {
        /// <summary>
        /// 试卷编号
        /// </summary>
        public string ExaminationPaperID { get; set; } = "";

        /// <summary>
        /// 试卷名称
        /// </summary>
        public string ExaminationPaperName { get; set; } = "";

        /// <summary>
        /// 出題时间
        /// </summary>
        public DateTime ExaminationPaperDateTime { get; set; } = DateTime.MinValue;

        /// <summary>
        /// 试卷说明
        /// </summary>
        public string ExaminationPaperDescription { get; set; } = "";


        /// <summary>
        /// 试卷单选题详细,主要来自 ExaminationPaperDetailed
        /// 已按照考生的特定题目顺序排列
        /// </summary>
        public ObservableCollection<ExaminationPagerDetailed> Examinations =
            new ObservableCollection<ExaminationPagerDetailed>();


        /// <summary>
        /// 生成指定顺序的试卷
        /// </summary>
        /// <param name="CurExaminationPaperID">指定的试卷</param>
        /// <param name="ExaminationItems">指定顺序</param>
        /// <returns></returns>
        public static ExaminationPaper GetCurExaminationPaper(string CurExaminationPaperID
            , string ExaminationItems
            , string ExamineeID)
        {
            ExaminationPaper examinationPaper = new ExaminationPaper();

            //试卷
            ExaminationPaperTableAdapter adp = new ExaminationPaperTableAdapter(Common.SingleInstance<ConnectConfig>.Instance.DBConnection);
            CommonDS.ExaminationPaperDataTable dt = adp.GetDataByExaminationPaperID(CurExaminationPaperID);
            if (dt.Rows.Count < 1)
                throw new Exception("目前没有指定的试卷！");

            examinationPaper.ExaminationPaperName = dt[0].ExaminationPaperName;

            if (dt[0].ExaminationPaperDateTime != null)
                examinationPaper.ExaminationPaperDateTime = dt[0].ExaminationPaperDateTime;
            else
                examinationPaper.ExaminationPaperDateTime = DateTime.MinValue;

            examinationPaper.ExaminationPaperName = dt[0].ExaminationPaperDescription;
            examinationPaper.ExaminationPaperName = dt[0].ExaminationPaperID;

            //试题
            ExaminationPaperDetailedTableAdapter epdAdp = new ExaminationPaperDetailedTableAdapter(Common.SingleInstance<ConnectConfig>.Instance.DBConnection);
            CommonDS.ExaminationPaperDetailedDataTable epdDT = epdAdp.GetDataByExaminationPaperID(CurExaminationPaperID);
            List<ExaminationPagerDetailed> epds = CollectionHelper.CollectionHelper.ConvertTo<ExaminationPagerDetailed>(epdDT).ToList();

            //选项
            ExaminationOptionDetailedTableAdapter eodAdp = new ExaminationOptionDetailedTableAdapter(
               Common.SingleInstance<ConnectConfig>.Instance.DBConnection);
            CommonDS.ExaminationOptionDetailedDataTable eodDT = eodAdp.GetDataByExaminationPaperID(CurExaminationPaperID);
            List<ExaminationOptionDetailed> eods = CollectionHelper.CollectionHelper.ConvertTo<ExaminationOptionDetailed>(eodDT).ToList();

            //答卷
            AnswerSheetDetailedTableAdapter answerSheetDetailedTableAdapter = new AnswerSheetDetailedTableAdapter(
               Common.SingleInstance<ConnectConfig>.Instance.DBConnection);
            CommonDS.AnswerSheetDetailedDataTable answerSheetDetailedDT = new CommonDS.AnswerSheetDetailedDataTable();
            answerSheetDetailedTableAdapter.FillByExamineeID(answerSheetDetailedDT, ExamineeID);

            //试题集
            List<ExaminationPagerDetailed> Newepds = new List<ExaminationPagerDetailed>();
            //生成指定顺序的试题
            string[] itemsIndex = ExaminationItems.Split('|');
            foreach (string s in itemsIndex)
                try
                {
                    ExaminationPagerDetailed e = epds.Find(o => o.ExaminationItemIndex ==
                     CollectionHelper.CollectionHelper.ConvertTo<int>(s));

                    if (e != null && epds.Remove(e))
                        Newepds.Add(e);
                }
                catch { }
            foreach (ExaminationPagerDetailed e in epds)
            {
                //if (epds.Remove(e))
                Newepds.Add(e);
            }

            //生成指定顺序的试卷
            foreach (ExaminationPagerDetailed epd in Newepds)
            {
                string[] answs = new string[0];
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();

                CommonDS.AnswerSheetDetailedRow v = answerSheetDetailedDT.FindByExamineeIDExaminationPaperDetailedID(ExamineeID, epd.ExaminationPaperDetailedID);
                if (v != null)
                {
                    answs = v.OptionList.Split('|');
                }

                //每题的选项
                List<ExaminationOptionDetailed> theeods = eods.FindAll(o => o.ExaminationPaperDetailedID == epd.ExaminationPaperDetailedID);
                foreach (ExaminationOptionDetailed eod in theeods)
                {

                    byte[] btNumber = new byte[] { (byte)(theeods.IndexOf(eod) + 65) };
                    eod.IsCkecked = answs.FirstOrDefault(o => o == asciiEncoding.GetString(btNumber)) != null;

                    epd.ExaminationOptions.Add(eod);
                }
                if (epd.ExaminationOptions.All(o => !o.IsCkecked))
                    epd.ExaminationState = ExaminationStateTypeEnum.无答案;
                else
                    epd.ExaminationState = ExaminationStateTypeEnum.未编辑;

                examinationPaper.Examinations.Add(epd);
            }

            return examinationPaper;
        }


        public double GetAllScore()
        {
            double sc = 0;
            double sum = 0;
            foreach (var v in Examinations)
            {
                sc = v.GetScore();
                sum += sc;
                if (sc == v.Score)
                    v.ExaminationState = ExaminationStateTypeEnum.正确;
                else
                    v.ExaminationState = ExaminationStateTypeEnum.错误;
            }

            return sum;
        }

        /// <summary>
        /// 保存答卷
        /// </summary>
        /// <param name="ExamineeID"></param>
        /// <returns>-1为有错，其他为保存的记录数</returns>
        public int Save(string ExamineeID)
        {
            int y = -1;

            try
            {
                AnswerSheetDetailedTableAdapter answerSheetDetailedTableAdapter
                = new AnswerSheetDetailedTableAdapter(
                     Common.SingleInstance<ConnectConfig>.Instance.DBConnection);

                CommonDS.AnswerSheetDetailedDataTable
                     answerSheetDetailedDT = new CommonDS.AnswerSheetDetailedDataTable();

                answerSheetDetailedTableAdapter.FillByExamineeID(answerSheetDetailedDT, ExamineeID);

                DateTime answerDateTime = DateTime.Now;
                string clientIP = Common.Tools.GetLocalIP();

                foreach (var v in Examinations)
                {
                    if (v.ExaminationState == ExaminationStateTypeEnum.自上次保存后有更改)
                    {
                        CommonDS.AnswerSheetDetailedRow dr = answerSheetDetailedDT.FindByExamineeIDExaminationPaperDetailedID(
                           ExamineeID, v.ExaminationPaperDetailedID);

                        if (dr == null)
                        {
                            dr = answerSheetDetailedDT.NewAnswerSheetDetailedRow();
                            dr.ExamineeID = ExamineeID;
                            dr.ExaminationPaperDetailedID = v.ExaminationPaperDetailedID;
                            answerSheetDetailedDT.AddAnswerSheetDetailedRow(dr);
                        }

                        dr.OptionList = v.ExaminationAnswer.OptionList;
                        dr.AnswerDateTime = answerDateTime;
                        dr.ClientIP = clientIP;

                    }
                }
                y = answerSheetDetailedTableAdapter.Update(answerSheetDetailedDT);

                foreach (var v in Examinations)
                {
                    if (v.ExaminationState == ExaminationStateTypeEnum.自上次保存后有更改)
                        v.ExaminationState = ExaminationStateTypeEnum.已保存;
                }

            }
            catch (Exception ex)
            { throw ex; }

            return y;
        }
    }
}
