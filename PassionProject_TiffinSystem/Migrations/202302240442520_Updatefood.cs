namespace PassionProject_TiffinSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatefood : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Foods", "Vegetarian", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Foods", "Vegetarian", c => c.Boolean(nullable: false));
        }
    }
}
