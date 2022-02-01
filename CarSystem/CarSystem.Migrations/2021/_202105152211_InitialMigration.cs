using System;
using CarSystem.Migrations.Helpers;
using FluentMigrator;

namespace CarSystem.Migrations._2021
{
    [Migration(202105152211, "InitialMigration")]
    public class _202105152211_InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table("TBEnterprises")
                .WithIdColumn()
                .WithColumn("CompanyName").AsString(100).NotNullable()
                .WithColumn("Cnpj").AsString(14).NotNullable().Unique();

            Create.Table("TBVehicles")
                .WithIdColumn()
                .WithColumn("CarLicensePlate").AsString(7).NotNullable().Unique()
                .WithColumn("Chassis").AsString(17).NotNullable().Unique()
                .WithColumn("Enterprise_Id").AsGuid().Nullable()
                .ForeignKey("FK_TBVehicles_TBEnterprise_id", "TBEnterprises", "Id");
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }
    }
}
