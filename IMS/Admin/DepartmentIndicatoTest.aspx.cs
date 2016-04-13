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

namespace IMS.Admin
{
    public partial class DepartmentIndicatoTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initialGridView();
            }
        }
        public void initialGridView()
        {
            using (ImsDbContext db = new ImsDbContext())
            {
                //var query = from d in db.Departments
                //            join p in db.Indicators on d.DepartmentID equals p.DepartmentID into r
                //            select new
                //            {
                //                d.DepartmentName,
                //                Indicator = r
                //            };

                //GridView1.DataSource = query.ToList();
                //GridView1.DataBind();
                //GridView gridView2 = new GridView();
                //gridView2 = (GridView)GridView1.FindControl("GridView2");
                //if (gridView2 != null)
                //{
                //    gridView2.DataSource = query.ElementAt(1);
                //    gridView2.DataBind();
                //}
               
                            
            }
        }

        // 返回类型可以更改为 IEnumerable，但是为了支持
        // 分页和排序，必须添加以下参数:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<IMS.Models.Department> ListView1_GetData()
        {
            IQueryable<IMS.Models.Department> query = null;
            ImsDbContext context = new ImsDbContext();
            query = context.Departments;
            return query;
        }

        // 返回类型可以更改为 IEnumerable，但是为了支持
        // 分页和排序，必须添加以下参数:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<IMS.Models.Indicator> GridView3_GetData([Control] Guid? lblID)
        {
            IQueryable<IMS.Models.Indicator> query = null;
            if(lblID != null)
            {
                ImsDbContext context = new ImsDbContext();
                query = context.Indicators.Where(i => i.DepartmentID == lblID).OrderBy(i => i.Name);

            }
            return query;
        }
    }
}