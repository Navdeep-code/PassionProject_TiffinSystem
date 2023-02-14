namespace PassionProject_TiffinSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mealfood : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        FoodID = c.Int(nullable: false, identity: true),
                        Foodtype = c.String(),
                        ItemName = c.String(),
                        Vegetarian = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FoodID);
            
            CreateTable(
                "dbo.MealFoods",
                c => new
                    {
                        Meal_MealID = c.Int(nullable: false),
                        Food_FoodID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meal_MealID, t.Food_FoodID })
                .ForeignKey("dbo.Meals", t => t.Meal_MealID, cascadeDelete: true)
                .ForeignKey("dbo.Foods", t => t.Food_FoodID, cascadeDelete: true)
                .Index(t => t.Meal_MealID)
                .Index(t => t.Food_FoodID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MealFoods", "Food_FoodID", "dbo.Foods");
            DropForeignKey("dbo.MealFoods", "Meal_MealID", "dbo.Meals");
            DropIndex("dbo.MealFoods", new[] { "Food_FoodID" });
            DropIndex("dbo.MealFoods", new[] { "Meal_MealID" });
            DropTable("dbo.MealFoods");
            DropTable("dbo.Foods");
        }
    }
}
