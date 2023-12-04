#region 文件描述
/******************************************************************************
* 创建: Daoting
* 摘要: 
* 日志: 2014-07-02 创建
******************************************************************************/
#endregion

#region 引用命名
using System;
#endregion

namespace Dt.Cells.Data
{
    /// <summary>
    /// Specifies the scale type for the value.
    /// </summary>
    public enum IconValueType
    {
        /// <summary>
        /// Indicates whether to return the result of a formula calculation.
        /// </summary>
        Formula = 7,
        /// <summary>
        /// Indicates whether to return a specified number directly.
        /// </summary>
        Number = 1,
        /// <summary>
        /// Indicates whether to return the percentage of a cell value in a specified cell range.
        /// </summary>
        Percent = 4,
        /// <summary>
        /// Indicates whether to return the percentile of a cell value in a specified cell range.
        /// </summary>
        Percentile = 5
    }
}

