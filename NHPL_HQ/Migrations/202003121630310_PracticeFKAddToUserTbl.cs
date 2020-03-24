namespace NHPL_HQ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PracticeFKAddToUserTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PracticeFK_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "PracticeFK_Id");
            AddForeignKey("dbo.AspNetUsers", "PracticeFK_Id", "dbo.Practices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "PracticeFK_Id", "dbo.Practices");
            DropIndex("dbo.AspNetUsers", new[] { "PracticeFK_Id" });
            DropColumn("dbo.AspNetUsers", "PracticeFK_Id");
        }
    }
}
