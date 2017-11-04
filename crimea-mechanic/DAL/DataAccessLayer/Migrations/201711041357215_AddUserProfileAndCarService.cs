using System.Data.Entity.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddUserProfileAndCarService : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarModelTags",
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
                "dbo.CarServices",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        ManagerName = c.String(),
                        Site = c.String(),
                        TimetableWorks = c.String(),
                        About = c.String(),
                        Updated = c.DateTime(nullable: false),
                        State = c.Byte(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Updated = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.CarServiceFiles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        Type = c.Byte(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CarService_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarServices", t => t.CarService_Id)
                .Index(t => t.CarService_Id);
            
            CreateTable(
                "dbo.CarServicePhones",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Number = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CarService_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarServices", t => t.CarService_Id)
                .Index(t => t.CarService_Id);
            
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
            
            CreateTable(
                "dbo.CarServiceCarModelTags",
                c => new
                    {
                        CarService_Id = c.Long(nullable: false),
                        CarModelTag_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.CarService_Id, t.CarModelTag_Id })
                .ForeignKey("dbo.CarServices", t => t.CarService_Id, cascadeDelete: true)
                .ForeignKey("dbo.CarModelTags", t => t.CarModelTag_Id, cascadeDelete: true)
                .Index(t => t.CarService_Id)
                .Index(t => t.CarModelTag_Id);
            
            CreateTable(
                "dbo.WorkTagCarServices",
                c => new
                    {
                        WorkTag_Id = c.Long(nullable: false),
                        CarService_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkTag_Id, t.CarService_Id })
                .ForeignKey("dbo.WorkTags", t => t.WorkTag_Id, cascadeDelete: true)
                .ForeignKey("dbo.CarServices", t => t.CarService_Id, cascadeDelete: true)
                .Index(t => t.WorkTag_Id)
                .Index(t => t.CarService_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkTagCarServices", "CarService_Id", "dbo.CarServices");
            DropForeignKey("dbo.WorkTagCarServices", "WorkTag_Id", "dbo.WorkTags");
            DropForeignKey("dbo.CarServicePhones", "CarService_Id", "dbo.CarServices");
            DropForeignKey("dbo.CarServiceFiles", "CarService_Id", "dbo.CarServices");
            DropForeignKey("dbo.CarServiceCarModelTags", "CarModelTag_Id", "dbo.CarModelTags");
            DropForeignKey("dbo.CarServiceCarModelTags", "CarService_Id", "dbo.CarServices");
            DropForeignKey("dbo.CarServices", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserProfiles", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.WorkTagCarServices", new[] { "CarService_Id" });
            DropIndex("dbo.WorkTagCarServices", new[] { "WorkTag_Id" });
            DropIndex("dbo.CarServiceCarModelTags", new[] { "CarModelTag_Id" });
            DropIndex("dbo.CarServiceCarModelTags", new[] { "CarService_Id" });
            DropIndex("dbo.CarServicePhones", new[] { "CarService_Id" });
            DropIndex("dbo.CarServiceFiles", new[] { "CarService_Id" });
            DropIndex("dbo.UserProfiles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.CarServices", new[] { "ApplicationUser_Id" });
            DropTable("dbo.WorkTagCarServices");
            DropTable("dbo.CarServiceCarModelTags");
            DropTable("dbo.WorkTags");
            DropTable("dbo.CarServicePhones");
            DropTable("dbo.CarServiceFiles");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.CarServices");
            DropTable("dbo.CarModelTags");
        }
    }
}
