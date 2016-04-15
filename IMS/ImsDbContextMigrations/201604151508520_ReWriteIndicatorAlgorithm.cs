namespace IMS.ImsDbContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReWriteIndicatorAlgorithm : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IndicatorAlgorithms", "FirstOperationID", "dbo.Indicators");
            DropForeignKey("dbo.IndicatorAlgorithms", "ResultOperationID", "dbo.Indicators");
            DropForeignKey("dbo.IndicatorAlgorithms", "SecondOperationID", "dbo.Indicators");
            DropIndex("dbo.IndicatorAlgorithms", new[] { "ResultOperationID" });
            DropIndex("dbo.IndicatorAlgorithms", new[] { "FirstOperationID" });
            DropIndex("dbo.IndicatorAlgorithms", new[] { "SecondOperationID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.IndicatorAlgorithms", "SecondOperationID");
            CreateIndex("dbo.IndicatorAlgorithms", "FirstOperationID");
            CreateIndex("dbo.IndicatorAlgorithms", "ResultOperationID");
            AddForeignKey("dbo.IndicatorAlgorithms", "SecondOperationID", "dbo.Indicators", "IndicatorID");
            AddForeignKey("dbo.IndicatorAlgorithms", "ResultOperationID", "dbo.Indicators", "IndicatorID");
            AddForeignKey("dbo.IndicatorAlgorithms", "FirstOperationID", "dbo.Indicators", "IndicatorID");
        }
    }
}
