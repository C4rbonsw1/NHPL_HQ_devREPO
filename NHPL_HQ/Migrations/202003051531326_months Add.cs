namespace NHPL_HQ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class monthsAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Months", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "Months");
        }
    }
}
