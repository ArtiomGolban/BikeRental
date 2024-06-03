namespace BikeRental.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAvailabilityFieldsFromBikesTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BikeDBTables", "AvailabilityStart");
            DropColumn("dbo.BikeDBTables", "AvailabilityEnd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BikeDBTables", "AvailabilityEnd", c => c.DateTime(nullable: false));
            AddColumn("dbo.BikeDBTables", "AvailabilityStart", c => c.DateTime(nullable: false));
        }
    }
}
