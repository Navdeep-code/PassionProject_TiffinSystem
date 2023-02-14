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
    public class SchedulesDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SchedulesData/ListSchedules
        [HttpGet]

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

        // GET: api/SchedulesData/FindSchedule/5
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

        // POST: api/SchedulesData/UpdateSchedule/5
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

        // POST: api/SchedulesData/AddSchedule
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

        // POST: api/SchedulesData/DeleteSchedule/5
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