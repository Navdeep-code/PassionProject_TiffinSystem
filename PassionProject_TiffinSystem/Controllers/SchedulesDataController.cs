using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProject_TiffinSystem.Models;

namespace PassionProject_TiffinSystem.Controllers
{
    public class SchedulesDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns all Schedules in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Schedules in the database
        /// </returns>
        /// <example>
        /// GET: api/SchedulesData/ListSchedules
        /// </example>
   
        [HttpGet]
        [ResponseType(typeof(ScheduleDto))]

        public IEnumerable<ScheduleDto> ListSchedules()
        {
            List<Schedule> Schedules= db.Schedules.ToList();
            List<ScheduleDto> ScheduleDtos = new List<ScheduleDto>();

            Schedules.ForEach(s => ScheduleDtos.Add(new ScheduleDto()
            {
                ScheduleID = s.ScheduleID,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
   
            }));

            return ScheduleDtos;
        }
        /// <summary>
        /// Returns all schedule in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A schedule in the system matching up to the schedule ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Schedule</param>
        /// <example>
        /// GET: api/SchedulesData/FindSchedule/5
        /// </example>

        [ResponseType(typeof(Schedule))]
        [HttpGet]
        public IHttpActionResult FindSchedule(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            ScheduleDto ScheduleDto = new ScheduleDto() {
                ScheduleID = schedule.ScheduleID,
                StartDate = schedule.StartDate,
                EndDate = schedule.EndDate
            };

            if (schedule == null)
            {
                return NotFound();
            }

            return Ok(ScheduleDto);
        }

        /// <summary>
        /// Updates a particular schedule in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the schedule ID primary key</param>
        /// <param name="schedule">JSON FORM DATA of an Schedule</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/SchedulesData/UpdateSchedule/5
        /// FORM DATA:  JSON Object
        /// </example>

        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateSchedule(int id, Schedule schedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schedule.ScheduleID)
            {
                return BadRequest();
            }

            db.Entry(schedule).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(id))
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
        /// Adds an Schedule to the system
        /// </summary>
        /// <param name="schedule">JSON FORM DATA of an meal</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Schedule ID, Schedule Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/SchedulesData/AddSchedule
        /// FORM DATA: JSON Object
        /// </example>
   
        [ResponseType(typeof(Schedule))]
        [HttpPost]
        public IHttpActionResult AddSchedule(Schedule schedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Schedules.Add(schedule);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = schedule.ScheduleID }, schedule);
        }

        /// <summary>
        /// Deletes an Schedule from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Schedule</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        // POST: api/SchedulesData/DeleteSchedule/5
        /// FORM DATA: (empty)
        /// </example>

        [ResponseType(typeof(Schedule))]
        [HttpPost]
        public IHttpActionResult DeleteSchedule(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return NotFound();
            }

            db.Schedules.Remove(schedule);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ScheduleExists(int id)
        {
            return db.Schedules.Count(e => e.ScheduleID == id) > 0;
        }

    

    }
}