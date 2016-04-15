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
namespace IMS.Admin
{
    public partial class AdminDepartmentIndicatorValue : System.Web.UI.Page
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
        public IQueryable<IMS.Models.DepartmentIndicatorValue> lvDepartmentIndicatorValue_GetData()
        {
            IQueryable<IMS.Models.DepartmentIndicatorValue> query = null;
            ImsDbContext context = new ImsDbContext();
            //query = context.DepartmentIndicatorValues.Include(i => i.Department).Include(i => i.Indicator).OrderBy(i => i.Department.DepartmentName);

            return query;
        }

        // id 参数名应该与控件上设置的 DataKeyNames 值匹配
        public void lvDepartmentIndicatorValue_UpdateItem(Guid id)
        {
            //管理员管理此项目，可不设删除限制
            using (ImsDbContext context = new ImsDbContext())
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
            
        }

        // id 参数名应该与控件上设置的 DataKeyNames 值匹配
        public void lvDepartmentIndicatorValue_DeleteItem(int id)
        {

        }
    }
}