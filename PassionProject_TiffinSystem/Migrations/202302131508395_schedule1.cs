namespace PassionProject_TiffinSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schedule1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ScheduleID = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ScheduleID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Schedules");
        }
    }
}
