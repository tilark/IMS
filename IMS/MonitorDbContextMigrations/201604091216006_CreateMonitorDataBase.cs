namespace IMS.MonitorDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMonitorDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DepartmentMonitors",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        DepartmentID = c.Long(nullable: false),
                        MonitorItemID = c.Long(nullable: false),
                        Value = c.String(maxLength: 30),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .ForeignKey("dbo.MonitorItems", t => t.MonitorItemID, cascadeDelete: true)
                .Index(t => t.DepartmentID)
                .Index(t => t.MonitorItemID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Long(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.MonitorItems",
                c => new
                    {
                        MonitorItemID = c.Long(nullable: false, identity: true),
                        MonitorName = c.String(),
                        StandardValue = c.String(maxLength: 30),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.MonitorItemID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DepartmentMonitors", "MonitorItemID", "dbo.MonitorItems");
            DropForeignKey("dbo.DepartmentMonitors", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.DepartmentMonitors", new[] { "MonitorItemID" });
            DropIndex("dbo.DepartmentMonitors", new[] { "DepartmentID" });
            DropTable("dbo.MonitorItems");
            DropTable("dbo.Departments");
            DropTable("dbo.DepartmentMonitors");
        }
    }
}
