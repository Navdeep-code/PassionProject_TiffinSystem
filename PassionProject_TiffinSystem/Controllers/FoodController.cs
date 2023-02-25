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
    public class FoodController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static FoodController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44348/api/");
        }
        // GET: Food
        public ActionResult List()
        {
            // GET: api//FoodsData/ListFood
            //curl https://localhost:44348/api/FoodsData/ListFood


            string url = "FoodsData/ListFood";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<FoodDto> food = response.Content.ReadAsAsync<IEnumerable<FoodDto>>().Result;

            return View(food);
        }

        // GET: Food/Details/5
        public ActionResult Detail(int id)
        {   
            //the existing food information
            string url = "FoodsData/FindFood/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            FoodDto Selected = response.Content.ReadAsAsync<FoodDto>().Result;


            return View(Selected);
        }

        public ActionResult Error()
        {

            return View();
        }
        // GET: Food/Create
        public ActionResult New()
        {
            return View();
        }

        // POST: Food/Create
        [HttpPost]
        public ActionResult Create(Food food)
        {
            Debug.WriteLine("the json payload is :");
            //objective: add a new food into our system using the API
            //curl -H "Content-Type:application/json" -d @food.json https://localhost:44348/api/FoodsData/AddFood
            string url = "FoodsData/AddFood";


            string jsonpayload = jss.Serialize(food);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
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

        // GET: Food/Edit/5
        public ActionResult Edit(int id)
        {
            //the existing food information
            string url = "FoodsData/FindFood/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            FoodDto Selected = response.Content.ReadAsAsync<FoodDto>().Result;


            return View(Selected); 
        }

        // POST: Food/Update/5
        [HttpPost]
        public ActionResult Update(int id, Food food)
        {

            string url = "FoodsData/UpdateFood/" + id;
            string jsonpayload = jss.Serialize(food);

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

        // GET: Food/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            //the existing food information
            string url = "FoodsData/FindFood/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            FoodDto Selected = response.Content.ReadAsAsync<FoodDto>().Result;


            return View(Selected);
        }

        // POST: Food/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {

            string url = "FoodsData/DeleteFood/" + id;
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
