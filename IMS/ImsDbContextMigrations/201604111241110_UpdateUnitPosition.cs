namespace IMS.ImsDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUnitPosition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Indicators", "Unit", c => c.String());
            DropColumn("dbo.DepartmentIndicatorStandardValues", "Unit");
            DropColumn("dbo.DepartmentIndicatorValues", "Unit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DepartmentIndicatorValues", "Unit", c => c.String());
            AddColumn("dbo.DepartmentIndicatorStandardValues", "Unit", c => c.String());
            DropColumn("dbo.Indicators", "Unit");
        }
    }
}
