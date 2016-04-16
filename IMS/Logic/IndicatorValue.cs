using System;
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
            decimal value = Decimal.Zero;
            using (ImsDbContext context = new ImsDbContext())
            {
                //根据inidicatorId从IndicatorAlgorithm中找到ResultOperation
                value = GetDepartmentIndicatorValue(context, departmentId, indicatorId, time);
            }
            return value;
        }
        private Decimal GetDepartmentIndicatorValue(ImsDbContext context, Guid departmentId, Guid indicatorId, DateTime time)
        {
            if (indicatorId == null)
            {
                return Decimal.Zero; ;
            }
            //先从IndicatorAlgorithm找，看该ID是否为计算结果项，如果是，则继续查找，直到该indicatorId 为基础项
            var resultIndicator = context.IndicatorAlgorithms.Where(i => i.ResultOperationID == indicatorId).FirstOrDefault();
            if (resultIndicator != null)
            {
                //如果能够找到Result的Indicator，继续算值
                //递归调用计算值，根据Operation计算
                if (resultIndicator.Operation == null)
                {
                    return Decimal.Zero;
                }
                else
                {
                    switch (resultIndicator.Operation)
                    {
                        case "addition":
                            return GetDepartmentIndicatorValue(context, departmentId, resultIndicator.FirstOperationID.Value, time)
                                    + GetDepartmentIndicatorValue(context, departmentId, resultIndicator.SecondOperationID.Value, time);
                        case "subtraction":
                            return GetDepartmentIndicatorValue(context, departmentId, resultIndicator.FirstOperationID.Value, time)
                             - GetDepartmentIndicatorValue(context, departmentId, resultIndicator.SecondOperationID.Value, time);

                        case "multiplication":
                            return GetDepartmentIndicatorValue(context, departmentId, resultIndicator.FirstOperationID.Value, time)
                                    * GetDepartmentIndicatorValue(context, departmentId, resultIndicator.SecondOperationID.Value, time);

                        case "division":
                            var secondValue = GetDepartmentIndicatorValue(context, departmentId, resultIndicator.SecondOperationID.Value, time);
                            if (secondValue != Decimal.Zero)
                            {
                                return GetDepartmentIndicatorValue(context, departmentId, resultIndicator.FirstOperationID.Value, time)
                                        / secondValue;
                            }
                            else
                            {
                                return Decimal.Zero;
                            }
                    }
                }
            }
            else
            {
                //为基础项，从DepartmentIndicatorValues中找值，如果值存在，返回Value，不存在，返回Zero
                var item = context.DepartmentIndicatorValues.Where(i => i.IndicatorID == indicatorId && i.DepartmentID == departmentId
        && i.Time.Year == time.Year && i.Time.Month == time.Month).FirstOrDefault();
                if (item == null)
                {
                    //未找到，返回Zero
                    return Decimal.Zero;

                }
                else
                {
                    Decimal parseValue;
                    if (Decimal.TryParse(item.Value.ToString(), out parseValue))
                    {
                        return parseValue;
                    }
                    else
                    {
                        return Decimal.Zero;
                    }
                }
            }
            return Decimal.Zero;
        }
    }
}