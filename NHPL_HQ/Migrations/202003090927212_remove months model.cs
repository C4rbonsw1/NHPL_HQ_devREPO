namespace NHPL_HQ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removemonthsmodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Files", "Months");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "Months", c => c.Int(nullable: false));
        }
    }
}
