using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Aptitudes
{
    public class Estado
    {
        private static string ruta = ".\\estado.json";
        public static List<string> Ejecutores { get; set; } = new List<string>();
        public static void LoadList()
        {
            if (File.Exists(ruta))
            {
                Ejecutores = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(ruta));
            }
        }

        public static void GuardarLista()
        {
            if (Ejecutores.Count != 0)
            {
                string json = JsonConvert.SerializeObject(Ejecutores, Formatting.Indented);
                File.WriteAllText(ruta, json);
            }
        }
    }
}
