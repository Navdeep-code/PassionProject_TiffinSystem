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

        // GET: api/MealsData/ListMeals
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

        // GET: api/MealsData/FindMeal/5
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

        // PUT: api/MealsData/UpdateMeal/5
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

        // POST: api/MealsData/AddMeal
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

        // DELETE: api/MealsData/DeleteMeal/5
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