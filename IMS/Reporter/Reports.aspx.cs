using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using IMS.Logic;
using IMS.Models;
using System.ComponentModel;
using System.Data;
using OperateExcel;
namespace IMS.Reporter
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlDepartmentCategoryBind();
            }
        }

        protected void ddlDepartmentCategoryBind()
        {
            ddlDepartmentCategory.DataSource = new GetBaseData().GetDepartmentCategoryDic();
            ddlDepartmentCategory.DataTextField = "Value";
            ddlDepartmentCategory.DataValueField = "Key";
            ddlDepartmentCategory.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //将ddlDepartmentCategory的ID与时间传入到列表中，获取DataTable，先绑定到GridView查看一下内容，之后再导出到Excel表中
            //保存到一个ReportFileTemp再下载
            DateTime startTime = DateTime.Parse(txtTimeFrom.Text);
            DateTime endTime = DateTime.Parse(txtTimeTo.Text);
            Guid departmentCategoryID = Guid.Parse(ddlDepartmentCategory.SelectedValue);
            if (DateTime.Compare(endTime, startTime) < 0)
            {
                Message.Text = "截止时间不能小于开始时间！";
            }
            using (ImsDbContext context = new ImsDbContext())
            {
                var dt = Logic.Reports.GetGenericReport(context, departmentCategoryID, startTime, endTime);
                //先分析表结构
                //添加固定的headerTitle
                List<string> headTitleList = new List<string>();
                //将列名写到headColumnNameList中
                List<string> headColumnList = new List<string>();
                foreach (DataColumn columnName in dt.Columns)
                {
                    headColumnList.Add(columnName.ColumnName);
                }

                List<List<string>> itemValueListList = new List<List<string>>();
                foreach (DataRow row in dt.Rows)
                {
                    List<string> itemValueList = new List<string>();

                    foreach (DataColumn column in dt.Columns)
                    {
                        itemValueList.Add(row[column].ToString());
                    }
                    itemValueListList.Add(itemValueList);
                }
                //写入到Excel表中

                string titleName = ddlDepartmentCategory.SelectedItem.Text + txtTimeFrom.Text + "至" + txtTimeTo.Text + "汇总报表";
                headTitleList.Add(titleName);
                string fileName = "/ReportFileTemp/" +  ddlDepartmentCategory.SelectedItem.Text + DateTime.UtcNow.ToBinary().ToString() + ".xlsx";
                string filePath = Server.MapPath(fileName);
                new WriteToExcel().WriteItemValueToExcel(filePath, headTitleList, headColumnList, itemValueListList);
                //将页面转至文件名处，提供下载
                Response.Redirect(fileName);
            }
        }


    }
}