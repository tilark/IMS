// ***********************************************************************
// Assembly         : IMS
// Author           : 刘林
// Created          : 04-09-2016
//
// Last Modified By : 刘林
// Last Modified On : 04-09-2016
// ***********************************************************************
// <copyright file="IndicatorIndicatorModels.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary>Indicator Indicator Models</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
/// <summary>
/// The Models namespace.
/// </summary>
namespace IMS.Models
{
    /// <summary>
    /// ImsDbContext.

    /// </summary>
    public class ImsDbContext : DbContext
    {
        /// <summary>
        /// 指定数据库 <see cref="IndicatorContext" /> class.
        /// </summary>
        public ImsDbContext() : base("IMSConnection") { }
        /// <summary>
        /// Gets or sets the department monitors.
        /// </summary>
        /// <value>The department monitors.</value>
        public DbSet<DepartmentIndicatorValue> DepartmentIndicatorValues { get; set; }
        /// <summary>
        /// Gets or sets the departments.
        /// </summary>
        /// <value>The departments.</value>
        public DbSet<Department> Departments { get; set; }
        /// <summary>
        /// Gets or sets the monitor items.
        /// </summary>
        /// <value>The monitor items.</value>
        public DbSet<Indicator> Indicators { get; set; }
        public DbSet<DepartmentCategory> DepartmentCategories { get; set; }
        public DbSet<DepartmentCategoryIndicatorMap> DepartmentCategoryIndicatorMaps { get; set; }
        public DbSet<DepartmentIndicatorStandardValue> DepartmentIndicatorStandardValues { get; set; }

    }
    /// <summary>
    /// DepartmentIndicatorValue.科室项目的多对多的值表
    /// </summary>
    public class DepartmentIndicatorValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentIndicatorValue"/> class.
        /// </summary>
        public DepartmentIndicatorValue()
        {
            this.Time = DateTime.Now;
        }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key]
        [Display(Name = "科室项目编号")]
        [ScaffoldColumn(false)]
        public Guid ID { get; set; }
        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>The department identifier.</value>
        public Guid DepartmentID { get; set; }
        /// <summary>
        /// Gets or sets the monitor item identifier.
        /// </summary>
        /// <value>The monitor item identifier.</value>
        public Guid IndicatorID { get; set; }
        /// <summary>
        /// Gets or sets the Indicator value.
        /// </summary>
        /// <value>The value.</value>
        [Display(Name = "项目值")]
        [MaxLength(30)]

        public string Value { get; set; }
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>The time.</value>
        [Display(Name = "监测时间")]

        public DateTime Time { get; set; }
        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>The department.</value>
        public virtual Department Department { get; set; }
        /// <summary>
        /// Gets or sets the monitor item.
        /// </summary>
        /// <value>The monitor item.</value>
        public virtual Indicator Indicator { get; set; }
        [Timestamp]
        public Byte[] TimeStamp { get; set; }

    }
    /// <summary>
    /// Department.科室基础表
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>The department identifier.</value>
        [Key]
        [Display(Name = "科室编号")]
        [ScaffoldColumn(false)]
        public Guid DepartmentID { get; set; }
        public Guid DepartmentCategoryID { get; set; }
        /// <summary>
        /// Gets or sets the name of the department.
        /// </summary>
        /// <value>The name of the department.</value>
        [Display(Name = "科室名称")]

        public string DepartmentName { get; set; }
        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>The remark.</value>
        public string Remarks { get; set; }
        /// <summary>
        /// Gets or sets the department monitor.
        /// </summary>
        /// <value>The department monitor.</value>
        public virtual ICollection<DepartmentIndicatorValue> DepartmentIndicatorValue { get; set; }
        public virtual ICollection<DepartmentIndicatorStandardValue> DepartmentIndicatorStandardValue { get; set; }
        public virtual DepartmentCategory DepartmentCategory { get; set; }

        [Timestamp]
        public Byte[] TimeStamp { get; set; }
    }
    /// <summary>
    /// Indicator.项目基础表
    /// </summary>
    public class Indicator
    {
        [Key]
        [Display(Name = "项目编号")]
        [ScaffoldColumn(false)]
        public Guid IndicatorID { get; set; }
        [Display(Name = "项目名称")]
        public string Name { get; set; }
        [Display(Name = "单位")]
        public string Unit { get; set; }

        [Display(Name = "项目备注")]
        public string Remarks { get; set; }
        public virtual ICollection<DepartmentIndicatorValue> DepartmentIndicatorValue { get; set; }
        public virtual ICollection<DepartmentIndicatorStandardValue> DepartmentIndicatorStandardValue { get; set; }
        public virtual ICollection<DepartmentCategoryIndicatorMap> DepartmentCategoryIndicatorMap { get; set; }

        [Timestamp]
        public Byte[] TimeStamp { get; set; }

    }
    /// <summary>
    /// Department Category.科室类别表
    /// </summary>
    /// <remarks>“非手术”、“手术”、“特殊科室”</remarks>

    public class DepartmentCategory
    {
        [Key]
        [Display(Name = "类型编号")]
        [ScaffoldColumn(false)]
        public Guid DepartmentCategoryID { get; set; }

        [Display(Name = "类型名称")]
        [MaxLength(128)]

        public string Name { get; set; }
        [Display(Name = "备注")]

        public string Remarks { get; set; }
        public virtual ICollection<Department> Department { get; set; }
        public virtual ICollection<DepartmentCategoryIndicatorMap> DepartmentCategoryIndicatorMap { get; set; }

        [Timestamp]
        public Byte[] TimeStamp { get; set; }
    }
    /// <summary>
    /// DepartmentCategoryIndicatorMap.科室类别与项目映射表
    /// </summary>
    public class DepartmentCategoryIndicatorMap
    {
        [Key]
        public Guid ID { get; set; }
        public Guid DepartmentCategoryID { get; set; }
        public Guid IndicatorID { get; set; }
        public virtual DepartmentCategory DepartmentCategory { get; set; }
        public virtual Indicator Indicator { get; set; }

        [Timestamp]
        public Byte[] TimeStamp { get; set; }
    }
    /// <summary>
    /// DepartmentIndicatorStandardValue.科室项目的标准值表
    /// </summary>
    public class DepartmentIndicatorStandardValue
    {
        [Key]
        public Guid ID { get; set; }
        public Guid DepartmentID { get; set; }
        public Guid IndicatorID { get; set; }
        public virtual Department Department { get; set; }
        public virtual Indicator Indicator { get; set; }

        [Display(Name = "比较方式")]
        [MaxLength(30)]
        public string CompareMethod { get; set; }
        [Display(Name = "标准值")]
        [MaxLength(100)]
        public string StandardValue { get; set; }
        [Timestamp]
        public Byte[] TimeStamp { get; set; }

    }
}