namespace BikeRental.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBalanceFieldToUsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDBTables", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserDBTables", "Balance");
        }
    }
}
