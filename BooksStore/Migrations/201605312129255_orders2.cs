namespace BooksStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orders2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "State");
            DropColumn("dbo.Orders", "Country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Country", c => c.String(nullable: false, maxLength: 40));
            AddColumn("dbo.Orders", "State", c => c.String(nullable: false, maxLength: 40));
        }
    }
}
