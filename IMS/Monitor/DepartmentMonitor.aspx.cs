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
using System.Web.ModelBinding;

namespace IMS.Monitor
{
    public partial class DepartmentMonitor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initialBaseItem();
            }
        }

        private void initialBaseItem()
        {
            dlDepatmentTypeBind();
            dlDepatmentBind();
            dlMonitorItemBind();
        }
        // 返回类型可以更改为 IEnumerable，但是为了支持
        // 分页和排序，必须添加以下参数:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<IMS.Models.DepartmentMonitor> lvDepartmentMonitor_GetData([Control] Int64? dlType, [Control] Int64? dlName, [Control] Int64? dlItem, [Control] string txtDate)
        {
            IQueryable<IMS.Models.DepartmentMonitor> query = null;
            MonitorDbContext context = new MonitorDbContext();
            query = context.DepartmentMonitors.Include(i => i.Department).Include(i => i.DepartmentType).Include(i => i.MonitorItem)
                .OrderBy(o => o.Department.DepartmentName);
            if( txtDate != null)
            {
                var time = DateTime.Parse(txtDate);
                if (time != null)
                {
                    query = query.Where(q => q.Time.Year == time.Year && q.Time.Month == time.Month);
                }
            }
            
            if (dlType != null && dlType != -1)
            {
                query = query.Where(q => q.DepartmentTypeID == dlType);
            }
            if (dlName != null && dlName != -1)
            {
                query = query.Where(q => q.DepartmentID == dlName);
            }

            if (dlItem != null && dlItem != -1)
            {
                query = query.Where(q => q.MonitorItemID == dlItem);
            }

            return query;
        }

        // id 参数名应该与控件上设置的 DataKeyNames 值匹配
        public void lvDepartmentMonitor_UpdateItem(Int64 id)
        {
            TextBox txtEditValue = new TextBox();
            txtEditValue = (TextBox)lvDepartmentMonitor.EditItem.FindControl("txtValue");
            var value = txtEditValue?.Text;
            if (String.IsNullOrEmpty(value))
            {
                return;
            }
            using (MonitorDbContext context = new MonitorDbContext())
            {
                IMS.Models.DepartmentMonitor item = null;
                // 在此加载该项，例如 item = MyDataLayer.Find(id);
                item = context.DepartmentMonitors.Find(id);
                if (item == null)
                {
                    // 未找到该项
                    ModelState.AddModelError("", String.Format("未找到 id 为 {0} 的项", id));
                    return;
                }
                TryUpdateModel(item);
                if (ModelState.IsValid)
                {
                    // 在此保存更改，例如 MyDataLayer.SaveChanges();
                    item.Value = value;
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
        public void lvDepartmentMonitor_DeleteItem(Int64 id)
        {
            using (MonitorDbContext context = new MonitorDbContext())
            {
                IMS.Models.DepartmentMonitor item = null;
                item = context.DepartmentMonitors.Find(id);
                if (item == null)
                {
                    // 未找到该项
                    ModelState.AddModelError("", String.Format("未找到 id 为 {0} 的项", id));
                    return;
                }
                
                TryUpdateModel(item);
                if (ModelState.IsValid)
                {
                    // 在此保存更改，例如 MyDataLayer.SaveChanges();
                    context.DepartmentMonitors.Remove(item);
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
        private void dlDepatmentBind()
        {
            dlName.DataSource = new GetBaseData().GetDepartmentDic();
            dlName.DataTextField = "Value";
            dlName.DataValueField = "Key";
            dlName.DataBind();
        }
        private void dlDepatmentTypeBind()
        {

            dlType.DataSource = new GetBaseData().GetDepartmentTypeDic();
            dlType.DataTextField = "Value";
            dlType.DataValueField = "Key";
            dlType.DataBind();
        }
        private void dlMonitorItemBind()
        {
            dlItem.DataSource = new GetBaseData().GetMonitorItemDic();
            dlItem.DataTextField = "Value";
            dlItem.DataValueField = "Key";
            dlItem.DataBind();
        }
        protected void search_Click(object sender, EventArgs e)
        {

        }
    }
}