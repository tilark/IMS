namespace IMS.ImsDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndicatorUnit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DepartmentIndicatorStandardValues", "Unit", c => c.String());
            AddColumn("dbo.DepartmentIndicatorValues", "Unit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DepartmentIndicatorValues", "Unit");
            DropColumn("dbo.DepartmentIndicatorStandardValues", "Unit");
        }
    }
}
