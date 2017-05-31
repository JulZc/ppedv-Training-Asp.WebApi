using System.Data.Entity;

namespace Trainings.Core
{
    public class TrainingsContext : DbContext
    {
        public TrainingsContext():base("name=TrainingsDb")
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Training> Trainings { get; set; }
    }
}