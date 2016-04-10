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
    public partial class MonitorList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initialGridView();
            }
        }
        private void initialGridView()
        {
            MonitorDbContext context = new MonitorDbContext();

            var query = from d in context.Departments
                        join m in context.DepartmentMonitors on d.DepartmentID equals m.DepartmentID into mdlist
                        from md in mdlist
                        select new { d.DepartmentName, md.DepartmentType.TypeName, md.MonitorItem.MonitorName, md.Value, md.Time };
            //var queryTest = query.FirstOrDefault();
            //foreach(var test in query)
            //{

            //}
            //var query = from d in context.Departments
            //            orderby d.DepartmentName
            //            join m in 
            //foreach(var item in context.Departments.)
            gvTest1.DataSource = query.ToList();
            gvTest1.DataBind();

        }
        // 返回类型可以更改为 IEnumerable，但是为了支持
        // 分页和排序，必须添加以下参数:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<IMS.Models.Department> lvDepartment_GetData()
        {
            IQueryable<IMS.Models.Department> query = null;
            MonitorDbContext context = new MonitorDbContext();
            query = context.Departments.OrderBy(o => o.DepartmentName);
            return query;
        }

        // 返回类型可以更改为 IEnumerable，但是为了支持
        // 分页和排序，必须添加以下参数:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<IMS.Models.DepartmentMonitor> gvDepartmentMonitor_GetData([Control] Int64? lblDepartmentID)
        {
            IQueryable<IMS.Models.DepartmentMonitor> query = null;
            MonitorDbContext context = new MonitorDbContext();
            if (lblDepartmentID != null)
            {
                query = context.DepartmentMonitors.Include(i => i.Department).Include(i => i.DepartmentType).Include(i => i.MonitorItem)
                    .OrderBy(o => o.Department.DepartmentName).Where(d => d.DepartmentID == lblDepartmentID);
            }
            return query;
        }
    }
}