namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRatings1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        userId = c.Int(nullable: false),
                        movieId = c.Int(nullable: false),
                        OneToFive = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ratings");
        }
    }
}
