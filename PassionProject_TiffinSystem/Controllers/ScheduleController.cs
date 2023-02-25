using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using System.Web.Script.Serialization;
using PassionProject_TiffinSystem.Models;


namespace PassionProject_TiffinSystem.Controllers
{
    public class ScheduleController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static ScheduleController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44348/api/");
        }

        // GET: Schedule/List
        public ActionResult List()
        {
            // api/SchedulesData/ListSchedules
            //curl https://localhost:44348//api/SchedulesData/ListSchedules


            string url = "SchedulesData/ListSchedules";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<ScheduleDto> schedules = response.Content.ReadAsAsync<IEnumerable<ScheduleDto>>().Result;

            return View(schedules);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Schedule/New
        public ActionResult New()
        {
            return View();
        }


        // POST: Schedule/Create
        [HttpPost]
        public ActionResult Create(Schedule schedule)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(schedule.StartDate);
            //objective: add a new schdeule into our system using the API
            //curl -H "Content-Type:application/json" -d @schedule.json https://localhost:44348/api/SchedulesData/AddSchedule
            string url = "SchedulesData/AddSchedule";


            string jsonpayload = jss.Serialize(schedule);
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

        // GET: Schedule/Edit/5
        public ActionResult Edit(int id)
        {
           

            //the existing schedule information
            string url = "SchedulesData/FindSchedule/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ScheduleDto Selected = response.Content.ReadAsAsync<ScheduleDto>().Result;
         

            return View(Selected);
        }

        // POST: Schedule/Update/5
        [HttpPost]
        public ActionResult Update(int id, Schedule schedule)
        {

            string url = "SchedulesData/UpdateSchedule/" + id;
            string jsonpayload = jss.Serialize(schedule);

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

        // GET: Schedule/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            //the existing schedule information
            string url = "SchedulesData/FindSchedule/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            ScheduleDto Selected = response.Content.ReadAsAsync<ScheduleDto>().Result;


            return View(Selected);
        }

        // POST: api/SchedulesData/DeleteSchedule/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "SchedulesData/DeleteSchedule/" + id;
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