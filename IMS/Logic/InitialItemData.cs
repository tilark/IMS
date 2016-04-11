using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OperateExcel;
using IMS.Models;
namespace IMS.Logic
{
    public class InitialItemData
    {
        private string MonitorItemSource = null;
        private string DepartmentSource = null;
        public InitialItemData()
        {
            this.MonitorItemSource = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["IndicatorItemSource"]);
        }
        public void InitialDepartmentAndCategory()
        {
            var departmentCategoryFile = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["DepartmentCategory"]);
            //第一列为Category，先获取，再填充到
            //从第二列开始，每列为科室，所属类别为第一列
            //获取当前表格的总共Row数
            ReadFromExcel readFromExcel = new ReadFromExcel();
            //按Row获取当前行的所有数据
            int rowCount = readFromExcel.GetRowCount(departmentCategoryFile);
            for (int i = 0; i <= rowCount; i++)
            {
                var columnData = readFromExcel.ReadRowFromExcel((uint)i, departmentCategoryFile);
                //获取第一个数为类别
                if (columnData.Count > 0)
                {
                    WriteBaseData writeBaseData = new WriteBaseData();
                    DepartmentCategory departmentCategory = new DepartmentCategory();
                    var firstData = columnData.FirstOrDefault();
                    if (firstData != null)
                    {
                        //将第一个数当作类别，插入科室类别表中
                        departmentCategory.Name = firstData;
                        departmentCategory = writeBaseData.AddDepartmentCategory(departmentCategory);
                        columnData.Remove(firstData);
                    }
                    foreach (var data in columnData)
                    {
                        //依次将第二个列之后的科室名插入到科室表中
                        Department department = new Department();
                        department.DepartmentName = data;
                        department.DepartmentCategoryID = departmentCategory.DepartmentCategoryID;
                        department = writeBaseData.AddDepartment(department);
                    }
                }

            }
        }

    }
}