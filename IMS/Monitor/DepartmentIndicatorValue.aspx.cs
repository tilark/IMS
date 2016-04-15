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
    public partial class DepartmentIndicatorValue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dlDepatmentBind();
            }
        }
        private void dlDepatmentBind()
        {
            var departmentDic = new GetBaseData().GetDepartmentDic();
            dlDepartment.DataSource = departmentDic;
            ListItem listItem = new ListItem();
            listItem.Text = null;
            listItem.Value = "-全选-";
            dlDepartment.Items.Insert(0, listItem);
            dlDepartment.DataTextField = "Value";
            dlDepartment.DataValueField = "Key";
            dlDepartment.DataBind();
        }
        // 返回类型可以更改为 IEnumerable，但是为了支持
        // 分页和排序，必须添加以下参数:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<IMS.Models.DepartmentIndicatorValue> lvDepartmentIndicatorValue_GetData([Control] Guid? dlDepartment, [Control] DateTime? txtDateFrom, [Control] DateTime? txtDateTo, [Control] string txtItem)
        {
            IQueryable<IMS.Models.DepartmentIndicatorValue> query = null;
            ImsDbContext context = new ImsDbContext();
            query = context.DepartmentIndicatorValues.Include(i => i.Department).Include(i => i.Indicator).OrderBy(i => i.Department.DepartmentName);
            if(dlDepartment != null)
            {
                query = query.Where(q => q.DepartmentID == dlDepartment).OrderBy(q => q.Time);
            }
            if(txtDateFrom != null && txtDateTo != null)
            {
                var dateFrom = DateTime.Parse(txtDateFrom.ToString());
                var dateTo = DateTime.Parse(txtDateTo.ToString());
                query = query.Where(q => DateTime.Compare(q.Time, dateFrom) >= 0 && DateTime.Compare(q.Time, dateTo) <= 0);
            }
            if(txtItem != null)
            {
                query = query.Where(i => i.Indicator.Name.Contains(txtItem));
            }

            return query;
        }

        // id 参数名应该与控件上设置的 DataKeyNames 值匹配
        public void lvDepartmentIndicatorValue_UpdateItem(Guid id)
        {
            IMS.Models.DepartmentIndicatorValue item = null;
            // 在此加载该项，例如 item = MyDataLayer.Find(id);
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

            }
        }

        protected void search_Click(object sender, EventArgs e)
        {

        }
    }
}