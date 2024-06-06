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
                        PhotoUrl = c.String()
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
                        UserId = c.Int(nullable: false),
                        Paid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationId)
                .Index(t => t.UserId)
                .Index(t => t.BikeId);
        }
        
        public override void Down()
        {
            DropIndex("dbo.ReservationDBTables", new[] { "BikeId" });
            DropIndex("dbo.ReservationDBTables", new[] { "UserId" });

            DropTable("dbo.ReservationDBTables");
            DropTable("dbo.BikeDBTables");
        }
    }
}
