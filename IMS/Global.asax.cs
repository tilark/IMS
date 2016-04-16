using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using IMS.Logic;
namespace IMS
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitialItemData initialItemData = new InitialItemData();
            initialItemData.InitialDepartmentCategory();
            initialItemData.InitialDataSourceSystem();
            initialItemData.InitialDepartment();
            initialItemData.InitialIndicator();
            initialItemData.InitialDepartmentCategoryIndicatorMap();
            initialItemData.InitialIndicatorAlgorithm();

            RoleActions roleAction = new RoleActions();
            roleAction.InitialRoleName();
            roleAction.CreateAdmin();
        }
    }
}