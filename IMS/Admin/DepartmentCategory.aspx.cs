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
    public partial class DepartmentCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void lvDepartmentCategory_InsertItem()
        {
            var item = new IMS.Models.DepartmentCategory();
            TextBox txtName = new TextBox();
            txtName = (TextBox)lvDepartmentCategory.InsertItem.FindControl("txtInsertName");
            var name = txtName?.Text;
            TextBox txtRemark = new TextBox();
            txtRemark = (TextBox)lvDepartmentCategory.InsertItem.FindControl("txtInsertRemark");
            var remark = txtRemark?.Text;
            if (String.IsNullOrEmpty(name))
            {
                ModelState.AddModelError("", "请输入名称!");
                return;
            }
            TryUpdateModel(item);
            if (ModelState.IsValid)
            {
                // Save changes here
                using (ImsDbContext context = new ImsDbContext())
                {
                    //需检查是否有重名情况
                    var query = context.DepartmentCategories.Where(n => n.Name == name).FirstOrDefault();
                    if (query != null)
                    {
                        ModelState.AddModelError("", String.Format("项目 {0} 已存在！", name));
                        return;
                    }
                    item.DepartmentCategoryID = new Guid();
                    item.Name = name;
                    item.Remarks = remark;
                    context.DepartmentCategories.Add(item);
                    context.SaveChanges();
                }
            }
        }

        // 返回类型可以更改为 IEnumerable，但是为了支持
        // 分页和排序，必须添加以下参数:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<IMS.Models.DepartmentCategory> lvDepartmentCategory_GetData()
        {
            IQueryable<IMS.Models.DepartmentCategory> query = null;
            ImsDbContext context = new ImsDbContext();
            query = context.DepartmentCategories.OrderBy(o => o.Name);
            return query;
        }

        // id 参数名应该与控件上设置的 DataKeyNames 值匹配
        public void lvDepartmentType_UpdateItem(Guid DepartmentCategoryID)
        {
            TextBox txtEditName = new TextBox();
            txtEditName = (TextBox)lvDepartmentCategory.EditItem.FindControl("txtEditName");
            var name = txtEditName?.Text;
            TextBox txtEditRemark = new TextBox();
            txtEditRemark = (TextBox)lvDepartmentCategory.EditItem.FindControl("txtEditRemark");
            var remark = txtEditRemark?.Text;
            if (String.IsNullOrEmpty(name))
            {
                return;
            }
            using (ImsDbContext context = new ImsDbContext())
            {
                IMS.Models.DepartmentCategory item = null;
                // 在此加载该项，例如 item = MyDataLayer.Find(id);
                item = context.DepartmentCategories.Find(DepartmentCategoryID);
                if (item == null)
                {
                    // 未找到该项
                    ModelState.AddModelError("", String.Format("未找到 id 为 {0} 的项", DepartmentCategoryID));
                    return;
                }
                TryUpdateModel(item);
                if (ModelState.IsValid)
                {
                    // 在此保存更改，例如 MyDataLayer.SaveChanges();
                    //不重名，更改项目名称
                    var query = context.DepartmentCategories.Where(n => n.Name == name && n.DepartmentCategoryID != item.DepartmentCategoryID).FirstOrDefault();
                    if (query != null)
                    {
                        ModelState.AddModelError("", String.Format("项目 {0} 已存在！", name));
                        return;
                    }
                    item.Name = name;
                    item.Remarks = remark;
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
        public void lvDepartmentCategory_DeleteItem(Guid DepartmentCategoryID)
        {
            using (ImsDbContext context = new ImsDbContext())
            {
                IMS.Models.DepartmentCategory item = null;
                item = context.DepartmentCategories.Find(DepartmentCategoryID);
                if (item == null)
                {
                    // 未找到该项
                    ModelState.AddModelError("", String.Format("未找到 id 为 {0} 的项", DepartmentCategoryID));
                    return;
                }
                //检查在DepartmentMonitors中是否存在该信息

                var query = context.DepartmentCategoryIndicatorMaps.Where(d => d.DepartmentCategoryID == DepartmentCategoryID).FirstOrDefault();
                if (query != null)
                {
                    //DepartmentMonitors 中存在该信息，不能删除
                    ModelState.AddModelError("", String.Format("在科室类别项目映射表中存在 {0} 的项，禁止删除", item.Name));
                    return;
                }
                TryUpdateModel(item);
                if (ModelState.IsValid)
                {
                    // 在此保存更改，例如 MyDataLayer.SaveChanges();
                    context.DepartmentCategories.Remove(item);
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