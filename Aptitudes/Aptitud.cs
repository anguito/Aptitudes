using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptitudes
{
    public class Factores
    {
        public int Id { get; set; }
        public string Factor { get; set; }
    }

    public class Medidas
    {
        public int Id { get; set; }
        public string Medida { get; set; }
    }
    public class Aptitud : Object
    {
        public Aptitud()
        {

        }
        public int Id { get; set; }
        public int Expediente { get; set; }
        public string Proceso { get; set; }
        public DateTime Fecha { get; set; }
        public string Carnet { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Municipio { get; set; }
        public string Provincia { get; set; }
        public string DirTierra { get; set; }
        public string MunTierra { get; set; }
        public int NumCatastro { get; set; }
        public string NumTenente { get; set; }
        public string CoordX { get; set; }
        public string CoordY { get; set; }
        public string Zona { get; set; }
        public string Parcelas { get; set; }
        public float Area { get; set; }
        public DateTime FechaCatastro { get; set; }
        public string Ejecutor { get; set; }
        public string Linea { get; set; }
        
        // Datos de suelo
        public string Formula { get; set; }
        public string Tipo { get; set; }
        public string Subtipo { get; set; }
        public List<Factores> Factores { get; set; }
        public string Categoria { get; set; }
        public int Profundidad { get; set; }
        public string Salinidad { get; set; }
        public string Rocosidad { get; set; }
        public string Pedregosidad { get; set; }
        public string Arroz { get; set; }
        public string Cafe { get; set;}
        public string Caña { get; set; }
        public string Citrico { get; set; }
        public string Frijoles { get; set; }
        public string Papa { get; set; }
        public string Pasto { get; set; }
        public string Platano { get; set; }
        public string Tabaco { get; set; }
        public string Tomate { get; set; }
        public List<Medidas> Medidas { get; set; }
    }
}
