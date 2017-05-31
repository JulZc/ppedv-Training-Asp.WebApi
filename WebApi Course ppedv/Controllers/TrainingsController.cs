using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Trainings.Core;
using System.Threading.Tasks;
using System.Web.Http.OData;
using System.Web.Http.Description;

namespace WebApi_Course_ppedv.Controllers
{
    /// <summary>
    /// Trainings API Documentation: 
    /// Crud-Methods for all Entries. Entries are public 
    /// and queryable but modifying Data  
    /// is just for authenticated users. 
    /// </summary>
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
        [EnableQuery]
        [ResponseType(typeof(List<Training>))]
        public async Task<IHttpActionResult> Get()
        {
            List<Training> trainings = await db.Trainings.Include(x => x.Category).ToListAsync();
            return Ok(trainings).Cached(Cacheability.Public, maxAge: TimeSpan.FromSeconds(20));
        }

        //GET api/Trainings/3
        [ResponseType(typeof(Training))]
        public async Task<IHttpActionResult> Get(int id)
        {
            Training training = await db.Trainings.Include(x => x.Category).SingleOrDefaultAsync(t => t.Id == id);

            if (training == null)
                return NotFound();
            else
                return Ok(training);
        }

        [ResponseType(typeof(List<Training>))]
        [Route("api/category/{id:int}/trainings")]
        public async Task<IHttpActionResult> GetByGenre(int id)
        {
            List<Training> trainings = await db.Trainings.Include(t => t.Category).Where(t => t.CategoryId == id).ToListAsync();

            if (!trainings.Any())
                return BadRequest();

            return Ok(trainings);

        }

        [ResponseType(typeof(List<Training>))]
        [Route("api/category/{name:alpha}/trainings")]
        public async Task<IHttpActionResult> GetByGenre(string name)
        {
            List<Training> trainings = await db.Trainings.Include(t => t.Category).Where(t => t.Category.Name == name).ToListAsync();

            if (!trainings.Any())
                return BadRequest();

            return Ok(trainings);

        }


        //POST api/Trainings
        [Authorize]
        [ResponseType(typeof(Training))]
        public async Task<IHttpActionResult> Post([FromBody]Training training)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Trainings.Add(training);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = training.Id }, training);
        }

        //PUT api/Trainings/3
        [Authorize]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Put(int id, [FromBody] Training training)
        {
            if (id != training.Id || !ModelState.IsValid)
                return BadRequest(ModelState);


            db.Entry(training).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok();
        }


        //DELETE api/Trainings/3
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(void))]
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
