using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBLL
{
    public enum ExaminationTypeEnum
    {
        单选题 = 1,
        多选题 = 2,
        判断题 = 3,
        装配题 = 4
    }

    /// <summary>
    /// 场次类型枚举
    /// </summary>
    public enum ExaminationVenueTypeEnum
    {
        正式考试 = 0,
        练习 = 1
    }

    /// <summary>
    /// 试题状态
    /// </summary>
    public enum ExaminationStateTypeEnum
    {
        未知 = 0,
        未编辑 = 1,
        自上次保存后有更改 = 2,
        已保存 = 3,
        正确 = 4,
        错误 = 5,
        无答案 = 6
    }
}
