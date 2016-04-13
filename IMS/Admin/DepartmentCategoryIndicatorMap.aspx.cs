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
namespace IMS.Admin
{
    public partial class DepartmentCategoryIndicatorMap : System.Web.UI.Page
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
        public IQueryable<IMS.Models.DepartmentCategoryIndicatorMap> gvCategoryIndicator_GetData()
        {
            IQueryable <IMS.Models.DepartmentCategoryIndicatorMap > query = null;
            ImsDbContext context = new ImsDbContext();
            query = context.DepartmentCategoryIndicatorMaps.Include(i => i.DepartmentCategory).Include(i => i.Indicator).OrderBy(o => o.DepartmentCategory.Name);
            return query;
        }
    }
}