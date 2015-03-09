namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedorBroke : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "userName", c => c.String());
            CreateIndex("dbo.Ratings", "MovieId");
            AddForeignKey("dbo.Ratings", "MovieId", "dbo.Movies", "ID", cascadeDelete: true);
            DropColumn("dbo.Ratings", "userId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "userId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Ratings", "MovieId", "dbo.Movies");
            DropIndex("dbo.Ratings", new[] { "MovieId" });
            DropColumn("dbo.Ratings", "userName");
        }
    }
}
