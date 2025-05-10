using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

public class XsdGenerator
{
    public static void GenerateXsdFromType(Type type, string outputPath)
    {
        // Crear el esquema XML
        XmlSchemas schemas = new XmlSchemas();
        XmlSchemaExporter exporter = new XmlSchemaExporter(schemas);

        // Configurar el mapeo
        XmlReflectionImporter importer = new XmlReflectionImporter();
        XmlTypeMapping mapping = importer.ImportTypeMapping(type);

        // Exportar el tipo
        exporter.ExportTypeMapping(mapping);

        // Escribir el esquema a archivo
        using (var writer = new StreamWriter(outputPath))
        using (var xmlWriter = XmlWriter.Create(writer))
        {
            schemas[0].Write(xmlWriter);
        }
    }
}