#region 文件描述
/******************************************************************************
* 创建: Daoting
* 摘要: 
* 日志: 2013-10-07 创建
******************************************************************************/
#endregion

#region 引用命名
using Dt.CalcEngine;
using System;
#endregion

namespace Dt.CalcEngine.Functions
{
    /// <summary>
    /// Adds the cells in a range that meet multiple criteria.
    /// </summary>
    /// <remarks>
    /// The order of arguments is different between SUMIFS and SUMIF. In 
    /// particular, the sum_range argument is the first argument in SUMIFS,
    /// but it is the third argument in SUMIF. If you are copying and editing 
    /// these similar functions, make sure you put the arguments in the correct orde.
    /// </remarks>
    public class CalcSumIfsFunction : CalcBuiltinFunction
    {
        /// <summary>
        /// Determines whether the function accepts array values
        /// for the specified argument.
        /// </summary>
        /// <param name="i">Index of the argument</param>
        /// <returns>
        /// <see langword="true" /> if the function accepts array values
        /// for the specified argument; <see langword="false" /> otherwise.
        /// </returns>
        public override bool AcceptsArray(int i)
        {
            if (i != 0)
            {
                return ((i % 2) == 1);
            }
            return true;
        }

        /// <summary>
        /// Determines whether the function accepts CalcReference values
        /// for the specified argument.
        /// </summary>
        /// <param name="i">Index of the argument</param>
        /// <returns>
        /// <see langword="true" /> if the function accepts CalcReference values
        /// for the specified argument; <see langword="false" /> otherwise.
        /// </returns>
        public override bool AcceptsReference(int i)
        {
            if (i != 0)
            {
                return ((i % 2) == 1);
            }
            return true;
        }

        private static bool AssertParams(object[] args)
        {
            if ((args.Length < 3) || ((args.Length % 2) != 1))
            {
                return false;
            }
            int rowCount = ArrayHelper.GetRowCount(args[0], 0);
            int columnCount = ArrayHelper.GetColumnCount(args[0], 0);
            for (int i = 1; i < args.Length; i += 2)
            {
                if (rowCount != ArrayHelper.GetRowCount(args[i], 0))
                {
                    return false;
                }
                if (columnCount != ArrayHelper.GetColumnCount(args[i], 0))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Adds the cells in a range that meet multiple criteria.
        /// </summary>
        /// <param name="args"><para>
        /// The args contains 3 - 255 items: sum_range, criteria_range1, criteria1,
        /// [criteria_range2], [criteria2], ..
        /// </para>
        /// <para>
        /// Sum_range is one or more cells to sum, including numbers or names,
        /// arrays, or references that contain numbers.
        /// </para>
        /// <para>
        /// Criteria_range1, criteria_range2, ... are 1 to 126 ranges in which
        /// to evaluate the associated criteria.
        /// </para>
        /// <para>
        /// Criteria1, criteria2, ... are 1 to 126 criteria in the form of a
        /// number, expression, cell reference, or text that define which cells
        /// will be added.
        /// </para></param>
        /// <returns>
        /// A <see cref="T:System.Double" /> value that indicates the evaluate result.
        /// </returns>
        public override object Evaluate(object[] args)
        {
            base.CheckArgumentsLength(args);
            if (!AssertParams(args))
            {
                return CalcErrors.Value;
            }
            double num = 0.0;
            object o = args[0];
            int length = ArrayHelper.GetLength(args[0], 0);
            for (int i = 0; i < length; i++)
            {
                bool flag = true;
                for (int j = 1; j < args.Length; j += 2)
                {
                    object obj3 = args[j];
                    object criteria = args[j + 1];
                    Func<object, bool> func = MathHelper.ParseCriteria(criteria);
                    object arg = ArrayHelper.GetValue(obj3, i, 0);
                    flag = (func == null) ? false : func(arg);
                    if (!flag)
                    {
                        break;
                    }
                }
                if (flag)
                {
                    object obj6 = ArrayHelper.GetValue(o, i, 0);
                    if (CalcConvert.IsNumber(obj6))
                    {
                        num += CalcConvert.ToDouble(obj6);
                    }
                    else if (obj6 is CalcError)
                    {
                        return obj6;
                    }
                }
            }
            return CalcConvert.ToResult(num);
        }

        /// <summary>
        /// Gets the maximum number of arguments for the function.
        /// </summary>
        /// <value>
        /// The maximum number of arguments for the function.
        /// </value>
        public override int MaxArgs
        {
            get
            {
                return 0xff;
            }
        }

        /// <summary>
        /// Gets the minimum number of arguments for the function.
        /// </summary>
        /// <value>
        /// The minimum number of arguments for the function.
        /// </value>
        public override int MinArgs
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// Gets The name of the function.
        /// </summary>
        /// <value>
        /// The name of the function.
        /// </value>
        public override string Name
        {
            get
            {
                return "SUMIFS";
            }
        }
    }
}

