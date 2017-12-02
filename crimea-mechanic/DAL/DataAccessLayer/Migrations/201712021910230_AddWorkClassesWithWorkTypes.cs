using System.Data.Entity.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddWorkClassesWithWorkTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Class_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkClasses", t => t.Class_Id)
                .Index(t => t.Class_Id);
            
            CreateTable(
                "dbo.WorkClasses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkTypeCarServices",
                c => new
                    {
                        WorkType_Id = c.Long(nullable: false),
                        CarService_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkType_Id, t.CarService_Id })
                .ForeignKey("dbo.WorkTypes", t => t.WorkType_Id, cascadeDelete: true)
                .ForeignKey("dbo.CarServices", t => t.CarService_Id, cascadeDelete: true)
                .Index(t => t.WorkType_Id)
                .Index(t => t.CarService_Id);
            
            AddColumn("dbo.Applications", "WorkType_Id", c => c.Long());
            CreateIndex("dbo.Applications", "WorkType_Id");
            AddForeignKey("dbo.Applications", "WorkType_Id", "dbo.WorkTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applications", "WorkType_Id", "dbo.WorkTypes");
            DropForeignKey("dbo.WorkTypes", "Class_Id", "dbo.WorkClasses");
            DropForeignKey("dbo.WorkTypeCarServices", "CarService_Id", "dbo.CarServices");
            DropForeignKey("dbo.WorkTypeCarServices", "WorkType_Id", "dbo.WorkTypes");
            DropIndex("dbo.WorkTypeCarServices", new[] { "CarService_Id" });
            DropIndex("dbo.WorkTypeCarServices", new[] { "WorkType_Id" });
            DropIndex("dbo.WorkTypes", new[] { "Class_Id" });
            DropIndex("dbo.Applications", new[] { "WorkType_Id" });
            DropColumn("dbo.Applications", "WorkType_Id");
            DropTable("dbo.WorkTypeCarServices");
            DropTable("dbo.WorkClasses");
            DropTable("dbo.WorkTypes");
        }
    }
}
