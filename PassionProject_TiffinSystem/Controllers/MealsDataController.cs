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
using PassionProject_TiffinSystem.Migrations;
using PassionProject_TiffinSystem.Models;

namespace PassionProject_TiffinSystem.Controllers
{
    public class MealsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns all Meals in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Meals in the database
        /// </returns>
        /// <example>
        /// GET: api/MealsData/ListMeals
        /// </example>

        [HttpGet]
        public IEnumerable<MealDto> ListMeals()
        {
            List<Meal> Meals= db.Meals.ToList();
            List<MealDto> MealDtos = new List<MealDto>();

            Meals.ForEach(m => MealDtos.Add(new MealDto()
            {
                MealID = m.MealID,
                Mealtype = m.Mealtype,

            }));

            return MealDtos;
        }

        /// <summary>
        /// Returns all meals in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An meal in the system matching up to the meal ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the meal</param>
        /// <example>
        /// GET: api/MealData/FindMeal/5
        /// </example>
        [ResponseType(typeof(Meal))]
        [HttpGet]
        public IHttpActionResult FindMeal(int id)
        {
            Meal meal = db.Meals.Find(id);
            MealDto MealDto = new MealDto()
            {
                MealID = meal.MealID,
                Mealtype = meal.Mealtype,
   
            };

            if (meal == null)
            {
                return NotFound();
            }

            return Ok(MealDto);
        }
        /// <summary>
        /// Updates a particular meal in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the meal ID primary key</param>
        /// <param name="meal">JSON FORM DATA of an meal</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/MealsData/UpdateMeal/5
        /// FORM DATA:  JSON Object
        /// </example>

        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateMeal(int id, Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meal.MealID)
            {
                return BadRequest();
            }

            db.Entry(meal).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(id))
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
        /// Adds an Meal to the system
        /// </summary>
        /// <param name="meal">JSON FORM DATA of an meal</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: meal ID, meal Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        // POST: api/MealsData/AddMeal
        /// FORM DATA: JSON Object
        /// </example>

        [ResponseType(typeof(Meal))]
        [HttpPost]
        public IHttpActionResult AddMeal(Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Meals.Add(meal);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = meal.MealID }, meal);
        }
        /// <summary>
        /// Deletes an Meal from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Meal</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        // POST: api/MealsData/DeleteMeal/5
        /// FORM DATA: (empty)
        /// </example>

        [ResponseType(typeof(Meal))]
        [HttpPost]
        public IHttpActionResult DeleteMeal(int id)
        {
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return NotFound();
            }

            db.Meals.Remove(meal);
            db.SaveChanges();

            return Ok(meal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MealExists(int id)
        {
            return db.Meals.Count(e => e.MealID == id) > 0;
        }
    }
}