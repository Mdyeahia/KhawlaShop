namespace Khawla.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubCategoryAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubCategoryName = c.String(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            AddColumn("dbo.Products", "SubCategoryId", c => c.Int());
            CreateIndex("dbo.Products", "SubCategoryId");
            AddForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "CategoryID", "dbo.Categories");
            DropIndex("dbo.SubCategories", new[] { "CategoryID" });
            DropIndex("dbo.Products", new[] { "SubCategoryId" });
            DropColumn("dbo.Products", "SubCategoryId");
            DropTable("dbo.SubCategories");
        }
    }
}
