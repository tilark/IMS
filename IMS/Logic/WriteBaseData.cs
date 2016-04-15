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
    public class WriteBaseData
    {
        public DepartmentCategory AddDepartmentCategory(DepartmentCategory departmentCategory)
        {

            //查重，如果数据库中已存在，返回该项
            DepartmentCategory item = null;
            if (departmentCategory != null && departmentCategory.Name == null)
            {
                return item;
            }
            using (ImsDbContext context = new ImsDbContext())
            {
                //先用ID查重
                if (departmentCategory.DepartmentCategoryID != null)
                {
                    item = context.DepartmentCategories.Find(departmentCategory.DepartmentCategoryID);
                }
                //或用Name查重
                else
                {
                    if (departmentCategory.Name != null)
                    {
                        item = context.DepartmentCategories.Where(d => d.Name == departmentCategory.Name).FirstOrDefault();

                    }
                }
                if (item == null)
                {
                    //如果为null，说明数据库不存在该项，添加
                    item = departmentCategory;

                    context.DepartmentCategories.Add(item);
                    context.SaveChanges();
                }
            }
            return item;
        }
        public Department AddDepartment(Department department)
        {
            Department item = null;
            if (department == null || department.DepartmentName == null)
            {
                return item;
            }
            using (ImsDbContext context = new ImsDbContext())
            {
                //先用ID查重

                if (department.DepartmentID != null)
                {
                    item = context.Departments.Find(department.DepartmentID);

                }
                //或用Name查重
                else
                {
                    if (department.DepartmentName != null)
                    {
                        item = context.Departments.Where(d => d.DepartmentName == department.DepartmentName).FirstOrDefault();
                    }
                }
                if (item == null)
                {
                    //如果为null，说明数据库不存在该项，添加
                    item = department;
                    //item.DepartmentID = System.Guid.NewGuid();
                    context.Departments.Add(item);
                    context.SaveChanges();
                }
            }
            return item;
        }
        internal DataSourceSystem AddDataSourceSystem(DataSourceSystem dataSourceSystem)
        {
            DataSourceSystem item = null;
            if (dataSourceSystem != null && dataSourceSystem.Name == null)
            {
                return item;
            }
            using (ImsDbContext context = new ImsDbContext())
            {
                //先用ID查重
                if (dataSourceSystem.ID != null)
                {
                    item = context.DataSourceSystems.Find(dataSourceSystem.ID);
                }
                //或用Name查重
                else
                {
                    if (dataSourceSystem.Name != null)
                    {
                        item = context.DataSourceSystems.Where(d => d.Name == dataSourceSystem.Name).FirstOrDefault();

                    }
                }
                if (item == null)
                {
                    //如果为null，说明数据库不存在该项，添加
                    item = dataSourceSystem;
                    context.DataSourceSystems.Add(item);
                    context.SaveChanges();
                }
            }
            return item;
        }

        public IndicatorAlgorithm AddIndicatorAlgorithm(IndicatorAlgorithm indicatorAlgorithm)
        {
            IndicatorAlgorithm item = null;
            if (indicatorAlgorithm == null)
            {
                return item;
            }
            using (ImsDbContext context = new ImsDbContext())
            {
                //先用ID查重
                if (indicatorAlgorithm.ID != null)
                {
                    item = context.IndicatorAlgorithms.Find(indicatorAlgorithm.ID);
                }
                //再用ResultOperation查找，如果能找到，则也不加入
                if (item == null && indicatorAlgorithm.ResultOperationID != null)
                {
                    item = context.IndicatorAlgorithms.Where(i => i.ResultOperationID == indicatorAlgorithm.ResultOperationID).FirstOrDefault();
                }
                if (item == null)
                {
                    //如果仍然没有，则可以添加到数据库
                    item = indicatorAlgorithm;
                    context.IndicatorAlgorithms.Add(item);
                    context.SaveChanges();
                }
            }
            return item;
        }

        /// <summary>
        /// 利用科室名称检测是否在科室表中.
        /// </summary>
        /// <param name="name">Name of the department.</param>
        /// <returns><c>true</c> 如果存在，返回Ture, <c>false</c>.</returns>
        public Department FindDepartmentByName(string name)
        {
            Department item = null;
            if (name != null)
            {
                using (ImsDbContext context = new ImsDbContext())
                {
                    item = context.Departments.Where(d => d.DepartmentName == name).FirstOrDefault();
                }
            }
            return item;
        }



        public DepartmentCategory FindDepartmentCategoryByName(string name)
        {
            DepartmentCategory item = null;
            if (name != null)
            {
                using (ImsDbContext context = new ImsDbContext())
                {
                    item = context.DepartmentCategories.Where(d => d.Name == name).FirstOrDefault();
                }
            }
            return item;
        }
        /// <summary>
        /// Adds the indicator.
        /// </summary>
        /// <param name="indicatorItem">代码中的对应映射类.</param>
        public void AddIndicator(IndicatorItem indicatorItem)
        {
            if (indicatorItem == null)
            {
                return;
            }
            using (ImsDbContext context = new ImsDbContext())
            {
                //先通过Id查重，如果存在，不添加
                Indicator item = new Indicator();
                var query = context.Indicators.Where(i => i.IndicatorID == indicatorItem.GuidId).FirstOrDefault();
                if (query != null)
                {
                    //已存在，返回
                    return;
                }
                //根据IsAuto从不同表中查找ID
                if (indicatorItem.IsAuto == "是")
                {
                    item.IsAutoGetData = true;
                    var dataSystem = context.DataSourceSystems.Where(d => d.Name == indicatorItem.DataSystem).FirstOrDefault();
                    if (dataSystem == null)
                    {
                        //若不存在，直接返回,
                        return;
                    }
                    //需获取DataSystem的ID值
                    item.DataSourceSystemID = dataSystem.ID;
                    //item.DepartmentID = null;
                }
                else
                {
                    item.IsAutoGetData = false;
                    var department = context.Departments.Where(d => d.DepartmentName == indicatorItem.Department).FirstOrDefault();
                    if (department == null)
                    {
                        //如果科室不存在，说明有问题，不能再继续下一步操作
                        return;
                    }
                    //获取department的ID
                    item.DepartmentID = department.DepartmentID;
                    //item.DataSourceSystemID = null;
                }
                //继续赋其他值
                item.Name = indicatorItem.Name;
                item.Unit = indicatorItem.Unit;
                item.DutyDepartment = indicatorItem?.DutyDepartment;
                item.Remarks = indicatorItem?.Remarks;
                //添加到数据库
                item.IndicatorID = indicatorItem.GuidId;
                context.Indicators.Add(item);
                context.SaveChanges();
            }
        }

        public void AddDepartmentCategoryIndicatorMap(DepartmentCategoryIndicatorMap item)
        {
            if (item.IndicatorID == null || item.DepartmentCategoryID == null)
            {
                return;
            }
            //根据DepartmentCategoryID与IndicatorID查重
            using (ImsDbContext context = new ImsDbContext())
            {
                var query = context.DepartmentCategoryIndicatorMaps.Where(d => d.DepartmentCategoryID == item.DepartmentCategoryID &&
                    d.IndicatorID == item.IndicatorID).FirstOrDefault();

                if (query == null)
                {
                    //加入数据库
                    DepartmentCategoryIndicatorMap newItem = new DepartmentCategoryIndicatorMap();
                    newItem = item;
                    newItem.ID = System.Guid.NewGuid();
                    context.DepartmentCategoryIndicatorMaps.Add(newItem);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Finds the name of the indicator by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>IMS.Models.Indicator.</returns>
        public Indicator FindIndicatorByName(string name)
        {
            Indicator item = null;
            if (name != null)
            {
                using (ImsDbContext context = new ImsDbContext())
                {
                    item = context.Indicators.Where(i => i.Name == name).FirstOrDefault();
                }
            }
            return item;
        }
    }

}