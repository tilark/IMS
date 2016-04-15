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
        public decimal GetDepartmentIndicatorValueByCaculate(Guid departmentId, Guid indicatorId, DateTime time)
        {
            decimal value = Decimal.Zero;
            using ( ImsDbContext context = new ImsDbContext())
            {
                //根据inidicatorId从IndicatorAlgorithm中找到ResultOperation
                var resultIndicator = context.IndicatorAlgorithms.Where(i => i.ResultOperationID == indicatorId).FirstOrDefault();
                //如果不存在，返回Decimal.Zero

                if (resultIndicator == null)
                {
                    return value;
                }
                //查找firstOperation，先从DepartmentIndicatorValue中查找，
                var firstIndicator = GetDepartmentIndicatorValue(context, departmentId, resultIndicator.FirstOperationID.Value, time);
            }
            //如果找到，则有值，如果没有，再从IndicatorAlgorithm中找ResultOperation
            //如果未找到，返回
            //如果找到，得到计算值，递归
            //查找SecondOperation,先从DepartmentIndicatorValue中查找
            //如果找到，则有值，如果没有，再从IndicatorAlgorithm中找ResultOperation
            //如果未找到，返回
            //如果找到，得到计算值，递归
            return value;
        }
        public DepartmentIndicatorValue GetDepartmentIndicatorValue( ImsDbContext context, Guid departmentId, Guid indicatorId, DateTime time)
        {
            //先从DepartmentIndicatorValues找项目
            var item = context.DepartmentIndicatorValues.Where(i => i.IndicatorID == indicatorId && i.DepartmentID == departmentId 
                    && i.Time.Year == time.Year && i.Time.Month == time.Month).FirstOrDefault();
            if (item == null)
            {
                //未找到，从IndicatorAlgorithm中找到ResultOperation

                var resultIndicator = context.IndicatorAlgorithms.Where(i => i.ResultOperationID == indicatorId).FirstOrDefault();
                if (resultIndicator != null)
                {
                    //如果能够找到Result的Indicator，继续算值
                    GetDepartmentIndicatorValue(context, departmentId, resultIndicator.ResultOperationID.Value, time);
                }
            }
            return item;
        }
    }
}