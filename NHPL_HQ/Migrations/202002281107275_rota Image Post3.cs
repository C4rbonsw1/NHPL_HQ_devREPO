namespace NHPL_HQ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rotaImagePost3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "Practice_Id", "dbo.Practices");
            DropIndex("dbo.Files", new[] { "Practice_Id" });
            AddColumn("dbo.Files", "PracticeName", c => c.String());
            DropColumn("dbo.Files", "Practice_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "Practice_Id", c => c.Int());
            DropColumn("dbo.Files", "PracticeName");
            CreateIndex("dbo.Files", "Practice_Id");
            AddForeignKey("dbo.Files", "Practice_Id", "dbo.Practices", "Id");
        }
    }
}
