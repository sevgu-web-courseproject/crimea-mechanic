namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMultipleWorkTypes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Applications", "WorkType_Id", "dbo.WorkTypes");
            DropIndex("dbo.Applications", new[] { "WorkType_Id" });
            CreateTable(
                "dbo.WorkTypeApplications",
                c => new
                    {
                        WorkType_Id = c.Long(nullable: false),
                        Application_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkType_Id, t.Application_Id })
                .ForeignKey("dbo.WorkTypes", t => t.WorkType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Applications", t => t.Application_Id, cascadeDelete: true)
                .Index(t => t.WorkType_Id)
                .Index(t => t.Application_Id);
            
            DropColumn("dbo.Applications", "WorkType_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Applications", "WorkType_Id", c => c.Long());
            DropForeignKey("dbo.WorkTypeApplications", "Application_Id", "dbo.Applications");
            DropForeignKey("dbo.WorkTypeApplications", "WorkType_Id", "dbo.WorkTypes");
            DropIndex("dbo.WorkTypeApplications", new[] { "Application_Id" });
            DropIndex("dbo.WorkTypeApplications", new[] { "WorkType_Id" });
            DropTable("dbo.WorkTypeApplications");
            CreateIndex("dbo.Applications", "WorkType_Id");
            AddForeignKey("dbo.Applications", "WorkType_Id", "dbo.WorkTypes", "Id");
        }
    }
}
