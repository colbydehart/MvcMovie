namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Razzie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Razzie", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Razzie");
        }
    }
}
