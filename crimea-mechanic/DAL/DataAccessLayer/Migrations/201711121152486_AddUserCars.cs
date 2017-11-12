using System.Data.Entity.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddUserCars : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCars",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.Int(nullable: false),
                        Vin = c.String(),
                        FuelType = c.Byte(nullable: false),
                        EngineCapacity = c.Single(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Model_Id = c.Long(),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarModels", t => t.Model_Id)
                .ForeignKey("dbo.UserProfiles", t => t.User_Id)
                .Index(t => t.Model_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCars", "User_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.UserCars", "Model_Id", "dbo.CarModels");
            DropIndex("dbo.UserCars", new[] { "User_Id" });
            DropIndex("dbo.UserCars", new[] { "Model_Id" });
            DropTable("dbo.UserCars");
        }
    }
}
