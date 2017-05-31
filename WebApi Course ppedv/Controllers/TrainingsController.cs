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
        //GET api/Trainings
        public IEnumerable<Training> Get()
        {
            using (var db = new TrainingsContext())
            {
                return db.Trainings.Include(x => x.Category).ToList();
            }
        }

        //GET api/Trainings/3
        public Training Get(int id)
        {
            using (var db = new TrainingsContext())
            {
                return db.Trainings.Include(x => x.Category).SingleOrDefault(t => t.Id == id);
            }
        }

        //POST api/Trainings
        public void Post([FromBody]Training training)
        {
            using (var db = new TrainingsContext())
            {
                db.Trainings.Add(training);
                db.SaveChanges();
            }
        }

        //PUT api/Trainings/3
        public void Put(int id, [FromBody] Training training)
        {
            if(id != training.Id)
            {
                return;
            }

            using (var db = new TrainingsContext())
            {
                db.Entry(training).State = EntityState.Modified;
                db.SaveChanges();
            }
        }


        //DELETE api/Trainings/3
        public void Delete(int id)
        {
            using (var db = new TrainingsContext())
            {
                var training = db.Trainings.Find(id);

                if(training != null)
                {
                    db.Entry(training).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }
    }
}
