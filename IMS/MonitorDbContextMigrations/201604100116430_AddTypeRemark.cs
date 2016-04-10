namespace IMS.MonitorDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTypeRemark : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DepartmentTypes", "Remark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DepartmentTypes", "Remark");
        }
    }
}
