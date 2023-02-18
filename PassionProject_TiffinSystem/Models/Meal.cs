using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject_TiffinSystem.Models
{
    public class Meal
    {
        [Key]
        public int MealID { get; set; }

        public string Mealtype { get; set; }

        //Schedule can have many meals
        //A meal can be a part of many schedules

      //  public ICollection<Schedule> Schedules { get; set; }

        public ICollection<Food> food { get; set; }
    }

    public class MealDto {
        public int MealID { get; set; }

        public string Mealtype { get; set; }
    }

}