using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Mit_Daten_arbeiten.Models
{
    public class TrainingsContext : DbContext
    {
        public TrainingsContext():base("name=TrainingsDb")
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Trainings> Trainings { get; set; }
    }
}