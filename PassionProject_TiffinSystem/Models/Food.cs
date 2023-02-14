using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject_TiffinSystem.Models
{
    public class Food
    {
        [Key]
        public int FoodID { get; set; }

        public string Foodtype { get; set; }

        public string ItemName { get; set; }

        public bool Vegetarian { get; set; }

        //A food item can be a part of many meals
        //A meal can have many food items

        public ICollection<Meal> Meals { get; set; }
    }
    public class FoodDto {
        public int FoodID { get; set; }

        public string Foodtype { get; set; }

        public string ItemName { get; set; }

        public bool Vegetarian { get; set; }
    }


}