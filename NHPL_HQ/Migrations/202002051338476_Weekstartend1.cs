namespace NHPL_HQ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Weekstartend1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shifts", "WeekStart", c => c.DateTime(nullable: false));
            AddColumn("dbo.Shifts", "WeekEnd", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shifts", "WeekEnd");
            DropColumn("dbo.Shifts", "WeekStart");
        }
    }
}
