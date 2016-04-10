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
        public Dictionary< Int64,string> GetDepartmentDic()
        {
            Dictionary<Int64, string> listData = new Dictionary<Int64, string>();

            using (MonitorDbContext context = new MonitorDbContext())
            {
                listData.Add(-1, "-全选-");
                foreach(var query in context.Departments)
                {
                    listData.Add(query.DepartmentID, query.DepartmentName);
                }
            }
            return listData;
        }
        public Dictionary<Int64, string> GetMonitorItemDic()
        {
            Dictionary<Int64, string> listData = new Dictionary<Int64, string>();
            using (MonitorDbContext context = new MonitorDbContext())
            {
                listData.Add(-1, "-全选-");

                foreach (var query in context.MonitorItems)
                {
                    listData.Add(query.MonitorItemID, query.MonitorName);
                }
            }
            return listData;
        }
        public Dictionary<Int64, string> GetDepartmentTypeDic()
        {
            Dictionary<Int64, string> listData = new Dictionary<Int64, string>();
            using (MonitorDbContext context = new MonitorDbContext())
            {
                listData.Add(-1, "-全选-");

                foreach (var query in context.DepartmentTypes)
                {
                    listData.Add(query.DepartmentTypeID, query.TypeName);
                }
            }
            return listData;
        }
    }
}