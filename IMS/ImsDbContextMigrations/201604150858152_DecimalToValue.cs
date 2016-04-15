namespace IMS.ImsDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DecimalToValue : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DepartmentIndicatorStandardValues", "StandardValue", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.DepartmentIndicatorValues", "Value", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DepartmentIndicatorValues", "Value", c => c.String(maxLength: 30));
            AlterColumn("dbo.DepartmentIndicatorStandardValues", "StandardValue", c => c.String(maxLength: 100));
        }
    }
}
