using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProject_TiffinSystem.Models;

namespace PassionProject_TiffinSystem.Controllers
{
    public class SchedulexMealDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Gets the Meals associated with a particular Schedules, alongside a status code of 200 (OK)
        /// </summary>
        /// <param name="id">Schedule MealID</param>
        /// <returns>
        /// A list of Meals for a schedule, stored in data transfer objects.
        /// </returns>
        public IHttpActionResult GetMealForSchedule(int id)
        {

            //Retrieve by accessing the bridging table directly
            IEnumerable<SchedulexMeal> SxMs = db.SchedulexMeals
                .Where(sxm => sxm.ScheduleId == id)
                //.Include(sxm => sxm.ScheduleId) //This step can be necessary if you can't grab the data
                .ToList();

            List<MealDto> MealDtos = new List<MealDto> { };
            foreach (var sxm in SxMs)
            {
                MealDto NewMeal = new MealDto
                {
                    MealID = sxm.Meal.MealID,
                    Mealtype = sxm.Meal.Mealtype
                };
                MealDtos.Add(NewMeal);

            }

            return Ok(MealDtos);



        }
    }
}