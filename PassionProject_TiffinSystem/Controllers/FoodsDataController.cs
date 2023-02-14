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
    public class FoodsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/FoodsData/ListFood
        [HttpGet]
        public IEnumerable<FoodDto> ListFood()
        {
            List<Food> food = db.food.ToList();
            List<FoodDto> FoodDtos = new List<FoodDto>();

            food.ForEach(f => FoodDtos.Add(new FoodDto()
            {
                FoodID = f.FoodID,
                Foodtype = f.Foodtype,
                ItemName = f.ItemName,
                Vegetarian=f.Vegetarian

            }));

            return FoodDtos;
        }

        // GET: api/FoodsData/FindFood/5
        [ResponseType(typeof(Food))]
        [HttpGet]
        public IHttpActionResult FindFood(int id)
        {
            Food food = db.food.Find(id);
            FoodDto FoodDto = new FoodDto()
            {
                FoodID = food.FoodID,
                Foodtype = food.Foodtype,
                ItemName = food.ItemName,
                Vegetarian = food.Vegetarian

            };
            if (food == null)
            {
                return NotFound();
            }

            return Ok(FoodDto);
        }

        // PUT: api/FoodsData/UpdateFood/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateFood(int id, Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != food.FoodID)
            {
                return BadRequest();
            }

            db.Entry(food).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
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

        // POST: api/FoodsData/AddFood
        [ResponseType(typeof(Food))]
        [HttpPost]
        public IHttpActionResult AddFood(Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.food.Add(food);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = food.FoodID }, food);
        }

        // DELETE: api/FoodsData/DeleteFood/5
        [ResponseType(typeof(Food))]
        [HttpPost]
        public IHttpActionResult DeleteFood(int id)
        {
            Food food = db.food.Find(id);
            if (food == null)
            {
                return NotFound();
            }

            db.food.Remove(food);
            db.SaveChanges();

            return Ok(food);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FoodExists(int id)
        {
            return db.food.Count(e => e.FoodID == id) > 0;
        }
    }
}