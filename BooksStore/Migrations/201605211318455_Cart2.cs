namespace BooksStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CartItems", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CartItems", "UserName", c => c.Int(nullable: false));
        }
    }
}
