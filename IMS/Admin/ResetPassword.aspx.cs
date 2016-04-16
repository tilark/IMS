using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using IMS.Logic;
using IMS.Models;
using System.Data.Entity;
using System.Web.ModelBinding;
namespace IMS.Admin
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // id 参数应与控件上设置的 DataKeyNames 值匹配
        // 或用值提供程序特性装饰，例如 [QueryString]int id
        public IMS.Models.ApplicationUser fvUser_GetItem([QueryString] Guid id)
        {
            ApplicationUser queryUser = new ApplicationUser(); ;
            ApplicationDbContext context = new ApplicationDbContext();
            queryUser = context.Users.Find(id.ToString());
            return queryUser;
        }

        // id 参数名应该与控件上设置的 DataKeyNames 值匹配
        public void fvUser_UpdateItem(Guid id)
        {
            TextBox txtPassword = new TextBox();
            txtPassword = (TextBox)fvUser.FindControl("Password");
            if (txtPassword == null)
            {
                return;
            }
            IdentityResult result = new RoleActions().ResetPassword(id.ToString(), txtPassword.Text);
            if (result.Succeeded)
            {
                Message.Text = "重置密码成功！";
            }
            else
            {
                Message.Text = String.Empty;
                foreach (var errorMessage in result.Errors)
                {
                    Message.Text += errorMessage;
                }

            }
        }
    }
}