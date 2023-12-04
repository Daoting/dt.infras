#region 文件描述
/******************************************************************************
* 创建: Daoting
* 摘要: 
* 日志: 2014-07-01 创建
******************************************************************************/
#endregion

#region 引用命名
using System;
#endregion

namespace Dt.Xls
{
    /// <summary>
    /// An interface used to represents  built-in name used in Excel
    /// </summary>
    public interface IBuiltInName
    {
        /// <summary>
        /// The built-in name
        /// </summary>
        string Name { get; }
    }
}

