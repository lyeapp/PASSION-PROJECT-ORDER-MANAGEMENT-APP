namespace PASSION_PROJECT_ORDER_MANAGEMENT_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Customer_id = c.Int(nullable: false, identity: true),
                        Customer_Name = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        Address = c.String(),
                        Phone_no = c.Int(nullable: false),
                        Email = c.String(),
                        Registration_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Customer_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
