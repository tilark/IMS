using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMS.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace IMS.Logic
{
    public class GetBaseData
    {
        public Dictionary<Guid, string> GetDepartmentDic()
        {
            Dictionary<Guid, string> listData = new Dictionary<Guid, string>();

            using (ImsDbContext context = new ImsDbContext())
            {
                //listData.Add(" ", "-全选-");
                foreach (var query in context.Departments)
                {
                    listData.Add(query.DepartmentID, query.DepartmentName);
                }
            }
            return listData;
        }

        public Dictionary<Guid, string> GetDepartmentIndicatorDic()
        {
            Dictionary<Guid, string> listData = new Dictionary<Guid, string>();
            using (ImsDbContext context = new ImsDbContext())
            {
                var indicatorItems = context.Indicators.OrderBy(i => i.Name).ToList();
                foreach(var indicator in indicatorItems)
                {
                    var department = indicator.Department;
                    if (department != null)
                    {
                        if (!listData.ContainsKey(department.DepartmentID))
                        {
                            listData.Add(department.DepartmentID, department.DepartmentName);

                        }
                    }
                }
            }
            return listData;

        }
       

        public Dictionary<Guid, string> GetDataSourceSystemDic()
        {
            Dictionary<Guid, string> listData = new Dictionary<Guid, string>();
            using (ImsDbContext context = new ImsDbContext())
            {
                //listData.Add(" ", "-全选-");
                foreach (var query in context.DataSourceSystems)
                {
                    listData.Add(query.ID, query.Name);
                }
            }
            return listData;

        }
    }
}