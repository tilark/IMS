using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMS.Logic;
using IMS.Models;
using System.Data.Entity.Infrastructure;
using System.Web.ModelBinding;
namespace IMS.Monitor
{
    public partial class AddMonitorData : System.Web.UI.Page
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


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var item = new IMS.Models.DepartmentIndicatorValue();
            if (dlName != null && Guid.Parse(dlName.SelectedValue) != -1)
            {
                item.DepartmentID = Int64.Parse(dlName.SelectedValue);

            }
            if (dlType != null && Int64.Parse(dlType.SelectedValue) != -1)
            {
                item.DepartmentCategoryID = Int64.Parse(dlType.SelectedValue);
            }
            if (dlItem != null && Int64.Parse(dlItem.SelectedValue) != -1)
            {
                item.IndicatorID = Int64.Parse(dlItem.SelectedValue);
            }
            if (txtDate != null && !String.IsNullOrEmpty(txtDate.Text))
            {
                item.Time = DateTime.Parse(txtDate.Text);
            }
            else
            {
                item.Time = DateTime.Now;
            }
            item.Value = txtValue.Text;

            if(item.DepartmentID ==null || item.DepartmentCategoryID == null || item.IndicatorID < 0 || item.Time == null)
            {
                return;
            }
            using(ImsDbContext context = new ImsDbContext())
            {
                //需检查是否有重复项
                var query = context.DepartmentIndicatorValues.Where(d => d.DepartmentID == item.DepartmentID && d.DepartmentCategoryID == item.DepartmentCategoryID
                && d.IndicatorID == item.IndicatorID && d.Time.Year == item.Time.Year && d.Time.Month == item.Time.Month).FirstOrDefault();
                if(query != null)
                {
                    return;
                }
                context.DepartmentIndicatorValues.Add(item);
                context.SaveChanges();
            }
            Response.Redirect("AddMonitorData.aspx");
        }
    }
}