using System;
using CarSystem.Migrations.Helpers;
using FluentMigrator;

namespace CarSystem.Migrations
{
    [Migration(202105302059, "Alter_Table_TB_Vehicles_Add_Image")]
    public class __202105302059_Alter_Table_TB_Vehicles_Add_Image: Migration
    {
        public override void Up()
        {
            Alter.Table("TBVehicles")
                .AddColumn("Image").AsString(5000).Nullable().SetExistingRowsTo(false);
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
