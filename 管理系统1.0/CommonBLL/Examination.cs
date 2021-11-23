using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBLL
{
    /// <summary>
    /// 试题选项
    /// </summary>
    public class ExaminationOption
    {
        /// <summary>
        /// 选项编号
        /// </summary>
        public int OptionListID { get; set; }
        /// <summary>
        /// 所属试题编号
        /// </summary>
        public int ExaminationID { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string OptionContent { get; set; }

        /// <summary>
        /// 是否正确
        /// </summary>
        public bool Correct { get; set; } = false;
    }

    /// <summary>
    /// 试题
    /// </summary>
    public class Examination
    {
        public ObservableCollection<ExaminationOption> ExaminationOptions { get; set; } =
            new ObservableCollection<ExaminationOption>();

        /// <summary>
        /// 分数
        /// </summary>
        public double Score { get; set; } = 0;
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

        /// <summary>
        /// 本题的回答
        /// </summary>
        public Answer ExaminationAnswer { get; set; } = new Answer();

        /// <summary>
        /// 在上次保存后，是否已保存
        /// </summary>
        public bool IsChanged { get; set; } = true;
    }
   
   
}
