using PassionProject_TiffinSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PassionProject_TiffinSystem.Controllers
{
    public class MealController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static MealController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44348/api/");
        }
        // GET: Meal
        public ActionResult List()
        {
            // GET: api/MealsData/ListMeals
            //curl https://localhost:44348/api/MealsData/ListMeals


            string url = "MealsData/ListMeals";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<MealDto> meal = response.Content.ReadAsAsync<IEnumerable<MealDto>>().Result;

           
            return View(meal);
        }

        // GET: Meal/Details/5
        public ActionResult Details(int id)
        {
            //the existing meal information
            string url = "MealsData/FindMeal/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            MealDto Selected = response.Content.ReadAsAsync<MealDto>().Result;


            return View(Selected);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Meal/Create
        public ActionResult New()
        {
            return View();
        }

        // POST: Meal/Create
        [HttpPost]
        public ActionResult Create(Meal meal)
        {
            Debug.WriteLine("the json payload is :");
            //objective: add a new meal into our system using the API
            //curl -H "Content-Type:application/json" -d @meal.json https://localhost:44348/api/MealsData/AddMeal
            string url = "MealsData/AddMeal";


            string jsonpayload = jss.Serialize(meal);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details");
            }
            else
            {
                return RedirectToAction("Error");
            }


        }

        // GET: Meal/Edit/5
        public ActionResult Edit(int id)
        { 
            //the existing meal information
            string url = "MealsData/FindMeal/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            MealDto Selected = response.Content.ReadAsAsync<MealDto>().Result;


            return View(Selected);
        }

        // POST: Meal/Update/5
        [HttpPost]
        public ActionResult Update(int id, Meal meal)
        {

            string url = "MealsData/UpdateMeal/" + id;
            string jsonpayload = jss.Serialize(meal);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Meal/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            //the existing meal information
            string url = "MealsData/FindMeal/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            MealDto Selected = response.Content.ReadAsAsync<MealDto>().Result;


            return View(Selected);
    
        }

        // POST: Meal/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {

            string url = "MealsData/DeleteMeal/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
