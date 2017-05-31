using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Trainings.Core;

namespace WebApi_Course_ppedv.Controllers
{
    public class TrainingsController : ApiController
    {
        private readonly TrainingsContext db;

        public TrainingsController()
        {
            db = new TrainingsContext();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }




        //GET api/Trainings
        public IHttpActionResult Get()
        {
            List<Training> trainings = db.Trainings.Include(x => x.Category).ToList();
            return Ok(trainings).Cached(Cacheability.Public, maxAge:TimeSpan.FromSeconds(20) );
        }

        //GET api/Trainings/3
        public IHttpActionResult Get(int id)
        {
            Training training = db.Trainings.Include(x => x.Category).SingleOrDefault(t => t.Id == id);

            if (training == null)
                return NotFound();
            else
                return Ok(training);
        }

        //POST api/Trainings
        public IHttpActionResult Post([FromBody]Training training)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            db.Trainings.Add(training);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = training.Id }, training);
        }

        //PUT api/Trainings/3
        public IHttpActionResult Put(int id, [FromBody] Training training)
        {
            if (id != training.Id)
            {
                return BadRequest();
            }

            db.Entry(training).State = EntityState.Modified;
            db.SaveChanges();

            return Ok();
        }


        //DELETE api/Trainings/3
        public IHttpActionResult Delete(int id)
        {
            var training = db.Trainings.Find(id);

            if (training == null)
                return NotFound();


            db.Entry(training).State = EntityState.Deleted;
            db.SaveChanges();
            return Ok();
        }
    }
}
