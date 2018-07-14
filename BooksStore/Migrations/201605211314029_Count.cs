namespace BooksStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Count : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Count", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Count", c => c.Int(nullable: false));
        }
    }
}
