using System.Data.Entity.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class RemoveWorkTags : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WorkTagCarServices", "WorkTag_Id", "dbo.WorkTags");
            DropForeignKey("dbo.WorkTagCarServices", "CarService_Id", "dbo.CarServices");
            DropIndex("dbo.WorkTagCarServices", new[] { "WorkTag_Id" });
            DropIndex("dbo.WorkTagCarServices", new[] { "CarService_Id" });
            DropTable("dbo.WorkTags");
            DropTable("dbo.WorkTagCarServices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WorkTagCarServices",
                c => new
                    {
                        WorkTag_Id = c.Long(nullable: false),
                        CarService_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkTag_Id, t.CarService_Id });
            
            CreateTable(
                "dbo.WorkTags",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.WorkTagCarServices", "CarService_Id");
            CreateIndex("dbo.WorkTagCarServices", "WorkTag_Id");
            AddForeignKey("dbo.WorkTagCarServices", "CarService_Id", "dbo.CarServices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WorkTagCarServices", "WorkTag_Id", "dbo.WorkTags", "Id", cascadeDelete: true);
        }
    }
}
