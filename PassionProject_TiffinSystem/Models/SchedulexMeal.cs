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

      public string Day { get; set; }
    }
}