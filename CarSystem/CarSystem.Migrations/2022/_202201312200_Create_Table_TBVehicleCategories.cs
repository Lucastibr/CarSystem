using System;
using CarSystem.Migrations.Helpers;
using FluentMigrator;

namespace CarSystem.Migrations._2022
{
    [Migration(02201312200, "Create_Table_TBVehicleCategories")]
    public class _202201312200_Create_Table_TBVehicleCategories: Migration
    {
        public override void Up()
        {
            Create.Table("TBVehicleCategories")
                .WithIdColumn()
                .WithColumn("Name").AsString(20).NotNullable();

            Execute.Sql(@"INSERT INTO TBVehicleCategories
                        (Id, Name) 
                        VALUES 
                        (NewId(), 'Sedan'),
                        (NewId(), 'Hatch'),
                        (NewId(), 'Picape'),
                        (NewId(), 'SUV')");

            Alter.Table("TBVehicles")
                .AddColumn("VehicleCategory_id")
                .AsGuid().Nullable()
                .ForeignKey("FK_TBVehicles_TBVehicleCategory_id", "TBVehicleCategories", "Id");
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
