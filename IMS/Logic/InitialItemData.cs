using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OperateExcel;
namespace IMS.Logic
{
    public class InitialItemData
    {
        private string MonitorItemSource = null;
        private string DepartmentSource = null;
        public InitialItemData()
        {
            this.MonitorItemSource = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["MonitorItemSource"]);
            this.DepartmentSource = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["DepartmentSource"]);
        }
        public void InitialDepartment()
        {
            
        }
    }
}