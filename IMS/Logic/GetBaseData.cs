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
        public Dictionary<string,string> GetDepartmentDic()
        {
            Dictionary<string, string> listData = new Dictionary<string, string>();

            using (ImsDbContext context = new ImsDbContext())
            {
                listData.Add(" ", "-全选-");
                foreach(var query in context.Departments)
                {
                    listData.Add(query.DepartmentID.ToString(), query.DepartmentName);
                }
            }
            return listData;
        }
        public Dictionary<string, string> GetMonitorItemDic()
        {
            Dictionary<string, string> listData = new Dictionary<string, string>();
            using (ImsDbContext context = new ImsDbContext())
            {
                listData.Add(" ", "-全选-");

                foreach (var query in context.Indicators)
                {
                    listData.Add(query.IndicatorID.ToString(), query.Name);
                }
            }
            return listData;
        }
        public Dictionary<string, string> GetDepartmentTypeDic()
        {
            Dictionary<string, string> listData = new Dictionary<string, string>();
            using (ImsDbContext context = new ImsDbContext())
            {
                listData.Add(" ", "-全选-");

                foreach (var query in context.DepartmentCategories)
                {
                    listData.Add(query.DepartmentCategoryID.ToString(), query.Name);
                }
            }
            return listData;
        }
    }
}