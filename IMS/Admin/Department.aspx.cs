using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using IMS.Logic;
using IMS.Models;
using System.Data.Entity.Infrastructure;
namespace IMS.Admin
{
    public partial class Department : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // 返回类型可以更改为 IEnumerable，但是为了支持
        // 分页和排序，必须添加以下参数:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<IMS.Models.Department> lvDepartment_GetData()
        {
            IQueryable<IMS.Models.Department> query = null;
            ImsDbContext context = new ImsDbContext();
            query = context.Departments.OrderBy(o => o.DepartmentName).Include(i => i.DepartmentCategory)
                .OrderBy(d => d.DepartmentName).AsQueryable();
            
            return query;
        }

        public void lvDepartment_InsertItem()
        {
            var item = new IMS.Models.Department();
            TextBox txtName = new TextBox();
            txtName = (TextBox)lvDepartment.InsertItem.FindControl("txtInsertName");
            var name = txtName?.Text;
            TextBox txtRemark = new TextBox();
            txtRemark = (TextBox)lvDepartment.InsertItem.FindControl("txtInsertRemark");
            var remark = txtRemark?.Text;

            TextBox txtInsertCategory = new TextBox();
            txtInsertCategory = (TextBox)lvDepartment.InsertItem.FindControl("txtInsertCategory");
            var categoryName = txtInsertCategory?.Text;

            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(categoryName))
            {
                ModelState.AddModelError("", "请输入科室名称和科室类别!");
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                // Save changes here
               using (ImsDbContext context = new ImsDbContext())
                {
                    //需检查是否有重名情况
                    var query = context.Departments.Where(n => n.DepartmentName == name).FirstOrDefault();
                    if(query != null)
                    {
                        ModelState.AddModelError("", String.Format("科室 {0} 已存在！", name));
                        return;
                    }
                    var categoryQuery = context.DepartmentCategories.Where(d => d.Name == categoryName).FirstOrDefault();
                    if (categoryQuery == null)
                    {
                        //输入的科室类别不存在，不能修改
                        ModelState.AddModelError("", String.Format("科室类别 {0} 不存在！", categoryQuery));

                        return;
                    }
                    item.DepartmentID = new Guid();
                    item.DepartmentName = name;
                    item.DepartmentCategoryID = categoryQuery.DepartmentCategoryID;
                    item.Remarks = remark;
                    context.Departments.Add(item);
                    context.SaveChanges();
                }

            }
        }

        // id 参数名应该与控件上设置的 DataKeyNames 值匹配
        public void lvDepartment_UpdateItem(Guid DepartmentID)
        {
            TextBox txtEditName = new TextBox();
            txtEditName = (TextBox)lvDepartment.EditItem.FindControl("txtEditName");
            var name = txtEditName?.Text;
            TextBox txtEditRemark = new TextBox();
            txtEditRemark = (TextBox)lvDepartment.EditItem.FindControl("txtEditRemark");
            var remark = txtEditRemark?.Text;

            TextBox txtEditCategory = new TextBox();
            txtEditCategory = (TextBox)lvDepartment.EditItem.FindControl("txtEditCategory");
            var categoryName = txtEditCategory?.Text;
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(categoryName))
            {
                return;
            }
            using (ImsDbContext context = new ImsDbContext())
            {
                IMS.Models.Department item = null;
                // 在此加载该项，例如 item = MyDataLayer.Find(id);
                item = context.Departments.Find(DepartmentID);
                if (item == null)
                {
                    // 未找到该项
                    ModelState.AddModelError("", String.Format("未找到 id 为 {0} 的项", DepartmentID));
                    return;
                }
                TryUpdateModel(item);
                if (ModelState.IsValid)
                {
                    // 在此保存更改，例如 MyDataLayer.SaveChanges();
                    //不重名，更改科室名称
                    var query = context.Departments.Where(n => n.DepartmentName == name && n.DepartmentID != item.DepartmentID).FirstOrDefault();
                    if (query != null)
                    {
                        ModelState.AddModelError("", String.Format("科室 {0} 已存在！", name));
                        return;
                    }
                    var categoryQuery = context.DepartmentCategories.Where(d => d.Name == categoryName).FirstOrDefault();
                    if(categoryQuery == null)
                    {
                        //输入的科室类别不存在，不能修改
                        ModelState.AddModelError("", String.Format("科室类别 {0} 不存在！", categoryQuery));

                        return;
                    }
                    item.DepartmentName = name;
                    item.Remarks = remark;
                    item.DepartmentCategoryID = categoryQuery.DepartmentCategoryID;
                    //database win
                    bool saveFailed;
                    do
                    {
                        saveFailed = false;
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (DbUpdateConcurrencyException ex)
                        {
                            saveFailed = true;
                            // Update the values of the entity that failed to save from the store 
                            ex.Entries.Single().Reload();
                        }
                    } while (saveFailed);
                }
            }
            
        }

        // id 参数名应该与控件上设置的 DataKeyNames 值匹配
        public void lvDepartment_DeleteItem(Guid DepartmentID)
        {
            using (ImsDbContext context = new ImsDbContext())
            {
                IMS.Models.Department item = null;
                item = context.Departments.Find(DepartmentID);
                if (item == null)
                {
                    // 未找到该项
                    ModelState.AddModelError("", String.Format("未找到 id 为 {0} 的项", DepartmentID));
                    return;
                }
                //检查在DepartmentMonitors中是否存在该信息

                var queryDepartmentMonitor = context.DepartmentIndicatorStandardValues.Where(d => d.DepartmentID == DepartmentID).FirstOrDefault();
                if (queryDepartmentMonitor != null)
                {
                    //DepartmentMonitors 中存在该信息，不能删除
                    ModelState.AddModelError("", String.Format("在科室项目值表中存在 {0} 的项，禁止删除", item.DepartmentName));
                    return;
                }
                TryUpdateModel(item);
                if (ModelState.IsValid)
                {
                    // 在此保存更改，例如 MyDataLayer.SaveChanges();
                    context.Departments.Remove(item);
                    //database win
                    bool saveFailed;
                    do
                    {
                        saveFailed = false;
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (DbUpdateConcurrencyException ex)
                        {
                            saveFailed = true;
                            // Update the values of the entity that failed to save from the store 
                            ex.Entries.Single().Reload();
                        }
                    } while (saveFailed);

                }
            }
        }




        
    }
}