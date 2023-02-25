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

        public string Vegetarian { get; set; }


    }
    public class FoodDto {
        public int FoodID { get; set; }

        public string Foodtype { get; set; }

        public string ItemName { get; set; }

        public string Vegetarian { get; set; }
    }


}