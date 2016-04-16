namespace IMS.ApplicationDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserDatabase1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "EmployeeNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "EmployeeNo");
        }
    }
}
