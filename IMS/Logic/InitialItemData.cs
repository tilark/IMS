using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OperateExcel;
using IMS.Models;
namespace IMS.Logic
{
    public class IndicatorItem
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public string IsAuto { get; set; }
        public string DataSystem { get; set; }
        public string Department { get; set; }
        public string Remarks { get; set; }
        public string DutyDepartment { get; set; }
    }
    public class InitialItemData
    {

        public InitialItemData()
        {
        }
        /// <summary>
        /// 初始化科室与科室类别
        /// </summary>
        public void InitialDepartmentAndCategory()
        {
            var departmentCategoryFile = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["DepartmentCategory"]);
            //第一列为Category，先获取，再填充到
            //从第二列开始，每列为科室，所属类别为第一列
            //获取当前表格的总共Row数
            ReadFromExcel readFromExcel = new ReadFromExcel();
            //按Row获取当前行的所有数据
            int rowCount = readFromExcel.GetRowCount(departmentCategoryFile);
            for (int i = 1; i <= rowCount; i++)
            {
                var columnData = readFromExcel.ReadRowFromExcel((uint)i, departmentCategoryFile);
                //获取第一个数为类别
                if (columnData.Count > 0)
                {
                    var firstData = columnData.First();

                    WriteBaseData writeBaseData = new WriteBaseData();
                    DepartmentCategory departmentCategory = new DepartmentCategory();
                    //将第一个数当作类别，插入科室类别表中
                    departmentCategory.Name = firstData;
                    departmentCategory = writeBaseData.AddDepartmentCategory(departmentCategory);
                    if (departmentCategory == null)
                    {
                        return;
                    }
                    columnData.Remove(firstData);

                    foreach (var data in columnData)
                    {
                        //依次将第二个列之后的科室名插入到科室表中
                        Department department = new Department();
                        department.DepartmentName = data;
                        department.DepartmentCategoryID = departmentCategory.DepartmentCategoryID;
                        writeBaseData.AddDepartment(department);
                    }
                }

            }
        }
        /// <summary>
        /// 初始化Indicator项目
        /// </summary>
        public void InitialIndicator()
        {
            var indicatorFile = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["Indicator"]);
            ReadFromExcel readFromExcel = new ReadFromExcel();
            //按Row获取当前行的所有数据
            //先获Row总数，从第二行开始取数据，第一行为标题
            int rowCount = readFromExcel.GetRowCount(indicatorFile);
            for (int i = 2; i <= rowCount; i++)
            {
                var columnData = readFromExcel.ReadRowFromExcel((uint)i, indicatorFile);
                //将该列按照IndicatorItem录入，再插入到数据库
                if (columnData.Count > 0)
                {
                    IndicatorItem indicatorItem = new IndicatorItem();
                    //按照顺序填充
                    indicatorItem.Name = columnData.ElementAt(0);
                    indicatorItem.Unit = columnData.ElementAt(1);
                    indicatorItem.IsAuto = columnData.ElementAt(2);
                    indicatorItem.DataSystem = columnData.ElementAt(3);
                    indicatorItem.Department = columnData.ElementAt(4);
                    indicatorItem.Remarks = columnData.ElementAt(5);
                    indicatorItem.DutyDepartment = columnData.ElementAt(6);

                    //写入数据库
                    WriteBaseData writeBaseData = new WriteBaseData();
                    writeBaseData.AddIndicator(indicatorItem);
                }
            }
        }
    }
}