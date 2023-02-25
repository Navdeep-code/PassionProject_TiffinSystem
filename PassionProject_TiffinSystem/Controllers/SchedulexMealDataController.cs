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
        /// add Meals associated with a particular Schedule
        /// </summary>
        /// <param name="SchedulexMeal">JSON Object</param>
        /// <returns>
        /// Add Meal for a schedule
        /// </returns>

        // POST: api/SchedulexMealData/AddMealToSchedule
        [ResponseType(typeof(SchedulexMeal))]
        [HttpPost]
        public IHttpActionResult AddMealToSchedule(SchedulexMeal schedulexmeal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SchedulexMeals.Add(schedulexmeal);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = schedulexmeal.SchedulexMealId }, schedulexmeal);
        }

        [HttpGet]
        [ResponseType(typeof(MealDto))]
        /// <summary>
        /// Gets the Meals associated with a particular Schedule, alongside a status code of 200 (OK)
        /// </summary>
        /// <param name="id">ScheduleID</param>
        /// <returns>
        /// A list of Meals for a schedule, stored in data transfer objects.
        /// </returns>
        /// 
        public IHttpActionResult ListMealsForSchedule(int scheduleid)
        {

            //Retrieve by accessing the bridging table directly
            IEnumerable<SchedulexMeal> SxMs = db.SchedulexMeals
                .Where(sxm => sxm.ScheduleId == scheduleid)
                //.Include(sxm => sxm.ScheduleId) //This step can be necessary if you can't grab the data
                .ToList();

            List<MealDto> MealDto = new List<MealDto> { };
            foreach (var sxm in SxMs)
            {
                MealDto NewMeal = new MealDto
                {
                    MealID = sxm.Meal.MealID,
                    Mealtype = sxm.Meal.Mealtype
                };
                MealDto.Add(NewMeal);

            }

            return Ok(MealDto);



        }

        /// <summary>
        /// Gets the Schedules associated with a particular Meal, alongside a status code of 200 (OK)
        /// </summary>
        /// <param name="id">MealID</param>
        /// <returns>
        /// A list of schedules for a meal, stored in data transfer objects.
        /// </returns>
        public IHttpActionResult ListSchedulesForMeal(int mealid)
        {

            //Retrieve by accessing the bridging table directly
            IEnumerable<SchedulexMeal> SxMs = db.SchedulexMeals
                .Where(sxm => sxm.MealId == mealid)
                //.Include(sxm => sxm.mealId) //This step can be necessary if you can't grab the data
                .ToList();

            List<ScheduleDto> ScheduleDtos = new List<ScheduleDto> { };
            foreach (var sxm in SxMs)
            {
                ScheduleDto NewSchedule = new ScheduleDto
                {
                    ScheduleID = sxm.Schedule.ScheduleID,
                    StartDate = sxm.Schedule.StartDate,
                    EndDate = sxm.Schedule.EndDate
                };
                ScheduleDtos.Add(NewSchedule);

            }

            return Ok(ScheduleDtos);



        }
        /// <summary>
        /// Returns the Schedulexmeal data in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Schedulexmeal data in the database, including their associated Meals and foods data.
        /// </returns>
        /// <example>
        /// GET: api/SchedulexMealData/ListMenu
        /// </example>
        [HttpGet]
        [ResponseType(typeof(SchedulexMeal))]
        public IHttpActionResult ListMenu()
        {
            List<SchedulexMeal> SchedulexMeals = db.SchedulexMeals.ToList();
            List<SchedulexMealDto> schedulexMealdtos = new List<SchedulexMealDto>();

            SchedulexMeals.ForEach(a => schedulexMealdtos.Add(new SchedulexMealDto()
            {
                SchedulexMealId = a.SchedulexMealId,
                Day = a.Day,
                ScheduleId = a.Schedule.ScheduleID,
                StartDate = a.Schedule.StartDate,
                EndDate = a.Schedule.EndDate,
                MealId = a.Meal.MealID,
                Mealtype = a.Meal.Mealtype,
                FoodID = a.Food.FoodID,
                Foodtype = a.Food.Foodtype,
                ItemName = a.Food.ItemName,
                Vegetarian = a.Food.Vegetarian
            }));





            return Ok(schedulexMealdtos);
        }


        /// <summary>
        /// Returns all data in the Meal, Schedule and food table in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An Schedulexmealid in the system matching up to the Schedulexmealid ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Schedulexmeal bridging table</param>
        /// <example>
        /// GET: api/SchedulexMealData/FindMenu/5
        /// </example>
        [ResponseType(typeof(SchedulexMeal))]
        [HttpGet]
        public IHttpActionResult FindMenu(int id)
        {
            SchedulexMeal a = db.SchedulexMeals.Find(id);
            SchedulexMealDto SchedulexMealDto = new SchedulexMealDto()
            {
                SchedulexMealId = a.SchedulexMealId,
                Day = a.Day,
                ScheduleId = a.Schedule.ScheduleID,
                StartDate = a.Schedule.StartDate,
                EndDate = a.Schedule.EndDate,
                MealId = a.Meal.MealID,
                Mealtype = a.Meal.Mealtype,
                FoodID = a.Food.FoodID,
                Foodtype = a.Food.Foodtype,
                ItemName = a.Food.ItemName,
                Vegetarian = a.Food.Vegetarian
            };
            if (a == null)
            {
                return NotFound();
            }

            return Ok(SchedulexMealDto);
        }

        /// <summary>
        /// Updates a Bridging table data of a particular SchedulexMeal id in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the primary key</param>
        /// <param name="SchedulexMeal">JSON FORM DATA</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/SchedulexMealData/UpdateMenu/5
        /// FORM DATA: SchedulexMeal JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
  

        public IHttpActionResult UpdateMenu(int id, SchedulexMeal schedulexMeal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schedulexMeal.SchedulexMealId)
            {
                return BadRequest();
            }

            db.Entry(schedulexMeal).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchedulexMealExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        /// <summary>
        /// Deletes an SchedulexMeal relation from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the SchedulexMeal table </param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST:api/SchedulexMealData/DeleteMenu/5
        /// FORM DATA: (empty)
        /// </example>
     
        [ResponseType(typeof(SchedulexMeal))]
        [HttpPost]
        public IHttpActionResult DeleteMenu(int id)
        {
            SchedulexMeal schedulexMeal = db.SchedulexMeals.Find(id);
            if (schedulexMeal == null)
            {
                return NotFound();
            }

            db.SchedulexMeals.Remove(schedulexMeal);
            db.SaveChanges();

            return Ok(schedulexMeal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SchedulexMealExists(int id)
        {
            return db.SchedulexMeals.Count(e => e.SchedulexMealId == id) > 0;
        }
    }
}