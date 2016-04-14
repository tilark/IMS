namespace IMS.ImsDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequireForName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DepartmentCategoryIndicatorMaps", "IndicatorID", "dbo.Indicators");
            DropForeignKey("dbo.DepartmentIndicatorStandardValues", "IndicatorID", "dbo.Indicators");
            DropForeignKey("dbo.DepartmentIndicatorValues", "IndicatorID", "dbo.Indicators");
            DropForeignKey("dbo.Departments", "DepartmentCategoryID", "dbo.DepartmentCategories");
            DropForeignKey("dbo.DepartmentIndicatorStandardValues", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.DepartmentIndicatorValues", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.DepartmentCategoryIndicatorMaps", "DepartmentCategoryID", "dbo.DepartmentCategories");
            DropIndex("dbo.Departments", new[] { "DepartmentCategoryID" });
            DropIndex("dbo.DepartmentCategoryIndicatorMaps", new[] { "DepartmentCategoryID" });
            DropIndex("dbo.DepartmentCategoryIndicatorMaps", new[] { "IndicatorID" });
            DropIndex("dbo.DepartmentIndicatorStandardValues", new[] { "DepartmentID" });
            DropIndex("dbo.DepartmentIndicatorStandardValues", new[] { "IndicatorID" });
            DropIndex("dbo.DepartmentIndicatorValues", new[] { "DepartmentID" });
            DropIndex("dbo.DepartmentIndicatorValues", new[] { "IndicatorID" });
            AlterColumn("dbo.DataSourceSystems", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Indicators", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Departments", "DepartmentCategoryID", c => c.Guid());
            AlterColumn("dbo.Departments", "DepartmentName", c => c.String(nullable: false));
            AlterColumn("dbo.DepartmentCategories", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.DepartmentCategoryIndicatorMaps", "DepartmentCategoryID", c => c.Guid());
            AlterColumn("dbo.DepartmentCategoryIndicatorMaps", "IndicatorID", c => c.Guid());
            AlterColumn("dbo.DepartmentIndicatorStandardValues", "DepartmentID", c => c.Guid());
            AlterColumn("dbo.DepartmentIndicatorStandardValues", "IndicatorID", c => c.Guid());
            AlterColumn("dbo.DepartmentIndicatorValues", "DepartmentID", c => c.Guid());
            AlterColumn("dbo.DepartmentIndicatorValues", "IndicatorID", c => c.Guid());
            AlterColumn("dbo.DepartmentIndicatorValues", "Time", c => c.DateTime());
            CreateIndex("dbo.Departments", "DepartmentCategoryID");
            CreateIndex("dbo.DepartmentCategoryIndicatorMaps", "DepartmentCategoryID");
            CreateIndex("dbo.DepartmentCategoryIndicatorMaps", "IndicatorID");
            CreateIndex("dbo.DepartmentIndicatorStandardValues", "DepartmentID");
            CreateIndex("dbo.DepartmentIndicatorStandardValues", "IndicatorID");
            CreateIndex("dbo.DepartmentIndicatorValues", "DepartmentID");
            CreateIndex("dbo.DepartmentIndicatorValues", "IndicatorID");
            AddForeignKey("dbo.DepartmentCategoryIndicatorMaps", "IndicatorID", "dbo.Indicators", "IndicatorID");
            AddForeignKey("dbo.DepartmentIndicatorStandardValues", "IndicatorID", "dbo.Indicators", "IndicatorID");
            AddForeignKey("dbo.DepartmentIndicatorValues", "IndicatorID", "dbo.Indicators", "IndicatorID");
            AddForeignKey("dbo.Departments", "DepartmentCategoryID", "dbo.DepartmentCategories", "DepartmentCategoryID");
            AddForeignKey("dbo.DepartmentIndicatorStandardValues", "DepartmentID", "dbo.Departments", "DepartmentID");
            AddForeignKey("dbo.DepartmentIndicatorValues", "DepartmentID", "dbo.Departments", "DepartmentID");
            AddForeignKey("dbo.DepartmentCategoryIndicatorMaps", "DepartmentCategoryID", "dbo.DepartmentCategories", "DepartmentCategoryID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DepartmentCategoryIndicatorMaps", "DepartmentCategoryID", "dbo.DepartmentCategories");
            DropForeignKey("dbo.DepartmentIndicatorValues", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.DepartmentIndicatorStandardValues", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Departments", "DepartmentCategoryID", "dbo.DepartmentCategories");
            DropForeignKey("dbo.DepartmentIndicatorValues", "IndicatorID", "dbo.Indicators");
            DropForeignKey("dbo.DepartmentIndicatorStandardValues", "IndicatorID", "dbo.Indicators");
            DropForeignKey("dbo.DepartmentCategoryIndicatorMaps", "IndicatorID", "dbo.Indicators");
            DropIndex("dbo.DepartmentIndicatorValues", new[] { "IndicatorID" });
            DropIndex("dbo.DepartmentIndicatorValues", new[] { "DepartmentID" });
            DropIndex("dbo.DepartmentIndicatorStandardValues", new[] { "IndicatorID" });
            DropIndex("dbo.DepartmentIndicatorStandardValues", new[] { "DepartmentID" });
            DropIndex("dbo.DepartmentCategoryIndicatorMaps", new[] { "IndicatorID" });
            DropIndex("dbo.DepartmentCategoryIndicatorMaps", new[] { "DepartmentCategoryID" });
            DropIndex("dbo.Departments", new[] { "DepartmentCategoryID" });
            AlterColumn("dbo.DepartmentIndicatorValues", "Time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DepartmentIndicatorValues", "IndicatorID", c => c.Guid(nullable: false));
            AlterColumn("dbo.DepartmentIndicatorValues", "DepartmentID", c => c.Guid(nullable: false));
            AlterColumn("dbo.DepartmentIndicatorStandardValues", "IndicatorID", c => c.Guid(nullable: false));
            AlterColumn("dbo.DepartmentIndicatorStandardValues", "DepartmentID", c => c.Guid(nullable: false));
            AlterColumn("dbo.DepartmentCategoryIndicatorMaps", "IndicatorID", c => c.Guid(nullable: false));
            AlterColumn("dbo.DepartmentCategoryIndicatorMaps", "DepartmentCategoryID", c => c.Guid(nullable: false));
            AlterColumn("dbo.DepartmentCategories", "Name", c => c.String(maxLength: 128));
            AlterColumn("dbo.Departments", "DepartmentName", c => c.String());
            AlterColumn("dbo.Departments", "DepartmentCategoryID", c => c.Guid(nullable: false));
            AlterColumn("dbo.Indicators", "Name", c => c.String());
            AlterColumn("dbo.DataSourceSystems", "Name", c => c.String());
            CreateIndex("dbo.DepartmentIndicatorValues", "IndicatorID");
            CreateIndex("dbo.DepartmentIndicatorValues", "DepartmentID");
            CreateIndex("dbo.DepartmentIndicatorStandardValues", "IndicatorID");
            CreateIndex("dbo.DepartmentIndicatorStandardValues", "DepartmentID");
            CreateIndex("dbo.DepartmentCategoryIndicatorMaps", "IndicatorID");
            CreateIndex("dbo.DepartmentCategoryIndicatorMaps", "DepartmentCategoryID");
            CreateIndex("dbo.Departments", "DepartmentCategoryID");
            AddForeignKey("dbo.DepartmentCategoryIndicatorMaps", "DepartmentCategoryID", "dbo.DepartmentCategories", "DepartmentCategoryID", cascadeDelete: true);
            AddForeignKey("dbo.DepartmentIndicatorValues", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: true);
            AddForeignKey("dbo.DepartmentIndicatorStandardValues", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: true);
            AddForeignKey("dbo.Departments", "DepartmentCategoryID", "dbo.DepartmentCategories", "DepartmentCategoryID", cascadeDelete: true);
            AddForeignKey("dbo.DepartmentIndicatorValues", "IndicatorID", "dbo.Indicators", "IndicatorID", cascadeDelete: true);
            AddForeignKey("dbo.DepartmentIndicatorStandardValues", "IndicatorID", "dbo.Indicators", "IndicatorID", cascadeDelete: true);
            AddForeignKey("dbo.DepartmentCategoryIndicatorMaps", "IndicatorID", "dbo.Indicators", "IndicatorID", cascadeDelete: true);
        }
    }
}
