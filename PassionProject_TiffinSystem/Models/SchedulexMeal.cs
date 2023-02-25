using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassionProject_TiffinSystem.Models
{
    public class SchedulexMeal
    {
      [Key]
      public int  SchedulexMealId {get;set;}

      [ForeignKey("Schedule")]
      public int ScheduleId {get;set;}
      public virtual Schedule Schedule { get; set; }

      [ForeignKey("Meal")]
      public int MealId { get; set; }
      public virtual Meal Meal { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }

        public virtual Food Food { get; set; }

        public string Day { get; set; }
    }

    public class SchedulexMealDto
    {
        [Key]
        public int SchedulexMealId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public int MealId { get; set; } 
        public string Mealtype { get; set; }
        public int FoodID { get; set; }

        public string Foodtype { get; set; }

        public string ItemName { get; set; }

        public string Vegetarian { get; set; }
        public string Day { get; set; }
    }

}