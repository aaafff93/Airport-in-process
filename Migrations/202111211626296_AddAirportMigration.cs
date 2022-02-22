namespace Airport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAirportMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aircraft",
                c => new
                    {
                        AircraftID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        TailNumber = c.String(nullable: false, maxLength: 15),
                        MaximumCapacity = c.Short(nullable: false),
                        Airlane = c.String(nullable: false, maxLength: 25),
                        Image = c.Binary(storeType: "image"),
                    })
                .PrimaryKey(t => t.AircraftID);
            
            CreateTable(
                "dbo.Flight",
                c => new
                    {
                        FlightID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        Arrival = c.String(nullable: false, maxLength: 50),
                        Departure = c.String(nullable: false, maxLength: 50),
                        BoardingTime = c.Time(nullable: false, precision: 7),
                        LastCallTime = c.Time(nullable: false, precision: 7),
                        OutTime = c.Time(nullable: false, precision: 7),
                        ArrivalTime = c.Time(nullable: false, precision: 7),
                        AvailableSeats = c.Short(nullable: false),
                        SoldSeats = c.Short(nullable: false),
                        AircraftID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FlightID)
                .ForeignKey("dbo.Aircraft", t => t.AircraftID, cascadeDelete: true)
                .Index(t => t.AircraftID);
            
            CreateTable(
                "dbo.BoardingPass",
                c => new
                    {
                        BoardingPassID = c.Int(nullable: false),
                        PassengerName = c.String(nullable: false, maxLength: 50),
                        Passport = c.String(nullable: false, maxLength: 20),
                        Seat = c.String(nullable: false, maxLength: 15),
                        FlightID = c.Int(nullable: false),
                        ClassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BoardingPassID)
                .ForeignKey("dbo.Class", t => t.ClassID)
                .ForeignKey("dbo.Flight", t => t.FlightID, cascadeDelete: true)
                .Index(t => t.FlightID)
                .Index(t => t.ClassID);
            
            CreateTable(
                "dbo.Class",
                c => new
                    {
                        ClassID = c.Int(nullable: false),
                        ClassName = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.ClassID);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoardingPass", "FlightID", "dbo.Flight");
            DropForeignKey("dbo.BoardingPass", "ClassID", "dbo.Class");
            DropForeignKey("dbo.Flight", "AircraftID", "dbo.Aircraft");
            DropIndex("dbo.BoardingPass", new[] { "ClassID" });
            DropIndex("dbo.BoardingPass", new[] { "FlightID" });
            DropIndex("dbo.Flight", new[] { "AircraftID" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Class");
            DropTable("dbo.BoardingPass");
            DropTable("dbo.Flight");
            DropTable("dbo.Aircraft");
        }
    }
}
