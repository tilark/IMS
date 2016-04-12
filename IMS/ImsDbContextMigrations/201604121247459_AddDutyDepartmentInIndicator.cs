namespace IMS.ImsDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDutyDepartmentInIndicator : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Indicators", "DutyDepartment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Indicators", "DutyDepartment");
        }
    }
}
