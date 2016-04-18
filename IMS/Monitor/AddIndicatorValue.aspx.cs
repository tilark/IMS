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
    public partial class AddIndicatorValue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Message.Text = "Load！";
            if (!IsPostBack)
            {
                dlDepatmentBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //添加时需对该用户进行权限检测，如果不属于该科室成员，不能够新增项目数据

            //将找到该项目的来源部门。
            if (dlDepartment == null || txtDate == null || String.IsNullOrEmpty(txtDate.Text))
            {
                return;
            }
            try
            {
                var departmentID = Guid.Parse(dlDepartment.SelectedValue);
                var addTime = DateTime.Parse(txtDate.Text);
                using (ImsDbContext context = new ImsDbContext())
                {
                    //1 由项目来源部门找出管属项目

                    var indicatorItems = context.Indicators.Where(i => i.DepartmentID != null && i.DepartmentID == departmentID).OrderBy(i => i.Name).ToList();
                    foreach (var indicator in indicatorItems)
                    {
                        //2 管属项目找到对应的科室类别项目映射表，找到对应科室类别
                        var departmentCategoryIndicators = indicator.DepartmentCategoryIndicatorMaps.ToList();
                        //列举出对应的科室类别，再由此找到每个科室
                        foreach (var categoryIndicator in departmentCategoryIndicators)
                        {
                            //3 通过科室类别找到管辖的科室，逐一与项目组合，添加到科室项目值表
                            var departmentCategory = categoryIndicator.DepartmentCategory;
                            if (departmentCategory == null)
                            {
                                continue;
                            }
                            var departments = departmentCategory.Departments;
                            //列出该科室负责的填报项目，再逐步添加到值表中。

                            foreach (var department in departments)
                            {
                                //需先查重，如果已经该项目已存在于数据库，不添加
                                //检查下一项
                                var query = context.DepartmentIndicatorValues.Where(d => d.DepartmentID == department.DepartmentID && d.IndicatorID == indicator.IndicatorID
                                && d.Time.Year == addTime.Year && d.Time.Month == addTime.Month).FirstOrDefault();
                                if (query != null)
                                {
                                    continue;
                                }
                                else
                                {
                                    //不存在该项目，需添加到数据库
                                    IMS.Models.DepartmentIndicatorValue item = new IMS.Models.DepartmentIndicatorValue();
                                    item.DepartmentID = department.DepartmentID;
                                    item.IndicatorID = indicator.IndicatorID;
                                    item.Time = addTime;
                                    item.ID = System.Guid.NewGuid();
                                    context.DepartmentIndicatorValues.Add(item);
                                    context.SaveChanges();
                                }

                            }
                        }
                    }
                }
                //需更新该页面 通过改变Message的值，会调用lvIndicatorValue_GetData
                //添加时间过长时，通过UpdateProcessing显示模态提示框，并能够显示进度条，添加成功后，关闭模态框
                Message.Text = "添加项目成功！";
            }
            catch (Exception ex)
            {
                Message.Text = ex.Message;
            }



        }
        private void dlDepatmentBind()
        {
            dlDepartment.DataSource = new GetBaseData().GetDepartmentIndicatorDic();
            dlDepartment.DataTextField = "Value";
            dlDepartment.DataValueField = "Key";
            dlDepartment.DataBind();
        }


        // 返回类型可以更改为 IEnumerable，但是为了支持
        // 分页和排序，必须添加以下参数:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<IMS.Models.DepartmentIndicatorValue> lvIndicatorValue_GetData([Control] Guid? dlDepartment, [Control] DateTime? txtDate, [Control] string Message)
        {
            IQueryable<IMS.Models.DepartmentIndicatorValue> query = null;
            if (dlDepartment == null || txtDate == null)
            {
                return query;
            }

            ImsDbContext context = new ImsDbContext();
            //找到该项目管辖的项目
            query = context.DepartmentIndicatorValues.Include(i => i.Indicator).Include(i => i.Department)
                .Where(d => d.Indicator.DepartmentID == dlDepartment && d.Time == txtDate).OrderBy(i => i.Department.DepartmentName).ThenBy(t => t.Indicator.Name);

            return query;
        }

        // id 参数名应该与控件上设置的 DataKeyNames 值匹配
        public void lvIndicatorValue_UpdateItem(Guid id)
        {
            TextBox txtValue = new TextBox();
            txtValue = (TextBox)lvIndicatorValue.EditItem.FindControl("txtValue");
            using (ImsDbContext context = new ImsDbContext())
            {
                IMS.Models.DepartmentIndicatorValue item = null;
                // 在此加载该项，例如 item = MyDataLayer.Find(id);
                item = context.DepartmentIndicatorValues.Find(id);
                if (item == null)
                {
                    // 未找到该项
                    ModelState.AddModelError("", String.Format("未找到 id 为 {0} 的项", id));
                    return;
                }
                TryUpdateModel(item);
                if (ModelState.IsValid)
                {
                    decimal value;
                    // 在此保存更改，例如 MyDataLayer.SaveChanges();
                    if (Decimal.TryParse(txtValue.Text, out value))
                    {
                        item.Value = value;
                    }
                    else
                    {
                        item.Value = null;
                    }

                    //database win
                    bool saveFailed;
                    do
                    {
                        saveFailed = false;
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (DbUpdateConcurrencyException ex)
                        {
                            saveFailed = true;
                            // Update the values of the entity that failed to save from the store 
                            ex.Entries.Single().Reload();
                        }
                    } while (saveFailed);
                }
            }

        }

        // 返回类型可以更改为 IEnumerable，但是为了支持
        // 分页和排序，必须添加以下参数:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<IMS.Models.Indicator> lvIndicatorItem_GetData([Control] Guid? dlDepartment, [Control] DateTime? txtDate, [Control] string Message)
        {
            IQueryable<IMS.Models.Indicator> query = null;

            if (dlDepartment != null)
            {
                ImsDbContext context = new ImsDbContext();

                //需找到该Department
                //var department = context.Departments.Find(dlDepartment);
                query = context.Indicators.Where(i => i.DepartmentID != null && i.DepartmentID == dlDepartment).OrderBy(i => i.Name);

            }
            return query;
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Message.Text = "查询成功！";
        }
    }
}