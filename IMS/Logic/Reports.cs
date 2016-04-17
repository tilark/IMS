using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Logic
{
    public static class Reports
    {
        public static System.Data.DataTable GetReport(Models.ImsDbContext imsDbContext, Guid departmentCategoryId, DateTime time, int duarationMonth)
        {
            return GetReport(imsDbContext, departmentCategoryId, new DateTime(time.Year, time.Month, 1), new DateTime(time.Year, time.Month, 1).AddMonths(duarationMonth).AddDays(-1));
        }

        public static System.Data.DataTable GetReport(Models.ImsDbContext imsDbContext, Guid departmentCategoryId, DateTime start, DateTime end)
        {
            System.Data.SqlClient.SqlConnection connection = (System.Data.SqlClient.SqlConnection)imsDbContext.Database.Connection;
            System.Data.SqlClient.SqlCommand command = connection.CreateCommand();
            System.Data.DataSet dataSet = new System.Data.DataSet();
            System.Data.SqlClient.SqlDataAdapter dataAdapter = new System.Data.SqlClient.SqlDataAdapter();

            dataSet.Tables.Add(new System.Data.DataTable("ViewValue"));
            dataSet.Tables["ViewValue"].Columns.Add(new System.Data.DataColumn("Indicator", typeof(Models.Indicator)));
            dataSet.Tables["ViewValue"].Columns.Add(new System.Data.DataColumn("Department", typeof(Models.Department)));
            dataSet.Tables["ViewValue"].Columns.Add(new System.Data.DataColumn("Value", typeof(decimal)));

            //获取实参指定的“科室类别”所涉及的“指标”。
            var listIndicator = imsDbContext.Indicators.Where(i => i.DepartmentCategoryIndicatorMaps.Any(j => j.DepartmentCategoryID == departmentCategoryId)).ToList();

            //获取实参指定的“科室类别”所涉及的“科室”。
            var listDepartment = imsDbContext.Departments.Where(i => i.DepartmentCategoryID == departmentCategoryId);

            //将“科室”和“指标”组合，并获取值。
            IndicatorValue indicatorValue = new IndicatorValue();//计算器

            foreach (var department in listDepartment)
            {
                foreach (var indicator in listIndicator)
                {
                    var row = dataSet.Tables["ViewValue"].NewRow();
                    row["Indicator"] = indicator;
                    row["Department"] = department;
                    row["Value"] = indicatorValue.GetDepartmentIndicatorValueValueByCalculate(department.DepartmentID, indicator.IndicatorID, start, end);
                }
            }

            //执行Pivot操作，整合最终报表。

        }
    }
}