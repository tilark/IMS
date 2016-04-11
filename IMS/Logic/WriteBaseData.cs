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
            using (ImsDbContext context = new ImsDbContext())
            {
                //先用ID查重
                if (departmentCategory.Name != null)
                {
                    item = context.DepartmentCategories.Where(d => d.Name == departmentCategory.Name).FirstOrDefault();

                }
                //或用Name查重
                else
                {
                    if (departmentCategory.DepartmentCategoryID != null)
                    {
                        item = context.DepartmentCategories.Find(departmentCategory.DepartmentCategoryID);
                    }
                }
                if (item == null)
                {
                    //如果为null，说明数据库不存在该项，添加
                    item = departmentCategory;
                    item.DepartmentCategoryID = System.Guid.NewGuid();

                    context.DepartmentCategories.Add(item);
                    context.SaveChanges();
                }
            }
            return item;
        }
        public Department AddDepartment(Department department)
        {
            Department item = null;

            using (ImsDbContext context = new ImsDbContext())
            {
                //先用ID查重
                if (department.DepartmentName != null)
                {
                    item = context.Departments.Where(d => d.DepartmentName == department.DepartmentName).FirstOrDefault();
                }
                //或用Name查重
                else
                {
                    if (department.DepartmentID != null)
                    {
                        item = context.Departments.Find(department.DepartmentCategoryID);

                    }
                }
                if (item == null)
                {
                    //如果为null，说明数据库不存在该项，添加
                    item = department;
                    item.DepartmentID = System.Guid.NewGuid();
                    context.Departments.Add(item);
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
        public bool IsInDepartmentByName(string name)
        {
            bool result = true;
            if (name != null)
            {
                Department item = null;
                using (ImsDbContext context = new ImsDbContext())
                {
                    item = context.Departments.Where(d => d.DepartmentName == name).FirstOrDefault();

                    if (item == null)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
        public bool IsInDepartmentCategoryByName(string name)
        {
            bool result = true;
            if (name != null)
            {
                DepartmentCategory item = null;
                using (ImsDbContext context = new ImsDbContext())
                {
                    item = context.DepartmentCategories.Where(d => d.Name == name).FirstOrDefault();
                    if (item == null)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
    }

}