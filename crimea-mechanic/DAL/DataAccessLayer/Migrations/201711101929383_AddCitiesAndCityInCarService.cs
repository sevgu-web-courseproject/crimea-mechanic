using System.Data.Entity.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddCitiesAndCityInCarService : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CarServices", "City_Id", c => c.Long());
            CreateIndex("dbo.CarServices", "City_Id");
            AddForeignKey("dbo.CarServices", "City_Id", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarServices", "City_Id", "dbo.Cities");
            DropIndex("dbo.CarServices", new[] { "City_Id" });
            DropColumn("dbo.CarServices", "City_Id");
            DropTable("dbo.Cities");
        }
    }
}
