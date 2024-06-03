namespace BikeRental.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBikesandReservationsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BikeDBTables",
                c => new
                    {
                        BikeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        PricePerDay = c.Int(nullable: false),
                        PhotoUrl = c.String(),
                        AvailabilityEnd = c.DateTime(nullable: false),
                        AvailabilityStart = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.BikeId);

            CreateTable(
                "dbo.ReservationDBTables",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        BikeId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.UserDBTables", t => t.UserId_Id)
                .Index(t => t.UserId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationDBTables", "UserId_Id", "dbo.UserDBTables");
            DropIndex("dbo.ReservationDBTables", new[] { "UserId_Id" });
            DropTable("dbo.ReservationDBTables");
            DropTable("dbo.BikeDBTables");
        }
    }
}
