using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject_TiffinSystem.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleID { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        //Schedule can have many meals
        //A meal can be a part of many schedules

        public ICollection<Meal> Meals { get; set; }

    }


    public class ScheduleDto {
        public int ScheduleID { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

}