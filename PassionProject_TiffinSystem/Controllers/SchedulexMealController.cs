using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using System.Web.Script.Serialization;
using PassionProject_TiffinSystem.Models.ViewModels;
using PassionProject_TiffinSystem.Models;
using PassionProject_TiffinSystem.Migrations;

namespace PassionProject_TiffinSystem.Controllers
{
    public class SchedulexMealController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static SchedulexMealController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44348/api/");
        }
        // GET: SchedulexMeal/List
        public ActionResult List()
        {
            //objective: communicate with our SchedulexMeal data api 
            //curl https://localhost:44348/api/SchedulexMealData/ListMenu


            string url = "SchedulexMealData/ListMenu";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<SchedulexMealDto> sxm = response.Content.ReadAsAsync<IEnumerable<SchedulexMealDto>>().Result;
         

            return View(sxm);
        }

        // GET: SchedulexMeal/Details/5
        public ActionResult Details(int id)
        {



            //curl https://localhost:44348/api/SchedulexMealData/FindMenu/{id}

            string url = "SchedulexMealData/FindMenu/" + id;
                HttpResponseMessage response = client.GetAsync(url).Result;

 
            SchedulexMealDto Selected = response.Content.ReadAsAsync<SchedulexMealDto>().Result;


            return View(Selected);
        }

        // GET: SchedulexMeal/Create
        public ActionResult New()
        {
            AddMeal ViewModel = new AddMeal();

            string url = "MealsData/ListMeals";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<MealDto> MealType = response.Content.ReadAsAsync<IEnumerable<MealDto>>().Result;

            ViewModel.MealType = MealType;

            url = "SchedulesData/ListSchedules";
            response = client.GetAsync(url).Result;
            IEnumerable<ScheduleDto> SelectedSchedule = response.Content.ReadAsAsync<IEnumerable<ScheduleDto>>().Result;

            ViewModel.SelectedSchedule = SelectedSchedule;

            url = "FoodsData/ListFood";
            response = client.GetAsync(url).Result;

            IEnumerable<FoodDto> foodoptions = response.Content.ReadAsAsync<IEnumerable<FoodDto>>().Result;

            ViewModel.foodoptions = foodoptions;

            return View(ViewModel);
        }

        // POST: SchedulexMeal/Create
        [HttpPost]
        public ActionResult Create(SchedulexMeal schedulexmeal)
        {
            Debug.WriteLine("the json payload is :");


            //curl -H "Content-Type:application/json" -d @schedule.json https://localhost:44348/api/SchedulexMealData/AddMealToSchedule
            string url = "SchedulexMealData/AddMealToSchedule";


            string jsonpayload = jss.Serialize(schedulexmeal);
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

        // GET: SchedulexMeal/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateMenu ViewModel=new UpdateMenu();
       
            //curl https://localhost:44348/api/SchedulexMealData/FindMenu/{id}

            string url = "SchedulexMealData/FindMenu/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            SchedulexMealDto Selected = response.Content.ReadAsAsync<SchedulexMealDto>().Result;

            ViewModel.SelectedMenu = Selected;

            url = "MealsData/ListMeals";
            response = client.GetAsync(url).Result;
            IEnumerable<MealDto> MealType = response.Content.ReadAsAsync<IEnumerable<MealDto>>().Result;

            ViewModel.MealType = MealType;

            url = "SchedulesData/ListSchedules";
            response = client.GetAsync(url).Result;
            IEnumerable<ScheduleDto> SelectedSchedule = response.Content.ReadAsAsync<IEnumerable<ScheduleDto>>().Result;

            ViewModel.SelectedSchedule = SelectedSchedule;

            url = "FoodsData/ListFood";
            response = client.GetAsync(url).Result;

            IEnumerable<FoodDto> foodoptions = response.Content.ReadAsAsync<IEnumerable<FoodDto>>().Result;

            ViewModel.foodoptions = foodoptions;

            return View(ViewModel);
        }

        // POST: SchedulexMeal/Edit/5
        [HttpPost]
        public ActionResult Update(int id, SchedulexMeal schedulexMeal)
        {
            string url = "SchedulexMealData/UpdateMenu" + id;
            string jsonpayload = jss.Serialize(schedulexMeal);
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

        // GET: SchedulexMeal/Delete/5
        public ActionResult DeleteConfirm(int id)
        {

            string url = "SchedulexMealData/FindMenu/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;


            SchedulexMealDto Selected = response.Content.ReadAsAsync<SchedulexMealDto>().Result;


            return View(Selected);
        }

        public ActionResult Error()
        {

            return View();
        }

        // POST: SchedulexMeal/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "SchedulexMealData/DeleteMenu/" + id;
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
