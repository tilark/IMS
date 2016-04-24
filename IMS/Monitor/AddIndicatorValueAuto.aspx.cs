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
//using ImsAutoLib;
namespace IMS.Monitor
{
    public partial class AddIndicatorValueAuto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dlSourceSystemBind();
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //添加时需对该用户进行权限检测，如果不属于该科室成员，不能够新增项目数据

            //将找到该项目的来源部门。
            if (dlSourceSystem == null || txtDate == null || String.IsNullOrEmpty(txtDate.Text))
            {
                return;
            }
            try
            {
                //根据下拉列表的选项，选择不同的操作方式，如果是来源于病案管理系统，为方法1，如果来源于计算值，为方法2
                var dlSourceSystemId = Guid.Parse(dlSourceSystem.SelectedValue);
                var sourceSystemName = dlSourceSystem.SelectedItem.Text;
                var addTime = DateTime.Parse(txtDate.Text);

                if (sourceSystemName == "病案管理系统")
                {
                    AddDepartmentIndicatorValueByMRMS(dlSourceSystemId, addTime);
                }
                else if (sourceSystemName == "计算")
                {
                    AddDepartmentIndicatorValueByCalculation(dlSourceSystemId, addTime);
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

        private void AddDepartmentIndicatorValueByCalculation(Guid dlSourceSystemId, DateTime addTime)
        {
            using (ImsDbContext context = new ImsDbContext())
            {
                //1 由项目来源部门找出管属项目

                var indicatorItems = context.Indicators.Where(i => i.DataSourceSystemID != null && i.DataSourceSystemID == dlSourceSystemId).OrderBy(i => i.Name).ToList();
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
                        //var departments = departmentCategory.Departments;
                        //列出该科室负责的填报项目，再逐步添加到值表中。

                        foreach (var department in departmentCategory.Departments)
                        {
                            //需先查重，如果已经该项目已存在于数据库，不添加
                            //检查下一项
                            var query = context.DepartmentIndicatorValues.Where(d => d.DepartmentID == department.DepartmentID && d.IndicatorID == indicator.IndicatorID
                            && d.Time.Year == addTime.Year && d.Time.Month == addTime.Month).FirstOrDefault();
                            if (query != null)
                            {
                                //需计算出该结果值
                                //相当于一个Update
                                IndicatorValue indicatorValue = new IndicatorValue();
                                var valueReturned = indicatorValue.GetDepartmentIndicatorValueByCalculate(query.DepartmentID, query.IndicatorID, addTime);
                                //根据项目值单位，如果是“百分比”，需乘以100
                                if (indicator.Unit == "百分比")
                                {
                                    valueReturned *= 100;
                                }
                                query.Value = valueReturned;
                                
                                #region Client win context.SaveChanges();
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

                                        // Update original values from the database 
                                        var entry = ex.Entries.Single();
                                        entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                                    }

                                } while (saveFailed);
                                #endregion
                            }
                            else
                            {
                                //不存在该项目，需添加到数据库
                                IMS.Models.DepartmentIndicatorValue item = new IMS.Models.DepartmentIndicatorValue();
                                item.DepartmentID = department.DepartmentID;
                                item.IndicatorID = indicator.IndicatorID;
                                item.Time = addTime;
                                item.ID = System.Guid.NewGuid();
                                //从计算获取该值
                                IndicatorValue indicatorValue = new IndicatorValue();
                                var valueReturned = indicatorValue.GetDepartmentIndicatorValueByCalculate(item.DepartmentID, item.IndicatorID, addTime);
                                //根据项目值单位，如果是“百分比”，需乘以100
                                if (indicator.Unit == "百分比")
                                {
                                    valueReturned *= 100;
                                }
                                item.Value = valueReturned;
                                context.DepartmentIndicatorValues.Add(item);
                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
        }

        private void AddDepartmentIndicatorValueByMRMS(Guid dlSourceSystemId, DateTime addTime)
        {
            using (ImsDbContext context = new ImsDbContext())
            {
                //1 由项目来源部门找出管属项目

                var indicatorItems = context.Indicators.Where(i => i.DataSourceSystemID != null && i.DataSourceSystemID == dlSourceSystemId).OrderBy(i => i.Name).ToList();
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
                        //var departments = departmentCategory.Departments;
                        //列出该科室负责的填报项目，再逐步添加到值表中。

                        foreach (var department in departmentCategory.Departments)
                        {
                            //从病案管理系统中获取项目值
                            decimal valueReturned = Decimal.Zero;
                            try
                            {
                                var bagl = new ImsAutoLib.Bagl.Bagl("BaglConnection");
                                valueReturned = bagl.GetIndicatorValue(department.DepartmentID, indicator.IndicatorID, addTime);
                            }
                            catch (Exception ex)
                            {
                                ModelState.AddModelError("", String.Format("无法连接病案室管理系统。<br/>详情：{0}", ex.Message));
                                valueReturned = Decimal.Zero;
                            }
                            //需先查重，如果已经该项目已存在于数据库，不添加
                            //检查下一项
                            var query = context.DepartmentIndicatorValues.Where(d => d.DepartmentID == department.DepartmentID && d.IndicatorID == indicator.IndicatorID
                            && d.Time.Year == addTime.Year && d.Time.Month == addTime.Month).FirstOrDefault();
                            if (query != null)
                            {
                                //更改项目的计算值。
                                //从病案管理系统中获取值
                                
                                query.Value = valueReturned;
                                #region  Client win context.SaveChanges();
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

                                        // Update original values from the database 
                                        var entry = ex.Entries.Single();
                                        entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                                    }

                                } while (saveFailed);
                                #endregion
                            }
                            else
                            {
                                //不存在该项目，需添加到数据库
                                IMS.Models.DepartmentIndicatorValue item = new IMS.Models.DepartmentIndicatorValue();
                                item.DepartmentID = department.DepartmentID;
                                item.IndicatorID = indicator.IndicatorID;
                                item.Time = addTime;
                                item.ID = System.Guid.NewGuid();
                                item.Value = valueReturned;
                                context.DepartmentIndicatorValues.Add(item);
                                context.SaveChanges();
                            }
                        }
                    }
                }


            }

        }
        private void dlSourceSystemBind()
        {
            dlSourceSystem.DataSource = new GetBaseData().GetDataSourceSystemDic();
            dlSourceSystem.DataTextField = "Value";
            dlSourceSystem.DataValueField = "Key";
            dlSourceSystem.DataBind();
        }


        // 返回类型可以更改为 IEnumerable，但是为了支持
        // 分页和排序，必须添加以下参数:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<IMS.Models.DepartmentIndicatorValue> lvIndicatorValue_GetData([Control] Guid? dlSourceSystem, [Control] DateTime? txtDate, [Control] string Message)
        {
            IQueryable<IMS.Models.DepartmentIndicatorValue> query = null;
            if (dlSourceSystem == null || txtDate == null)
            {
                return query;
            }

            ImsDbContext context = new ImsDbContext();
            //找到该来源系统管辖的项目
            query = context.DepartmentIndicatorValues.Include(i => i.Indicator).Include(i => i.Department)
                .Where(d => d.Indicator.DataSourceSystemID == dlSourceSystem && d.Time == txtDate)
                .OrderBy(i => i.Department.DepartmentName).ThenBy(i => i.Indicator.Name);

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
        public IQueryable<IMS.Models.Indicator> lvIndicatorItem_GetData([Control] Guid? dlSourceSystem, [Control] DateTime? txtDate)
        {
            IQueryable<IMS.Models.Indicator> query = null;

            if (dlSourceSystem != null)
            {
                ImsDbContext context = new ImsDbContext();

                //需找到该Department
                //var department = context.Departments.Find(dlSourceSystem);
                query = context.Indicators.Where(i => i.DataSourceSystemID != null && i.DataSourceSystemID == dlSourceSystem).OrderBy(i => i.Name);

            }
            return query;
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Message.Text = "查询成功！";
        }
    }
}