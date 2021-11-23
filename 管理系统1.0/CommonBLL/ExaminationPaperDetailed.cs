using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBLL
{
    /// <summary>
    /// 试题
    /// </summary>
    public class ExaminationPagerDetailed
    {
        public ObservableCollection<ExaminationOptionDetailed> ExaminationOptions { get; set; } =
            new ObservableCollection<ExaminationOptionDetailed>();

        /// <summary>
        /// 试卷详细编号
        /// </summary>
        public String ExaminationPaperDetailedID { get; set; }

        /// <summary>
        /// 试卷编号
        /// </summary>
        public String ExaminationPaperID { get; set; }

        /// <summary>
        /// 题目的原始序号
        /// </summary>
        public int ExaminationItemIndex { get; set; }

        private double score = 0;
        /// <summary>
        /// 分数
        /// </summary>
        public double Score
        {
            get { return score; }
            set
            {
                score = value;
                //RaisePropertyChanged(() => Score);
            }
        }

        /// <summary>
        /// 题干
        /// </summary>
        public String ExaminationContent { get; set; } = "";

        /// <summary>
        /// 解析
        /// </summary>
        public String Analysis { get; set; } = "";

        /// <summary>
        /// 难度
        /// </summary>
        public int Difficulty { get; set; } = 1;

        /// <summary>
        /// 相关知识点
        /// </summary>
        public String ExaminationPoint { get; set; } = "";

        /// <summary>
        /// 题目类型枚举：1:单选题 2:多选题 3:判断题
        /// </summary>
        public ExaminationTypeEnum ExaminationType { get; set; }
            = ExaminationTypeEnum.单选题;

        public event EventHandler ExaminationStateChanged;
        private ExaminationStateTypeEnum examinationState = ExaminationStateTypeEnum.未知;
        public ExaminationStateTypeEnum ExaminationState
        {
            get
            {
                return examinationState;
            }
            set
            {
                ExaminationStateTypeEnum oldExaminationState = examinationState;
                examinationState = value;
                if (oldExaminationState != examinationState
                    && ExaminationStateChanged != null)
                    ExaminationStateChanged(this, null);
            }
        }



        /// <summary>
        /// 本题的回答
        /// </summary>
        public Answer ExaminationAnswer { get; set; } = new Answer();

        /// <summary>
        /// 本题得分
        /// </summary>
        /// <returns>本题得分</returns>
        public double GetScore()
        {
            bool isCorrect = true;
            foreach (ExaminationOptionDetailed eod in ExaminationOptions)
            {
                isCorrect = isCorrect &&
                    eod.IsCkecked == eod.Correct;
            }

            if (isCorrect)
                return Score;
            else
                return 0;
        }

        /// <summary>
        /// 获得正确答案
        /// </summary>
        /// <returns></returns>
        public string GetRightAnswer()
        {
            string an = "";

            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            foreach (ExaminationOptionDetailed v in ExaminationOptions)
            {
                if (v.Correct)
                {
                    byte[] btNumber = new byte[] { (byte)(ExaminationOptions.IndexOf(v) + 65) };
                    an += asciiEncoding.GetString(btNumber);
                }
            }

            return an;
        }

        public ExaminationPagerDetailed()
        {
            ExaminationAnswer.AnswerChanged += ExaminationAnswer_AnswerChanged;
        }

        private void ExaminationAnswer_AnswerChanged(object sender, EventArgs e)
        {
            foreach (ExaminationOptionDetailed eod in ExaminationOptions)
            {
                eod.IsCkecked = ExaminationAnswer.OptionList.LastIndexOf(eod.ExaminationOptionDetailedID) > 0;
            }
        }

        public void Reply()
        {
            ExaminationState = ExaminationStateTypeEnum.自上次保存后有更改;

            Answer answer = new Answer();

            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            foreach (var v in ExaminationOptions)
            {
                if (!v.IsCkecked) continue;

                byte[] btNumber = new byte[] { (byte)(ExaminationOptions.IndexOf(v) + 65) };
                answer.OptionList += asciiEncoding.GetString(btNumber) + "|";
            }
            answer.OptionList = answer.OptionList.Trim(new char[] { '|' });

            ExaminationAnswer = answer;

        }
    }

    /// <summary>
    /// 试题选项
    /// </summary>
    public class ExaminationOptionDetailed
    {
        /// <summary>
        /// 试卷选项详细编号
        /// </summary>
        public string ExaminationOptionDetailedID { get; set; }

        /// <summary>
        /// 试卷详细编号
        /// </summary>
        public string ExaminationPaperDetailedID { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string OptionContent { get; set; }

        /// <summary>
        /// 是否正确
        /// </summary>
        public bool Correct { get; set; } = false;

        /// <summary>
        /// 考生是否选择该项
        /// </summary>
        public bool IsCkecked
        {
            get;
            set;
        } = false;
    }

    /// <summary>
    /// 试题的考生答题
    /// </summary>
    public class Answer
    {
        public event EventHandler AnswerChanged;

        public string optionList = "";
        /// <summary>
        /// 答卷试题中的答案，多项之间用|分隔
        /// </summary>
        public string OptionList
        {
            get
            {
                return optionList;
            }
            set
            {
                optionList = value;
                if (AnswerChanged != null)
                    AnswerChanged(this, null);
            }
        }

        /// <summary>
        /// 考生答题时间
        /// </summary>
        public DateTime AnswerDateTime { get; set; } = DateTime.MinValue;

        /// <summary>
        /// 答题时的IP
        /// </summary>
        public String ClientIP { get; set; } = "";

        /// <summary>
        /// 考生编号
        /// </summary>
        public String ExamineeID { get; set; } = "";

        /// <summary>
        /// 试卷详细编号
        /// </summary>
        public String ExaminationPaperDetailedID { get; set; } = "";

    }

}
