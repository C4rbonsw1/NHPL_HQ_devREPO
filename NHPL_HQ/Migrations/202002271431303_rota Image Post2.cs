namespace NHPL_HQ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rotaImagePost2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "Employee_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Files", new[] { "Employee_Id" });
            AddColumn("dbo.Files", "Practice_Id", c => c.Int());
            CreateIndex("dbo.Files", "Practice_Id");
            AddForeignKey("dbo.Files", "Practice_Id", "dbo.Practices", "Id");
            DropColumn("dbo.Files", "Employee_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "Employee_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Files", "Practice_Id", "dbo.Practices");
            DropIndex("dbo.Files", new[] { "Practice_Id" });
            DropColumn("dbo.Files", "Practice_Id");
            CreateIndex("dbo.Files", "Employee_Id");
            AddForeignKey("dbo.Files", "Employee_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
