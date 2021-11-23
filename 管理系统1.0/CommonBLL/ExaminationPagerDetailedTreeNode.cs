using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBLL
{
    public class ExaminationPagerDetailedTreeNode : Tools4WPF.TreeNode
    {
        public ExaminationPagerDetailed ExaminationPagerDetailed
        { get; set; } = new ExaminationPagerDetailed();

        public ExaminationPagerDetailedTreeNode()
        {
            //ExaminationPagerDetailed.ExaminationStateChanged += TNExaminationPagerDetailed_ExaminationStateChanged;
        }

        public void TNExaminationPagerDetailed_ExaminationStateChanged(object sender, EventArgs e)
        {
            switch (ExaminationPagerDetailed.ExaminationState)
            {
                case ExaminationStateTypeEnum.已保存:
                    this.Icon = "saved.png";
                    break;
                case ExaminationStateTypeEnum.自上次保存后有更改:
                     this.Icon = "edit.png";
                    break;
                case ExaminationStateTypeEnum.未编辑:
                    this.Icon = "NoEdit.png";
                    break;
                case ExaminationStateTypeEnum.正确:
                    this.Icon = "ok.png";
                    break;
                case ExaminationStateTypeEnum.错误:
                    this.Icon = "error.png";
                    break;
                case ExaminationStateTypeEnum.无答案:
                    this.Icon = "NoAnswer.png";
                    break;

            }
        }
    }
}
