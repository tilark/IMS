using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMS.Models;
namespace IMS.Admin
{
    public partial class ListUsers : System.Web.UI.Page
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
        public IQueryable<IMS.Models.ApplicationUser> lvUser_GetData()
        {
            IQueryable<IMS.Models.ApplicationUser> query = null;
            ApplicationDbContext context = new ApplicationDbContext();
            query = context.Users.OrderBy(u => u.UserName).ThenBy(u => u.Department);
            return query;
        }

        // id 参数名应该与控件上设置的 DataKeyNames 值匹配
        public void lvUser_DeleteItem(int id)
        {

        }

        // id 参数名应该与控件上设置的 DataKeyNames 值匹配
        public void lvUser_UpdateItem(string id)
        {
            IMS.Models.ApplicationUser item = null;
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
}