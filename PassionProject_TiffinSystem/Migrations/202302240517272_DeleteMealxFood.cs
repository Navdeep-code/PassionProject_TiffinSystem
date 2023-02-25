namespace PassionProject_TiffinSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteMealxFood : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MealFoods", "Meal_MealID", "dbo.Meals");
            DropForeignKey("dbo.MealFoods", "Food_FoodID", "dbo.Foods");
            DropIndex("dbo.MealFoods", new[] { "Meal_MealID" });
            DropIndex("dbo.MealFoods", new[] { "Food_FoodID" });
            AddColumn("dbo.SchedulexMeals", "FoodId", c => c.Int(nullable: false));
            CreateIndex("dbo.SchedulexMeals", "FoodId");
            AddForeignKey("dbo.SchedulexMeals", "FoodId", "dbo.Foods", "FoodID", cascadeDelete: true);
            DropTable("dbo.MealFoods");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MealFoods",
                c => new
                    {
                        Meal_MealID = c.Int(nullable: false),
                        Food_FoodID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meal_MealID, t.Food_FoodID });
            
            DropForeignKey("dbo.SchedulexMeals", "FoodId", "dbo.Foods");
            DropIndex("dbo.SchedulexMeals", new[] { "FoodId" });
            DropColumn("dbo.SchedulexMeals", "FoodId");
            CreateIndex("dbo.MealFoods", "Food_FoodID");
            CreateIndex("dbo.MealFoods", "Meal_MealID");
            AddForeignKey("dbo.MealFoods", "Food_FoodID", "dbo.Foods", "FoodID", cascadeDelete: true);
            AddForeignKey("dbo.MealFoods", "Meal_MealID", "dbo.Meals", "MealID", cascadeDelete: true);
        }
    }
}
