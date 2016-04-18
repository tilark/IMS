using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMS.Models;
using OperateExcel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IMS.Logic
{
    public class RoleActions
    {
        internal void InitialRoleName()
        {
            //从权限Excel表中读取权限名称，加入到权限表中
            var fileName = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager
               .AppSettings["RoleName"]);
            ReadFromExcel readFromExcel = new ReadFromExcel();
            //按Row获取当前行的所有数据
            int rowCount = readFromExcel.GetRowCount(fileName);
            for (int i = 2; i <= rowCount; i++)
            {
                var columnData = readFromExcel.ReadRowFromExcel((uint)i, fileName);
                //数据必须大于等1才有效
                //第1列为权限名称
                if (columnData.Count >= 1)
                {
                    using (ApplicationDbContext context = new ApplicationDbContext())
                    {
                        using (RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)))
                        {
                            var roleName = columnData.ElementAt(0);
                            if (!roleManager.RoleExists(roleName))
                            {
                                roleManager.Create(new IdentityRole(roleName));
                            }
                        }
                    }
                }
            }

        }

        public IdentityResult ResetPassword(string id, string Password)
        {
            IdentityResult result = IdentityResult.Failed("重置密码失败！");
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
                {
                    result = userManager.RemovePassword(id);
                    if (result.Succeeded)
                    {
                        result = userManager.AddPassword(id, Password);
                    }
                }
            }
            return result;
        }

        internal void CreateAdmin()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
                {
                    using (RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)))
                    {
                        //Create Role Administrator if it does not exist
                        if (!roleManager.RoleExists("Administrators"))
                        {
                            var roleResult = roleManager.Create(new IdentityRole("Administrators"));
                        }
                        //创建Administrator用户
                        string adminName = "Administrator@qq.com";
                        string password = "52166057";
                        var user = new ApplicationUser();
                        user.UserName = adminName;
                        user.Email = "Administrator@qq.com";
                        user.Name = "管理员";
                        var adminResult = userManager.Create(user, password);
                        //Add User Admin to Role Administrator
                        if (adminResult.Succeeded)
                        {
                            var result = userManager.AddToRole(user.Id, "Administrators");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Adds the user to role.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>IdentityResult.</returns>
        public IdentityResult AddUserToRole(string id, string roleName)
        {
            IdentityResult result = IdentityResult.Failed("添加用户至权限表失败！");
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
                {
                    if (!userManager.IsInRole(id, roleName))
                    {
                        result = userManager.AddToRole(id, roleName);
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// Removes the user from role.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>IdentityResult.</returns>
        public IdentityResult RemoveUserFromRole(string id, string roleName)
        {
            IdentityResult result = IdentityResult.Failed("删除用户权限失败！");
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
                {
                    if (userManager.IsInRole(id, roleName))
                    {
                        result = userManager.RemoveFromRoles(id, roleName);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Gets the user roles.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IList&lt;System.String&gt;.</returns>
        public IList<string> GetUserRoles(string id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
                {
                    return userManager.GetRoles(id);
                }
            }
        }
        /// <summary>
        /// Gets the roles dictionary.
        /// </summary>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        public Dictionary<string, string> GetRolesDic()
        {
            Dictionary<string, string> rolesDic = new Dictionary<string, string>();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                using (RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)))
                {
                    var roleList = roleManager.Roles.ToList();
                    foreach (var role in roleList)
                    {
                        rolesDic.Add(role.Id, role.Name);
                    }
                }
            }
            return rolesDic;
        }
    }
}