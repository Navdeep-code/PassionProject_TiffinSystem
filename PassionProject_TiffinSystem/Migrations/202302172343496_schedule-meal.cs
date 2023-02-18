namespace PassionProject_TiffinSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schedulemeal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SchedulexMeals",
                c => new
                    {
                        SchedulexMealId = c.Int(nullable: false, identity: true),
                        ScheduleId = c.Int(nullable: false),
                        MealId = c.Int(nullable: false),
                        Day = c.String(),
                    })
                .PrimaryKey(t => t.SchedulexMealId)
                .ForeignKey("dbo.Meals", t => t.MealId, cascadeDelete: true)
                .ForeignKey("dbo.Schedules", t => t.ScheduleId, cascadeDelete: true)
                .Index(t => t.ScheduleId)
                .Index(t => t.MealId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchedulexMeals", "ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.SchedulexMeals", "MealId", "dbo.Meals");
            DropIndex("dbo.SchedulexMeals", new[] { "MealId" });
            DropIndex("dbo.SchedulexMeals", new[] { "ScheduleId" });
            DropTable("dbo.SchedulexMeals");
        }
    }
}
