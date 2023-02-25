using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject_TiffinSystem.Models.ViewModels
{
    public class AddMeal
    {
        //ViewModel needed to add a meal to a schedule
        //for schedule information
        public IEnumerable<ScheduleDto> SelectedSchedule { get; set; }

        public IEnumerable<MealDto> MealType { get; set; }

        public SchedulexMealDto DayOptions { get; set; }

        public IEnumerable<FoodDto> foodoptions { get; set; }

         public Food Foodtype { get; set; }

    }
}