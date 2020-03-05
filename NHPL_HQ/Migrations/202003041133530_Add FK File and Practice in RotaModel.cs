namespace NHPL_HQ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFKFileandPracticeinRotaModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rotas", "File_FileId", c => c.Int());
            AddColumn("dbo.Rotas", "Practice_Id", c => c.Int());
            CreateIndex("dbo.Rotas", "File_FileId");
            CreateIndex("dbo.Rotas", "Practice_Id");
            AddForeignKey("dbo.Rotas", "File_FileId", "dbo.Files", "FileId");
            AddForeignKey("dbo.Rotas", "Practice_Id", "dbo.Practices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rotas", "Practice_Id", "dbo.Practices");
            DropForeignKey("dbo.Rotas", "File_FileId", "dbo.Files");
            DropIndex("dbo.Rotas", new[] { "Practice_Id" });
            DropIndex("dbo.Rotas", new[] { "File_FileId" });
            DropColumn("dbo.Rotas", "Practice_Id");
            DropColumn("dbo.Rotas", "File_FileId");
        }
    }
}
