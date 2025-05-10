using System;
using System.Data.Entity.Migrations;

public partial class InitialCreate : DbMigration
{
    public override void Up()
    {
        CreateTable(
            "dbo.Aptituds",
            c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Expediente = c.Int(nullable: false),
                    Proceso = c.String(maxLength: 2147483647),
                    Fecha = c.DateTime(nullable: false),
                    Carnet = c.String(maxLength: 2147483647),
                    Nombre = c.String(maxLength: 2147483647),
                    Direccion = c.String(maxLength: 2147483647),
                    Municipio = c.String(maxLength: 2147483647),
                    Provincia = c.String(maxLength: 2147483647),
                    DirTierra = c.String(maxLength: 2147483647),
                    MunTierra = c.String(maxLength: 2147483647),
                    NumCatastro = c.Int(nullable: false),
                    NumTenente = c.String(maxLength: 2147483647),
                    CoordX = c.String(maxLength: 2147483647),
                    CoordY = c.String(maxLength: 2147483647),
                    Zona = c.String(maxLength: 2147483647),
                    Parcelas = c.String(maxLength: 2147483647),
                    Area = c.Double(nullable: false, storeType: "real"),
                    FechaCatastro = c.DateTime(nullable: false),
                    Ejecutor = c.String(maxLength: 2147483647),
                    Linea = c.String(maxLength: 2147483647),
                    Formula = c.String(maxLength: 2147483647),
                    Tipo = c.String(maxLength: 2147483647),
                    Subtipo = c.String(maxLength: 2147483647),
                    Categoria = c.String(maxLength: 2147483647),
                    Profundidad = c.Int(nullable: false),
                    Salinidad = c.String(maxLength: 2147483647),
                    Rocosidad = c.String(maxLength: 2147483647),
                    Pedregosidad = c.String(maxLength: 2147483647),
                    Arroz = c.String(maxLength: 2147483647),
                    Cafe = c.String(maxLength: 2147483647),
                    Caña = c.String(maxLength: 2147483647),
                    Citrico = c.String(maxLength: 2147483647),
                    Frijoles = c.String(maxLength: 2147483647),
                    Papa = c.String(maxLength: 2147483647),
                    Pasto = c.String(maxLength: 2147483647),
                    Platano = c.String(maxLength: 2147483647),
                    Tabaco = c.String(maxLength: 2147483647),
                    Tomate = c.String(maxLength: 2147483647),
                })
            .PrimaryKey(t => t.Id);
        
        CreateTable(
            "dbo.Factores",
            c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Factor = c.String(maxLength: 2147483647),
                    Aptitud_Id = c.Int(),
                })
            .PrimaryKey(t => t.Id)
            .ForeignKey("dbo.Aptituds", t => t.Aptitud_Id)
            .Index(t => t.Aptitud_Id);
        
        CreateTable(
            "dbo.Medidas",
            c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Medida = c.String(maxLength: 2147483647),
                    Aptitud_Id = c.Int(),
                })
            .PrimaryKey(t => t.Id)
            .ForeignKey("dbo.Aptituds", t => t.Aptitud_Id)
            .Index(t => t.Aptitud_Id);
        
    }
    
    public override void Down()
    {
        DropForeignKey("dbo.Medidas", "Aptitud_Id", "dbo.Aptituds");
        DropForeignKey("dbo.Factores", "Aptitud_Id", "dbo.Aptituds");
        DropIndex("dbo.Medidas", new[] { "Aptitud_Id" });
        DropIndex("dbo.Factores", new[] { "Aptitud_Id" });
        DropTable("dbo.Medidas");
        DropTable("dbo.Factores");
        DropTable("dbo.Aptituds");
    }
}
