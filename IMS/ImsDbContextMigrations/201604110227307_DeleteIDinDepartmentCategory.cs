namespace IMS.ImsDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteIDinDepartmentCategory : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DepartmentCategories", "DepartmentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DepartmentCategories", "DepartmentID", c => c.Guid(nullable: false));
        }
    }
}
