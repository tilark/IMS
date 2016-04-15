namespace IMS.ImsDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndicatorAlgorithm : DbMigration
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
            CreateTable(
                "dbo.IndicatorAlgorithms",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ResultOperationID = c.Guid(),
                        FirstOperationID = c.Guid(),
                        SecondOperationID = c.Guid(),
                        Operation = c.String(),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Indicators", t => t.FirstOperationID)
                .ForeignKey("dbo.Indicators", t => t.ResultOperationID)
                .ForeignKey("dbo.Indicators", t => t.SecondOperationID)
                .Index(t => t.ResultOperationID)
                .Index(t => t.FirstOperationID)
                .Index(t => t.SecondOperationID);
            
            AlterColumn("dbo.Departments", "DepartmentCategoryID", c => c.Guid(nullable: false));
            AlterColumn("dbo.DepartmentCategoryIndicatorMaps", "DepartmentCategoryID", c => c.Guid(nullable: false));
            AlterColumn("dbo.DepartmentCategoryIndicatorMaps", "IndicatorID", c => c.Guid(nullable: false));
            AlterColumn("dbo.DepartmentIndicatorStandardValues", "DepartmentID", c => c.Guid(nullable: false));
            AlterColumn("dbo.DepartmentIndicatorStandardValues", "IndicatorID", c => c.Guid(nullable: false));
            AlterColumn("dbo.DepartmentIndicatorValues", "DepartmentID", c => c.Guid(nullable: false));
            AlterColumn("dbo.DepartmentIndicatorValues", "IndicatorID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Departments", "DepartmentCategoryID");
            CreateIndex("dbo.DepartmentCategoryIndicatorMaps", "DepartmentCategoryID");
            CreateIndex("dbo.DepartmentCategoryIndicatorMaps", "IndicatorID");
            CreateIndex("dbo.DepartmentIndicatorStandardValues", "DepartmentID");
            CreateIndex("dbo.DepartmentIndicatorStandardValues", "IndicatorID");
            CreateIndex("dbo.DepartmentIndicatorValues", "DepartmentID");
            CreateIndex("dbo.DepartmentIndicatorValues", "IndicatorID");
            AddForeignKey("dbo.DepartmentCategoryIndicatorMaps", "IndicatorID", "dbo.Indicators", "IndicatorID", cascadeDelete: true);
            AddForeignKey("dbo.DepartmentIndicatorStandardValues", "IndicatorID", "dbo.Indicators", "IndicatorID", cascadeDelete: true);
            AddForeignKey("dbo.DepartmentIndicatorValues", "IndicatorID", "dbo.Indicators", "IndicatorID", cascadeDelete: true);
            AddForeignKey("dbo.Departments", "DepartmentCategoryID", "dbo.DepartmentCategories", "DepartmentCategoryID", cascadeDelete: true);
            AddForeignKey("dbo.DepartmentIndicatorStandardValues", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: true);
            AddForeignKey("dbo.DepartmentIndicatorValues", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: true);
            AddForeignKey("dbo.DepartmentCategoryIndicatorMaps", "DepartmentCategoryID", "dbo.DepartmentCategories", "DepartmentCategoryID", cascadeDelete: true);
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
            DropForeignKey("dbo.IndicatorAlgorithms", "SecondOperationID", "dbo.Indicators");
            DropForeignKey("dbo.IndicatorAlgorithms", "ResultOperationID", "dbo.Indicators");
            DropForeignKey("dbo.IndicatorAlgorithms", "FirstOperationID", "dbo.Indicators");
            DropIndex("dbo.IndicatorAlgorithms", new[] { "SecondOperationID" });
            DropIndex("dbo.IndicatorAlgorithms", new[] { "FirstOperationID" });
            DropIndex("dbo.IndicatorAlgorithms", new[] { "ResultOperationID" });
            DropIndex("dbo.DepartmentIndicatorValues", new[] { "IndicatorID" });
            DropIndex("dbo.DepartmentIndicatorValues", new[] { "DepartmentID" });
            DropIndex("dbo.DepartmentIndicatorStandardValues", new[] { "IndicatorID" });
            DropIndex("dbo.DepartmentIndicatorStandardValues", new[] { "DepartmentID" });
            DropIndex("dbo.DepartmentCategoryIndicatorMaps", new[] { "IndicatorID" });
            DropIndex("dbo.DepartmentCategoryIndicatorMaps", new[] { "DepartmentCategoryID" });
            DropIndex("dbo.Departments", new[] { "DepartmentCategoryID" });
            AlterColumn("dbo.DepartmentIndicatorValues", "IndicatorID", c => c.Guid());
            AlterColumn("dbo.DepartmentIndicatorValues", "DepartmentID", c => c.Guid());
            AlterColumn("dbo.DepartmentIndicatorStandardValues", "IndicatorID", c => c.Guid());
            AlterColumn("dbo.DepartmentIndicatorStandardValues", "DepartmentID", c => c.Guid());
            AlterColumn("dbo.DepartmentCategoryIndicatorMaps", "IndicatorID", c => c.Guid());
            AlterColumn("dbo.DepartmentCategoryIndicatorMaps", "DepartmentCategoryID", c => c.Guid());
            AlterColumn("dbo.Departments", "DepartmentCategoryID", c => c.Guid());
            DropTable("dbo.IndicatorAlgorithms");
            CreateIndex("dbo.DepartmentIndicatorValues", "IndicatorID");
            CreateIndex("dbo.DepartmentIndicatorValues", "DepartmentID");
            CreateIndex("dbo.DepartmentIndicatorStandardValues", "IndicatorID");
            CreateIndex("dbo.DepartmentIndicatorStandardValues", "DepartmentID");
            CreateIndex("dbo.DepartmentCategoryIndicatorMaps", "IndicatorID");
            CreateIndex("dbo.DepartmentCategoryIndicatorMaps", "DepartmentCategoryID");
            CreateIndex("dbo.Departments", "DepartmentCategoryID");
            AddForeignKey("dbo.DepartmentCategoryIndicatorMaps", "DepartmentCategoryID", "dbo.DepartmentCategories", "DepartmentCategoryID");
            AddForeignKey("dbo.DepartmentIndicatorValues", "DepartmentID", "dbo.Departments", "DepartmentID");
            AddForeignKey("dbo.DepartmentIndicatorStandardValues", "DepartmentID", "dbo.Departments", "DepartmentID");
            AddForeignKey("dbo.Departments", "DepartmentCategoryID", "dbo.DepartmentCategories", "DepartmentCategoryID");
            AddForeignKey("dbo.DepartmentIndicatorValues", "IndicatorID", "dbo.Indicators", "IndicatorID");
            AddForeignKey("dbo.DepartmentIndicatorStandardValues", "IndicatorID", "dbo.Indicators", "IndicatorID");
            AddForeignKey("dbo.DepartmentCategoryIndicatorMaps", "IndicatorID", "dbo.Indicators", "IndicatorID");
        }
    }
}
