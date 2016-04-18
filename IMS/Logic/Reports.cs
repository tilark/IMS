using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Logic
{
    /// <summary>
    /// 处理“报表”相关功能。
    /// </summary>
    public static class Reports
    {
        /// <summary>
        /// 获取“通用”“报表”。
        /// </summary>
        /// <param name="imsDbContext">IMS上下文。</param>
        /// <param name="departmentCategoryId">科室类别ID。</param>
        /// <param name="time">时间点。</param>
        /// <param name="duarationMonth">波及的月份时长。</param>
        /// <returns>指定时间段的指定科室的报表。</returns>
        /// <remarks>时间点自动修正为波及时段的起始和终末日期。
        /// 该方法完全信任源数据对“科室”“时段”“指标”的值的记录的唯一性保证。
        /// </remarks>
        public static System.Data.DataTable GetGenericReport(Models.ImsDbContext imsDbContext, Guid departmentCategoryId, DateTime time, int duarationMonth)
        {
            return GetGenericReport(imsDbContext, departmentCategoryId, new DateTime(time.Year, time.Month, 1), new DateTime(time.Year, time.Month, 1).AddMonths(duarationMonth).AddDays(-1));
        }

        /// <summary>
        /// 获取“通用”“报表”。
        /// </summary>
        /// <param name="imsDbContext">IMS上下文。</param>
        /// <param name="departmentCategoryId">科室类别ID。</param>
        /// <param name="start">时段的开始时间。</param>
        /// <param name="end">时段的结束时间。</param>
        /// <returns>指定时间段的指定科室的报表。</returns>
        /// <remarks>时间点不做修正，请在调用的代码中自行修正。
        /// 该方法完全信任源数据对“科室”“时段”“指标”的值的记录的唯一性保证。
        /// </remarks>
        public static System.Data.DataTable GetGenericReport(Models.ImsDbContext imsDbContext, Guid departmentCategoryId, DateTime start, DateTime end)
        {
            System.Data.DataSet dataSet = new System.Data.DataSet();

            //创建视图，整理和存放待透视数据。
            dataSet.Tables.Add(new System.Data.DataTable("ViewValue"));

            //该视图有3列。
            dataSet.Tables["ViewValue"].Columns.Add(new System.Data.DataColumn("Indicator", typeof(Models.Indicator)));
            dataSet.Tables["ViewValue"].Columns.Add(new System.Data.DataColumn("Department", typeof(Models.Department)));
            dataSet.Tables["ViewValue"].Columns.Add(new System.Data.DataColumn("Value", typeof(decimal)));

            //获取实参指定的“科室类别”所涉及的“指标”实例。
            var listIndicator = imsDbContext.Indicators.Where(i => i.DepartmentCategoryIndicatorMaps.Any(j => j.DepartmentCategoryID == departmentCategoryId)).ToList();

            //获取实参指定的“科室类别”所涉及的“科室”实例。
            var listDepartment = imsDbContext.Departments.Where(i => i.DepartmentCategoryID == departmentCategoryId).ToList();

            //将“科室”和“指标”组合，并获取值，填入视图。    
            foreach (var department in listDepartment)
            {
                foreach (var indicator in listIndicator)
                {
                    var row = dataSet.Tables["ViewValue"].NewRow();

                    row["Indicator"] = indicator;
                    row["Department"] = department;
                    row["Value"] = IndicatorValue.GetDepartmentIndicatorValueValue(imsDbContext, department.DepartmentID, indicator.IndicatorID, start, end);

                    dataSet.Tables["ViewValue"].Rows.Add(row);
                }
            }

            //筛选“科室”和“指标”的独立项。
            dataSet.Tables.Add(dataSet.Tables["ViewValue"].DefaultView.ToTable("Department", true, "Department"));
            dataSet.Tables.Add(dataSet.Tables["ViewValue"].DefaultView.ToTable("Indicator", true, "Indicator"));

            //创建最终用于返回的透视表。
            dataSet.Tables.Add("PivotView");

            //为透视表增加列。
            dataSet.Tables["PivotView"].Columns.Add("Department");
            foreach (System.Data.DataRow row in dataSet.Tables["Indicator"].Rows)
            {
                dataSet.Tables["PivotView"].Columns.Add(((Models.Indicator)row["Indicator"]).Name);
            }

            //为透视表增加行。
            foreach (System.Data.DataRow row in dataSet.Tables["Department"].Rows)
            {
                var newPivotViewRow = dataSet.Tables["PivotView"].NewRow();
                //添加首列“科室名称”
                newPivotViewRow["Department"] = ((IMS.Models.Department)row["Department"]).DepartmentName;
                dataSet.Tables["PivotView"].Rows.Add(newPivotViewRow);
            }

            //为透视表填入数值。
            foreach (System.Data.DataRow row in dataSet.Tables["ViewValue"].Rows)
            {
                dataSet.Tables["PivotView"].Select("Department='" + ((IMS.Models.Department)row["Department"]).DepartmentName + "'")[0][((IMS.Models.Indicator)row["Indicator"]).Name] = (decimal)row["Value"];
            }

            //返回透视表。
            return dataSet.Tables["PivotView"];
        }
    }
}