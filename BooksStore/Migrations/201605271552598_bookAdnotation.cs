namespace BooksStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookAdnotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Title", c => c.String());
        }
    }
}
