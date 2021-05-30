using FluentMigrator.Builders.Create.Table;

namespace CarSystem.Migrations.Helpers
{
    internal static class MigrationExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn("Id")
                .AsGuid()
                .PrimaryKey();
        }
    }
}
