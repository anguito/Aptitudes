using Aptitudes;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SQLite.EF6.Migrations;
using System.Linq;

internal sealed class Configuration : DbMigrationsConfiguration<AptitudesContext>
{
    public Configuration()
    {
        AutomaticMigrationsEnabled = false;
        SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
    }
}
