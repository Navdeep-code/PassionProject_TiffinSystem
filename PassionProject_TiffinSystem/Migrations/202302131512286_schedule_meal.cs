namespace PassionProject_TiffinSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schedule_meal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        MealID = c.Int(nullable: false, identity: true),
                        Mealtype = c.String(),
                    })
                .PrimaryKey(t => t.MealID);
            
            CreateTable(
                "dbo.ScheduleMeals",
                c => new
                    {
                        Schedule_ScheduleID = c.Int(nullable: false),
                        Meal_MealID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Schedule_ScheduleID, t.Meal_MealID })
                .ForeignKey("dbo.Schedules", t => t.Schedule_ScheduleID, cascadeDelete: true)
                .ForeignKey("dbo.Meals", t => t.Meal_MealID, cascadeDelete: true)
                .Index(t => t.Schedule_ScheduleID)
                .Index(t => t.Meal_MealID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleMeals", "Meal_MealID", "dbo.Meals");
            DropForeignKey("dbo.ScheduleMeals", "Schedule_ScheduleID", "dbo.Schedules");
            DropIndex("dbo.ScheduleMeals", new[] { "Meal_MealID" });
            DropIndex("dbo.ScheduleMeals", new[] { "Schedule_ScheduleID" });
            DropTable("dbo.ScheduleMeals");
            DropTable("dbo.Meals");
        }
    }
}
