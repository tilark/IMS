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
            DepartmentCategory item = new DepartmentCategory();
            using (ImsDbContext context = new ImsDbContext())
            {
                //先用ID查重
                if (departmentCategory.DepartmentCategoryID != null)
                {
                    item = context.DepartmentCategories.Find(departmentCategory.DepartmentCategoryID);
                }
                //或用Name查重
                else if(departmentCategory.Name != null)
                {
                    item = context.DepartmentCategories.Where(d => d.Name == departmentCategory.Name).FirstOrDefault();
                }
                if(item == null)
                {
                    //如果为null，说明数据库不存在该项，添加
                    context.DepartmentCategories.Add(item);
                    context.SaveChanges();
                }
            }
            return item;
        }
        public Department AddDepartment(Department department)
        {
            Department item = new Department();

            using (ImsDbContext context = new ImsDbContext())
            {
                //先用ID查重
                if (department.DepartmentID != null)
                {
                    item = context.Departments.Find(department.DepartmentCategoryID);
                }
                //或用Name查重
                else if (department.DepartmentName != null)
                {
                    item = context.Departments.Where(d => d.DepartmentName == department.DepartmentName).FirstOrDefault();
                }
                if (item == null)
                {
                    //如果为null，说明数据库不存在该项，添加
                    context.Departments.Add(item);
                    context.SaveChanges();
                }
            }
            return item;
        }

    }

}