namespace Trainings.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnnotationOnTraining : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trainings", "Name", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainings", "Name", c => c.String());
        }
    }
}
