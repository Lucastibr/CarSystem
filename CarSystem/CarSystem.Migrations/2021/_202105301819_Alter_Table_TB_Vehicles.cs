using System;
using FluentMigrator;

namespace CarSystem.Migrations._2021
{
    [Migration(202105301819, "Alter_Table_TB_Vehicles")]
    public class _202105301819_Alter_Table_TB_Vehicles: Migration
    {
        public override void Up()
        {
            Rename.Column("CarLicensePlate")
                .OnTable("TBVehicles").To("LicensePlate");

            Alter.Table("TBVehicles")
                .AddColumn("Price").AsDecimal().NotNullable().SetExistingRowsTo(false)
                .AddColumn("VehicleType").AsString(100).Nullable()
                .AddColumn("YearRelease").AsDateTime().NotNullable().SetExistingRowsTo(false)
                .AddColumn("CarBody").AsString(100).Nullable();
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
