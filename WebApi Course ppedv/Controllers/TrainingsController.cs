﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Trainings.Core;
using System.Threading.Tasks;

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
        public async Task<IHttpActionResult> Get()
        {
            List<Training> trainings = await db.Trainings.Include(x => x.Category).ToListAsync();
            return Ok(trainings).Cached(Cacheability.Public, maxAge: TimeSpan.FromSeconds(20));
        }

        //GET api/Trainings/3
        public async Task<IHttpActionResult> Get(int id)
        {
            Training training = await db.Trainings.Include(x => x.Category).SingleOrDefaultAsync(t => t.Id == id);

            if (training == null)
                return NotFound();
            else
                return Ok(training);
        }

        //POST api/Trainings
        public async Task<IHttpActionResult> Post([FromBody]Training training)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Trainings.Add(training);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = training.Id }, training);
        }

        //PUT api/Trainings/3
        public async Task<IHttpActionResult> Put(int id, [FromBody] Training training)
        {
            if (id != training.Id || !ModelState.IsValid)
                return BadRequest(ModelState);


            db.Entry(training).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok();
        }


        //DELETE api/Trainings/3
        public async Task<IHttpActionResult> Delete(int id)
        {
            var training = db.Trainings.Find(id);

            if (training == null)
                return NotFound();


            db.Entry(training).State = EntityState.Deleted;
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
