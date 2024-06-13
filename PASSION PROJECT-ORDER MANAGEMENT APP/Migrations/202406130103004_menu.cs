namespace PASSION_PROJECT_ORDER_MANAGEMENT_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class menu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Menu_id = c.Int(nullable: false, identity: true),
                        Menu_Name = c.String(),
                        Menu_Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Menu_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Menus");
        }
    }
}
