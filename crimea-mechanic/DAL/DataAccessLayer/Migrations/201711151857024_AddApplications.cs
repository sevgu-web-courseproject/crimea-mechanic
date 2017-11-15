using System.Data.Entity.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddApplications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationOffers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Price = c.Single(nullable: false),
                        Content = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Service_Id = c.Long(),
                        Application_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarServices", t => t.Service_Id)
                .ForeignKey("dbo.Applications", t => t.Application_Id)
                .Index(t => t.Service_Id)
                .Index(t => t.Application_Id);
            
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        State = c.Byte(nullable: false),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Car_Id = c.Long(),
                        Service_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserCars", t => t.Car_Id)
                .ForeignKey("dbo.CarServices", t => t.Service_Id)
                .Index(t => t.Car_Id)
                .Index(t => t.Service_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationOffers", "Application_Id", "dbo.Applications");
            DropForeignKey("dbo.ApplicationOffers", "Service_Id", "dbo.CarServices");
            DropForeignKey("dbo.Applications", "Service_Id", "dbo.CarServices");
            DropForeignKey("dbo.Applications", "Car_Id", "dbo.UserCars");
            DropIndex("dbo.Applications", new[] { "Service_Id" });
            DropIndex("dbo.Applications", new[] { "Car_Id" });
            DropIndex("dbo.ApplicationOffers", new[] { "Application_Id" });
            DropIndex("dbo.ApplicationOffers", new[] { "Service_Id" });
            DropTable("dbo.Applications");
            DropTable("dbo.ApplicationOffers");
        }
    }
}
