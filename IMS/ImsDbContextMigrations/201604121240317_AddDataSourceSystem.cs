namespace IMS.ImsDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataSourceSystem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataSourceSystems",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Remarks = c.String(),
                        TimeStamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Indicators", "IsAutoGetData", c => c.Boolean(nullable: false, defaultValue:false));
            AddColumn("dbo.Indicators", "DepartmentID", c => c.Guid());
            AddColumn("dbo.Indicators", "DataSourceSystemID", c => c.Guid());
            CreateIndex("dbo.Indicators", "DepartmentID");
            CreateIndex("dbo.Indicators", "DataSourceSystemID");
            AddForeignKey("dbo.Indicators", "DataSourceSystemID", "dbo.DataSourceSystems", "ID");
            AddForeignKey("dbo.Indicators", "DepartmentID", "dbo.Departments", "DepartmentID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Indicators", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Indicators", "DataSourceSystemID", "dbo.DataSourceSystems");
            DropIndex("dbo.Indicators", new[] { "DataSourceSystemID" });
            DropIndex("dbo.Indicators", new[] { "DepartmentID" });
            DropColumn("dbo.Indicators", "DataSourceSystemID");
            DropColumn("dbo.Indicators", "DepartmentID");
            DropColumn("dbo.Indicators", "IsAutoGetData");
            DropTable("dbo.DataSourceSystems");
        }
    }
}
