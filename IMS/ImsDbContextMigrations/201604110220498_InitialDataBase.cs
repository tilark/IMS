namespace IMS.ImsDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DepartmentCategories",
                c => new
                    {
                        DepartmentCategoryID = c.Guid(nullable: false),
                        DepartmentID = c.Guid(nullable: false),
                        Name = c.String(maxLength: 128),
                        Remarks = c.String(),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.DepartmentCategoryID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Guid(nullable: false),
                        DepartmentCategoryID = c.Guid(nullable: false),
                        DepartmentName = c.String(),
                        Remarks = c.String(),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.DepartmentCategories", t => t.DepartmentCategoryID, cascadeDelete: true)
                .Index(t => t.DepartmentCategoryID);
            
            CreateTable(
                "dbo.DepartmentIndicatorStandardValues",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DepartmentID = c.Guid(nullable: false),
                        IndicatorID = c.Guid(nullable: false),
                        CompareMethod = c.String(maxLength: 30),
                        StandardValue = c.String(maxLength: 100),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.Indicators", t => t.IndicatorID, cascadeDelete: true)
                .Index(t => t.DepartmentID)
                .Index(t => t.IndicatorID);
            
            CreateTable(
                "dbo.Indicators",
                c => new
                    {
                        IndicatorID = c.Guid(nullable: false),
                        Name = c.String(),
                        Remarks = c.String(),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.IndicatorID);
            
            CreateTable(
                "dbo.DepartmentCategoryIndicatorMaps",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DepartmentCategoryID = c.Guid(nullable: false),
                        IndicatorID = c.Guid(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DepartmentCategories", t => t.DepartmentCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Indicators", t => t.IndicatorID, cascadeDelete: true)
                .Index(t => t.DepartmentCategoryID)
                .Index(t => t.IndicatorID);
            
            CreateTable(
                "dbo.DepartmentIndicatorValues",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DepartmentID = c.Guid(nullable: false),
                        IndicatorID = c.Guid(nullable: false),
                        Value = c.String(maxLength: 30),
                        Time = c.DateTime(nullable: false),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.Indicators", t => t.IndicatorID, cascadeDelete: true)
                .Index(t => t.DepartmentID)
                .Index(t => t.IndicatorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DepartmentIndicatorValues", "IndicatorID", "dbo.Indicators");
            DropForeignKey("dbo.DepartmentIndicatorValues", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.DepartmentIndicatorStandardValues", "IndicatorID", "dbo.Indicators");
            DropForeignKey("dbo.DepartmentCategoryIndicatorMaps", "IndicatorID", "dbo.Indicators");
            DropForeignKey("dbo.DepartmentCategoryIndicatorMaps", "DepartmentCategoryID", "dbo.DepartmentCategories");
            DropForeignKey("dbo.DepartmentIndicatorStandardValues", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Departments", "DepartmentCategoryID", "dbo.DepartmentCategories");
            DropIndex("dbo.DepartmentIndicatorValues", new[] { "IndicatorID" });
            DropIndex("dbo.DepartmentIndicatorValues", new[] { "DepartmentID" });
            DropIndex("dbo.DepartmentCategoryIndicatorMaps", new[] { "IndicatorID" });
            DropIndex("dbo.DepartmentCategoryIndicatorMaps", new[] { "DepartmentCategoryID" });
            DropIndex("dbo.DepartmentIndicatorStandardValues", new[] { "IndicatorID" });
            DropIndex("dbo.DepartmentIndicatorStandardValues", new[] { "DepartmentID" });
            DropIndex("dbo.Departments", new[] { "DepartmentCategoryID" });
            DropTable("dbo.DepartmentIndicatorValues");
            DropTable("dbo.DepartmentCategoryIndicatorMaps");
            DropTable("dbo.Indicators");
            DropTable("dbo.DepartmentIndicatorStandardValues");
            DropTable("dbo.Departments");
            DropTable("dbo.DepartmentCategories");
        }
    }
}
