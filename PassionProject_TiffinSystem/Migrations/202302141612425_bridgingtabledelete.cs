namespace PassionProject_TiffinSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bridgingtabledelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScheduleMeals", "Schedule_ScheduleID", "dbo.Schedules");
            DropForeignKey("dbo.ScheduleMeals", "Meal_MealID", "dbo.Meals");
            DropIndex("dbo.ScheduleMeals", new[] { "Schedule_ScheduleID" });
            DropIndex("dbo.ScheduleMeals", new[] { "Meal_MealID" });
            DropTable("dbo.ScheduleMeals");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ScheduleMeals",
                c => new
                    {
                        Schedule_ScheduleID = c.Int(nullable: false),
                        Meal_MealID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Schedule_ScheduleID, t.Meal_MealID });
            
            CreateIndex("dbo.ScheduleMeals", "Meal_MealID");
            CreateIndex("dbo.ScheduleMeals", "Schedule_ScheduleID");
            AddForeignKey("dbo.ScheduleMeals", "Meal_MealID", "dbo.Meals", "MealID", cascadeDelete: true);
            AddForeignKey("dbo.ScheduleMeals", "Schedule_ScheduleID", "dbo.Schedules", "ScheduleID", cascadeDelete: true);
        }
    }
}
