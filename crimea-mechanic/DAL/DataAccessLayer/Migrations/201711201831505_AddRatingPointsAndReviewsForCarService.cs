using System.Data.Entity.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddRatingPointsAndReviewsForCarService : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarServiceReviews",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Mark = c.Byte(nullable: false),
                        Review = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Service_Id = c.Long(),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarServices", t => t.Service_Id)
                .ForeignKey("dbo.UserProfiles", t => t.User_Id)
                .Index(t => t.Service_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.CarServices", "Points", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarServiceReviews", "User_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.CarServiceReviews", "Service_Id", "dbo.CarServices");
            DropIndex("dbo.CarServiceReviews", new[] { "User_Id" });
            DropIndex("dbo.CarServiceReviews", new[] { "Service_Id" });
            DropColumn("dbo.CarServices", "Points");
            DropTable("dbo.CarServiceReviews");
        }
    }
}
