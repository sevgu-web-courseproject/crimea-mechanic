using System.Data.Entity.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AddFieldDeclineReasonIntoCarService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarServices", "DeclineReason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarServices", "DeclineReason");
        }
    }
}