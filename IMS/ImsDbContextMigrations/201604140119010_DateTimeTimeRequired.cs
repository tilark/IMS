namespace IMS.ImsDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeTimeRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DepartmentIndicatorValues", "Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DepartmentIndicatorValues", "Time", c => c.DateTime());
        }
    }
}
