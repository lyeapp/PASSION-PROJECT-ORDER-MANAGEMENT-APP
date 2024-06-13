﻿namespace PASSION_PROJECT_ORDER_MANAGEMENT_APP.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PASSION_PROJECT_ORDER_MANAGEMENT_APP.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PASSION_PROJECT_ORDER_MANAGEMENT_APP.Models.ApplicationDbContext";
        }

        protected override void Seed(PASSION_PROJECT_ORDER_MANAGEMENT_APP.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
