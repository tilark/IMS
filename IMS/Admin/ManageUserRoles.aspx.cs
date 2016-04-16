using IMS.Logic;
using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IMS.Admin
{
    public partial class ManageUserRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lboxRolesBind();
            }
        }

        // id 参数应与控件上设置的 DataKeyNames 值匹配
        // 或用值提供程序特性装饰，例如 [QueryString]int id
        public IMS.Models.ApplicationUser fvUserRoles_GetItem([QueryString] Guid id)
        {
            ApplicationUser queryUser = new ApplicationUser(); ;
            ApplicationDbContext context = new ApplicationDbContext();
            queryUser = context.Users.Find(id.ToString());
            lblUserId.Text = id.ToString();
            lboxUserRolesBind();
            return queryUser;
        }


        protected void btnAddRoles_Click(object sender, EventArgs e)
        {
            var userId = lblUserId.Text;
            if (String.IsNullOrEmpty(userId))
            {
                return;
            }
            //选中的项，先查找是否已存在于MapRole的TrueRole中，如果不存在，添加到List中
            foreach (ListItem selectedRoles in lboxRoles.Items)
            {
                if (selectedRoles.Selected == true)
                {
                    List<string> targetList = new List<string>();
                    foreach (ListItem queryList in lboxUserRoles.Items)
                    {
                        targetList.Add(queryList.Text);
                    }
                    if (!targetList.Contains(selectedRoles.Text))
                    {
                        //将权限添加到角色中

                        var result = new RoleActions().AddUserToRole(userId, selectedRoles.Text);
                        if (result.Succeeded)
                        {
                            Message.Text = "添加成功!";
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
            lboxUserRolesBind();
        }

        protected void btnDeleteRoles_Click(object sender, EventArgs e)
        {
            if (lboxUserRoles.SelectedItem != null)
            {
                var userId = lblUserId.Text;
                if (!String.IsNullOrEmpty(userId))
                {
                    var result = new RoleActions().RemoveUserFromRole(userId, lboxUserRoles.SelectedItem.Text);
                    if (result.Succeeded)
                    {
                        Message.Text = "删除成功!";
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
                lboxUserRolesBind();

            }
        }

        private void lboxUserRolesBind()
        {
            var userId = lblUserId.Text;
            if (!String.IsNullOrEmpty(userId))
            {
                lboxUserRoles.DataSource = new RoleActions().GetUserRoles(userId);
                lboxUserRoles.DataBind();
            }
        }

        protected void lboxRolesBind()
        {
            //Role ListBox Bind
            lboxRoles.DataSource = new RoleActions().GetRolesDic();
            lboxRoles.DataValueField = "Key";
            lboxRoles.DataTextField = "Value";
            lboxRoles.DataBind();
            lboxUserRolesBind();
        }
    }
}