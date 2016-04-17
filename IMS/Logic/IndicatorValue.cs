﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using IMS.Models;
using System.Data.Entity.Infrastructure;
using System.Web.ModelBinding;
namespace IMS.Logic
{
    public class IndicatorValue
    {
        public Decimal GetDepartmentIndicatorValueByCalculate(Guid departmentId, Guid indicatorId, DateTime time)
        {
            GetDepartmentIndicatorValueValueByCalculate(departmentId, indicatorId, time, 1);

            #region "原代码"

            //decimal value = Decimal.Zero;
            //using (ImsDbContext context = new ImsDbContext())
            //{
            //    //根据inidicatorId从IndicatorAlgorithm中找到ResultOperation
            //    value = GetDepartmentIndicatorValue(context, departmentId, indicatorId, time);
            //}
            //return value;

            #endregion
        }
        private Decimal GetDepartmentIndicatorValue(ImsDbContext context, Guid departmentId, Guid indicatorId, DateTime time)
        {
            GetDepartmentIndicatorValueValue(context, departmentId, indicatorId, time, 1);

            #region "原代码"

            //    if (indicatorId == null)
            //    {
            //        return Decimal.Zero; ;
            //    }
            //    //先从IndicatorAlgorithm找，看该ID是否为计算结果项，如果是，则继续查找，直到该indicatorId 为基础项
            //    var resultIndicator = context.IndicatorAlgorithms.Where(i => i.ResultOperationID == indicatorId).FirstOrDefault();
            //    if (resultIndicator != null)
            //    {
            //        //如果能够找到Result的Indicator，继续算值
            //        //递归调用计算值，根据Operation计算
            //        if (resultIndicator.Operation == null)
            //        {
            //            return Decimal.Zero;
            //        }
            //        else
            //        {
            //            switch (resultIndicator.Operation)
            //            {
            //                case "addition":
            //                    return GetDepartmentIndicatorValue(context, departmentId, resultIndicator.FirstOperationID.Value, time)
            //                            + GetDepartmentIndicatorValue(context, departmentId, resultIndicator.SecondOperationID.Value, time);
            //                case "subtraction":
            //                    return GetDepartmentIndicatorValue(context, departmentId, resultIndicator.FirstOperationID.Value, time)
            //                     - GetDepartmentIndicatorValue(context, departmentId, resultIndicator.SecondOperationID.Value, time);

            //                case "multiplication":
            //                    return GetDepartmentIndicatorValue(context, departmentId, resultIndicator.FirstOperationID.Value, time)
            //                            * GetDepartmentIndicatorValue(context, departmentId, resultIndicator.SecondOperationID.Value, time);

            //                case "division":
            //                    var secondValue = GetDepartmentIndicatorValue(context, departmentId, resultIndicator.SecondOperationID.Value, time);
            //                    if (secondValue != Decimal.Zero)
            //                    {
            //                        return GetDepartmentIndicatorValue(context, departmentId, resultIndicator.FirstOperationID.Value, time)
            //                                / secondValue;
            //                    }
            //                    else
            //                    {
            //                        return Decimal.Zero;
            //                    }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        //为基础项，从DepartmentIndicatorValues中找值，如果值存在，返回Value，不存在，返回Zero
            //        var item = context.DepartmentIndicatorValues.Where(i => i.IndicatorID == indicatorId && i.DepartmentID == departmentId
            //&& i.Time.Year == time.Year && i.Time.Month == time.Month).FirstOrDefault();
            //        if (item == null)
            //        {
            //            //未找到，返回Zero
            //            return Decimal.Zero;

            //        }
            //        else
            //        {
            //            Decimal parseValue;
            //            if (Decimal.TryParse(item.Value.ToString(), out parseValue))
            //            {
            //                return parseValue;
            //            }
            //            else
            //            {
            //                return Decimal.Zero;
            //            }
            //        }
            //    }
            //    return Decimal.Zero;

            #endregion
        }





        #region"更新的计算指标算法与获取结果"

        /// <summary>
        /// 获取“计算指标”的值。（通过“时段的开始时间”和“时段的结束时间”）。
        /// </summary>
        /// <param name="departmentId">科室ID。</param>
        /// <param name="indicatorId">指标ID。</param>
        /// <param name="start">时段的开始时间。</param>
        /// <param name="end">时段的结束时间。</param>
        /// <returns>指定时段的指定科室的指定指标的值。</returns>
        /// <remarks>时间点不做修正，请在调用的代码中自行修正。</remarks>
        /// <example>
        /// <code>
        /// decimal value = GetDepartmentIndicatorValueValueByCalculate(departmentId, indicatorId, new DateTime(2016, 1, 1), new DateTime(2016, 1, 30));
        /// </code>
        /// </example>
        public decimal GetDepartmentIndicatorValueValueByCalculate(Guid departmentId, Guid indicatorId, DateTime start, DateTime end)
        {
            using (var imsDbContext = new ImsDbContext())
            {
                return GetDepartmentIndicatorValueValue(imsDbContext, departmentId, indicatorId, start, end);
            }
        }

        /// <summary>
        /// 获取“计算指标”的值。（通过“时间点”和“波及的月份时长”）。
        /// </summary>
        /// <param name="departmentId">科室ID。</param>
        /// <param name="indicatorId">指标ID。</param>
        /// <param name="time">时间点。</param>
        /// <param name="duarationMonth">波及的月份时长。</param>
        /// <returns>指定时段的指定科室的指定指标的值。</returns>
        /// <exception cref="ArgumentOutOfRangeException">波及的月份时长必须为正整数。</exception>
        /// <remarks>时间点自动修正为波及时段的起始和终末日期。</remarks>
        /// <example>
        /// <code>
        /// decimal value = GetDepartmentIndicatorValueValueByCalculate(departmentId, indicatorId, new DateTime(2016, 1, 5), 2);
        /// //将获取2016年1月1日至2016年2月29日的数据。
        /// </code>
        /// </example>
        public decimal GetDepartmentIndicatorValueValueByCalculate(Guid departmentId, Guid indicatorId, DateTime time, int duarationMonth)
        {
            if (duarationMonth <= 0)
            {
                throw new ArgumentOutOfRangeException("波及的月份时长必须为正整数。");
            }

            return GetDepartmentIndicatorValueValueByCalculate(departmentId, indicatorId, new DateTime(time.Year, time.Month, 1), new DateTime(time.Year, time.Month, 1).AddMonths(duarationMonth).AddDays(-1));
        }

        /// <summary>
        /// 获取“指标”的值。（通过“时段的开始时间”和“时段的结束时间”）。
        /// </summary>
        /// <param name="imsDbContext">IMS上下文。</param>
        /// <param name="departmentId">科室ID。</param>
        /// <param name="indicatorId">指标ID。</param>
        /// <param name="start">时段的开始时间。</param>
        /// <param name="end">时段的结束时间。</param>
        /// <returns>指定时段的指定科室的指定指标的值。</returns>
        /// <example>
        /// <code>
        /// decimal value = GetDepartmentIndicatorValueValue(new ImsDbContext(), departmentId, indicatorId, new DateTime(2016, 1, 1), new DateTime(2016, 1, 30));
        /// </code>
        /// </example>
        public decimal GetDepartmentIndicatorValueValue(ImsDbContext imsDbContext, Guid departmentId, Guid indicatorId, DateTime start, DateTime end)
        {
            //检测“算法表”中是否有相应的项目

            var IndicatorAlgorithm = imsDbContext.IndicatorAlgorithms.Where(i => i.ResultOperationID == indicatorId).FirstOrDefault();

            if (IndicatorAlgorithm != null)
            {
                //在“算法表”中找到。
                //执行其所代表的计算过程。

                //当算法操作符为空时，返回0。
                if (IndicatorAlgorithm.Operation = null)
                    return decimal.Zero;

                decimal FirstOperand = GetDepartmentIndicatorValue(imsDbContext, departmentId, resultIndicator.FirstOperationID.Value, start, end);
                decimal SecondOperand = GetDepartmentIndicatorValue(imsDbContext, departmentId, resultIndicator.SecondOperationID.Value, start, end);

                switch (IndicatorAlgorithm.Operation)
                {
                    case ("addition"):
                        return FirstOperand + SecondOperand;
                    case ("subtraction"):
                        return FirstOperand - SecondOperand;
                    case ("multiplication"):
                        return FirstOperand * SecondOperand;
                    case ("division"):
                        return (SecondOperand == decimal.Zero) ? decimal.Zero : FirstOperand / SecondOperand;
                }
            }
            else
            {
                //最终尝试在“值表”中找。
                //在“值表”中不应该查找出“计算指标”并进行整合，因为“计算指标”不可以直接整合，而必须由“算法表”中通过获取其计算方法并执行相应计算而得出。逻辑上不会出现在“值表”中查找“计算指标”的可能性，因为在维护“算法表”的时候，已设置所有“计算指标”的“指标ID”，所以当尝试在“算法表”中寻找“计算指标”时，必然命中。

                return imsDbContext.DepartmentIndicatorValues.Where(i => i.IndicatorID == indicatorId && i.DepartmentID == departmentId && i.Time <= end && i.Time >= start).Sum(i => i.Value).Value;
            }
        }

        /// <summary>
        /// 获取“指标”的值。（通过“时间点”和“波及的月份时长”）。
        /// </summary>
        /// <param name="imsDbContext">IMS上下文。</param>
        /// <param name="departmentId">科室ID。</param>
        /// <param name="indicatorId">指标ID。</param>
        /// <param name="time">时间点。</param>
        /// <param name="duarationMonth">波及的月份时长。</param>
        /// <returns>指定时段的指定科室的指定指标的值。当算法操作符为空时，返回0。当除数为0时，直接返回0。</returns>
        /// <exception cref="ArgumentOutOfRangeException">波及的月份时长必须为正整数。</exception>
        /// <remarks>时间点自动修正为波及时段的起始和终末日期。</remarks>
        /// <example>
        /// <code>
        /// decimal value = GetDepartmentIndicatorValueValue(new ImsDbContext(), departmentId, indicatorId, new DateTime(2016, 1, 5), 2);
        /// //将获取2016年1月1日至2016年2月29日的数据。
        /// </code>
        /// </example>
        public decimal GetDepartmentIndicatorValueValue(ImsDbContext imsDbContext, Guid departmentId, Guid indicatorId, DateTime time, int duarationMonth)
        {
            if (duarationMonth <= 0)
            {
                throw new ArgumentOutOfRangeException("波及的月份时长必须为正整数。");
            }

            return GetDepartmentIndicatorValueValue(imsDbContext, departmentId, indicatorId, new DateTime(time.Year, time.Month, 1), new DateTime(time.Year, time.Month, 1).AddMonths(duarationMonth).AddDays(-1));
        }

        #endregion
    }
}