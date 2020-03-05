namespace NHPL_HQ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDberror : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Practice_Id", c => c.Int());
            CreateIndex("dbo.Files", "Practice_Id");
            AddForeignKey("dbo.Files", "Practice_Id", "dbo.Practices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "Practice_Id", "dbo.Practices");
            DropIndex("dbo.Files", new[] { "Practice_Id" });
            DropColumn("dbo.Files", "Practice_Id");
        }
    }
}
