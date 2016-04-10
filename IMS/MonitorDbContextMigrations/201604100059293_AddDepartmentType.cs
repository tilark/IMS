namespace IMS.MonitorDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDepartmentType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DepartmentTypes",
                c => new
                    {
                        DepartmentTypeID = c.Long(nullable: false, identity: true),
                        TypeName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DepartmentTypeID);
            
            AddColumn("dbo.DepartmentMonitors", "DepartmentTypeID", c => c.Long(nullable: false));
            CreateIndex("dbo.DepartmentMonitors", "DepartmentTypeID");
            AddForeignKey("dbo.DepartmentMonitors", "DepartmentTypeID", "dbo.DepartmentTypes", "DepartmentTypeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DepartmentMonitors", "DepartmentTypeID", "dbo.DepartmentTypes");
            DropIndex("dbo.DepartmentMonitors", new[] { "DepartmentTypeID" });
            DropColumn("dbo.DepartmentMonitors", "DepartmentTypeID");
            DropTable("dbo.DepartmentTypes");
        }
    }
}
