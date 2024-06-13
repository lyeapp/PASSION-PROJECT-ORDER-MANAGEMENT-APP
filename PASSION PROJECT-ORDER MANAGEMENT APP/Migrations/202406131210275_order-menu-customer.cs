namespace PASSION_PROJECT_ORDER_MANAGEMENT_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ordermenucustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Order_id = c.Int(nullable: false, identity: true),
                        Customer_id = c.Int(nullable: false),
                        Menu_id = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Location = c.String(),
                        Order_Date = c.DateTime(nullable: false),
                        Total_Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Order_id)
                .ForeignKey("dbo.Customers", t => t.Customer_id, cascadeDelete: true)
                .ForeignKey("dbo.Menus", t => t.Menu_id, cascadeDelete: true)
                .Index(t => t.Customer_id)
                .Index(t => t.Menu_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Menu_id", "dbo.Menus");
            DropForeignKey("dbo.Orders", "Customer_id", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "Menu_id" });
            DropIndex("dbo.Orders", new[] { "Customer_id" });
            DropTable("dbo.Orders");
        }
    }
}
