using OSGeo.OGR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aptitudes
{
    public class InfoSuelo
    {
        public KeyValuePair<string, string> Tipo { get; set; }
        public KeyValuePair<string, string> Subtipo { get; set; }
        public KeyValuePair<string, string> Salinidad { get; set; }
        public KeyValuePair<string, string> Pedregosidad { get; set; }
        public KeyValuePair<string, string> Rocosidad { get; set; }
        public string Profundidad { get; set; }
        public string Categoria { get; set; }
        public string Arroz { get; set; }
        public string Cafe { get; set; }
        public string Caña { get; set; }
        public string Citrico { get; set; }
        public string Frijol { get; set; }
        public string Papa { get; set; }
        public string Pasto { get; set; }
        public string Platano { get; set; }
        public string Tabaco { get; set; }
        public string Tomate { get; set; }
        public Dictionary<int, string> Limitantes { get; set; }
        public Dictionary<int, string> Medidas { get; set; }
    }

    public class InfoGeografica
    {
        private readonly Dictionary<string, string> tipos = new Dictionary<string, string>()
        {
            {"I", "Ferrítico Púrpura" },
            {"II", "Ferralítico Rojo" },
            {"III", "Ferralítico Rojo Lixiviado" },
            {"IV", "Ferralítico Amarillento" },
            {"V", "Ferralítico Cuarcítico Amarillo Lixiviado" },
            {"VI", "Ferralítico Cuarcítico Amarillo Rojizo Lixiviado" },
            {"VII","Fersialítico Rojo Parduzco Ferromagnesial" },
            {"VIII", "Fersialítico Pardo Rojizo" },
            {"IX", "Pardo sin Carbonatos" },
            {"X", "Pardo con Carbonatos" },
            {"XI", "Pardo Grisáceo" },
            {"XII", "Húmico Carbonático" },
            {"XIII", "Rendzina Roja" },
            {"XIV", "Rendzina Negra" },
            {"XV", "Oscuro Plástico Gleyzado" },
            {"XVI", "Oscuro Plástico Gleyzoso" },
            {"XVII", "Oscuro Plástico no Gleyzado" },
            {"XVIII", "Gley Húmivo" },
            {"XIX", "Gley Ferralítico" },
            {"XX", "Gley Amarillento Cuarcítico" },
            {"XXI", "Húmico Marga" },
            {"XXII", "Pantanoso" },
            {"XXIII", "Solonchak Mangle" },
            {"XXIV", "Solonchak" },
            {"XXV", "Solonets" },
            {"XXVI", "Aluvial" },
            {"XXVII", "Arenoso Cuarcítico" },
            {"XXVIII", "Esquelético" }
        };

        private readonly Dictionary<string, string> subtipos = new Dictionary<string, string>()
        {
            {"A", "Típico" },
            {"B", "Concrecionario" },
            {"C", "Laterizado" },
            {"D", "Hidratado" },
            {"E", "Lixiviado" },
            {"F", "Gleyzoso" },
            {"G", "Gleyzado" },
            {"H", "Plastogénico" },
            {"J", "Negro" },
            {"K", "Negro Grisáceo" },
            {"L", "Gris" },
            {"M", "Gris Amarillento" },
            {"N", "Pardo Oscuro" },
            {"O", "Estratificado" },
            {"P", "Turboso" },
            {"Q", "Turba" },
            {"R", "Mineral" },
            {"S", "Poco Diferenciado" },
            {"T", "Diferenciado" },
            {"U", "Natural" },
            {"V", "Antrópico" },
            {"W", "Compactado" },
            {"X", "Lavado" },
            {"Y", "Poco Lixiviado" },
            {"Z", "Humificado" }
        };

        private readonly Dictionary<string, string> salinidad = new Dictionary<string, string>()
        {
            {"s¹", "Muy fuertemente salino" },
            {"s²", "Fuertemente salino" },
            {"s³", "Medianamente salino" },
            {"s⁴", "Débilmente salino"}
        };

        private readonly Dictionary<string, string> pedregosidad = new Dictionary<string, string>()
        {
            {"w₁", "Excesiva" },
            {"w₂", "Muy pedregoso" },
            {"w₃", "Pedregoso" },
            {"w₄", "Moderadamente Pedregoso"}
        };

        private readonly Dictionary<string, string> rocosidad = new Dictionary<string, string>()
        {
            {"z₁", "Extremadamente rocoso" },
            {"z₂", "Muy rocoso" },
            {"z₃", "Rocoso" },
            {"z₄", "Moderadamente rocoso"}
        };

        private readonly Dictionary<int, string> factores = new Dictionary<int, string>()
        {
            {1, "Baja fertilidad natural" },
            {2, "Presenta compactacion" },
            {3, "Baja Retencion de humedad" },
            {4, "Drenaje deficiente" },
            {5, "Plasticos" },
            {6, "Bajo contenido de materia organica" },
            {7, "Resecantes" },
            {8, "Presencia de carbonatos" },
            {9, "Pobre estructura" }
        };

        private readonly Dictionary<int, string> medidas = new Dictionary<int, string>()
        {
            {1, "Incorporar restos de cosecha y abonos verdes" },
            {2, "Aplicar materia organica" },
            {3, "Subsolacion" },
            {4, "Descompactacion" },
            {5, "Limpieza de canales o hacer zanjas de desague" },
            {6, "Nunca aplicar materia organica" },
            {7, "Sembrar especies tolerantes a la salinidad" },
            {8, "Realizar labores de suelo para evitar encharcamiento" },
            {9, "Recogida de piedras durante la preparacion del suelo" }
        };

        public struct CondicionFactores
        {
            public int factor { get; set; }
            public string condicion { get; set; }
        }

        Dictionary<int, List<int>> FactoresMedidas = new Dictionary<int, List<int>>()
        {
            [1] = new List<int>() { 1, 2 },
            [2] = new List<int>() { 3 },
            [4] = new List<int>() { 5 },
            [5] = new List<int>() { 3 },
            [8] = new List<int>() { 1, 6 },
            [9] = new List<int>() { 2 }
        };

        Dictionary<string, List<CondicionFactores>> asignacionFactores = new Dictionary<string, List<CondicionFactores>>()
        {
            ["I"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 1, condicion = "" },
                            new CondicionFactores { factor = 2, condicion = "" },
                            new CondicionFactores { factor = 3, condicion = "" }
                        },
            ["II"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 1, condicion = "" },
                            new CondicionFactores { factor = 2, condicion = "" },
                            new CondicionFactores { factor = 3, condicion = "-D" },
                            new CondicionFactores { factor = 4, condicion = "+D"}
                        },
            ["III"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 1, condicion = "" },
                            new CondicionFactores { factor = 2, condicion = "" },
                            new CondicionFactores { factor = 3, condicion = "" }
                        },
            ["IV"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 1, condicion = "" },
                            new CondicionFactores { factor = 2, condicion = "" },
                            new CondicionFactores { factor = 4, condicion = "" },
                            new CondicionFactores { factor = 5, condicion = "" }
                        },
            ["V"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 1, condicion = "" },
                            new CondicionFactores { factor = 2, condicion = "" },
                            new CondicionFactores { factor = 3, condicion = "-F" },
                            new CondicionFactores { factor = 4, condicion = "+F" },
                            new CondicionFactores { factor = 6, condicion = "" },
                            new CondicionFactores { factor = 7, condicion = "" }
                        },
            ["VI"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 1, condicion = "" },
                            new CondicionFactores { factor = 2, condicion = "" },
                            new CondicionFactores { factor = 3, condicion = "+A,+C,+F,+Z,+B" },
                        },
            ["VII"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 1, condicion = "" },
                            new CondicionFactores { factor = 2, condicion = "" },
                            new CondicionFactores { factor = 3, condicion = "" },
                        },
            ["VIII"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 8, condicion = "" }
                        },
            ["IX"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 4, condicion = "+F,+H" }
                        },
            ["X"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 8, condicion = "" },
                            new CondicionFactores { factor = 4, condicion = "+F,+H" },
                            new CondicionFactores { factor = 5, condicion = "-A" },
                            new CondicionFactores { factor = 2, condicion = "+F,+H" }
                        },
            ["XI"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 1, condicion = "" },
                            new CondicionFactores { factor = 3, condicion = "" },
                        },
            ["XII"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 8, condicion = "" },
                            new CondicionFactores { factor = 4, condicion = "+F,+H" },
                            new CondicionFactores { factor = 5, condicion = "-A" },
                            new CondicionFactores { factor = 2, condicion = "+F,+H" }
                        },
            ["XIII"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 8, condicion = "" }
                        },
            ["XIV"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 8, condicion = "" }
                        },
            ["XV"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 9, condicion = "" },
                            new CondicionFactores { factor = 2, condicion = "" },
                            new CondicionFactores { factor = 4, condicion = "" },
                            new CondicionFactores { factor = 5, condicion = "" }
                        },
            ["XVI"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 9, condicion = "" },
                            new CondicionFactores { factor = 2, condicion = "" },
                            new CondicionFactores { factor = 4, condicion = "" },
                            new CondicionFactores { factor = 5, condicion = "" }
                        },
            ["XVII"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 9, condicion = "" },
                            new CondicionFactores { factor = 2, condicion = "" },
                            new CondicionFactores { factor = 4, condicion = "" },
                            new CondicionFactores { factor = 5, condicion = "" }
                        },
            ["XVIII"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 9, condicion = "" },
                            new CondicionFactores { factor = 2, condicion = "" },
                            new CondicionFactores { factor = 4, condicion = "" },
                            new CondicionFactores { factor = 5, condicion = "" }
                        },
            ["XIX"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 1, condicion = "" },
                            new CondicionFactores { factor = 2, condicion = "" },
                            new CondicionFactores { factor = 4, condicion = "" },
                            new CondicionFactores { factor = 9, condicion = "" }
                        },
            ["XX"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 1, condicion = "" },
                            new CondicionFactores { factor = 2, condicion = "" },
                            new CondicionFactores { factor = 4, condicion = "" },
                            new CondicionFactores { factor = 9, condicion = "" }
                        },
            ["XXI"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 4, condicion = "" },
                            new CondicionFactores { factor = 8, condicion = "" },
                        },
            ["XXII"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 4, condicion = "" },
                        },
            ["XXIII"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 1, condicion = "" },
                            new CondicionFactores { factor = 4, condicion = "" },
                            new CondicionFactores { factor = 5, condicion = "" },
                            new CondicionFactores { factor = 9, condicion = "" }
                        },
            ["XXIV"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 1, condicion = "" },
                            new CondicionFactores { factor = 2, condicion = "" },
                            new CondicionFactores { factor = 4, condicion = "" },
                            new CondicionFactores { factor = 5, condicion = "" },
                            new CondicionFactores { factor = 9, condicion = "" }
                        },
            ["XXV"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 1, condicion = "" },
                            new CondicionFactores { factor = 2, condicion = "" },
                            new CondicionFactores { factor = 4, condicion = "" },
                            new CondicionFactores { factor = 5, condicion = "" }
                        },
            ["XXVI"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 2, condicion = "+T,+F" },
                            new CondicionFactores { factor = 4, condicion = "+F" }
                        },
            ["XXVII"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 1, condicion = "" },
                            new CondicionFactores { factor = 3, condicion = "" }
                        },
            ["XXVIII"] = new List<CondicionFactores>()
                        {
                            new CondicionFactores { factor = 1, condicion = "" },
                            new CondicionFactores { factor = 3, condicion = "" },
                            new CondicionFactores { factor = 8, condicion = "" }
                        }
        };




        private InfoGeografica()
        {
            GdalConfiguration.ConfigureGdal();
            GdalConfiguration.ConfigureOgr();
        }

        private static InfoGeografica _instancia;

        public static InfoGeografica Instancia()
        {
            if (_instancia == null)
                _instancia = new InfoGeografica();
            return _instancia;
        }

        public InfoSuelo DatosSuelo(int x, int y, bool ganaderia)
        {
            var datasource = Ogr.Open(".\\SuelosVCDatAgropRESULTADOFINAL5Julio.tab", 0);
            if (datasource == null)
                throw new InvalidOperationException("No se puede abrir la tabla de los datos de suelo");

            var layer = datasource.GetLayerByIndex(0);

            Geometry point = new Geometry(wkbGeometryType.wkbPoint);
            point.AddPoint((double)x, (double)y, 0);

            Geometry searchArea = point.Buffer(0.001, 0);

            // Establecer el filtro espacial
            layer.SetSpatialFilter(searchArea);
            //
            Feature feature;
            string key, valor;
            var resultado = new InfoSuelo();

            while ((feature = layer.GetNextFeature()) != null)
            {

                // Mostrar todos los atributos
                for (int i = 0; i < feature.GetFieldCount(); i++)
                {
                    key = feature.GetFieldDefnRef(i).GetName();
                    valor = feature.GetFieldAsString(i);

                    if (key == "Tipos") resultado.Tipo = AsignarClave(tipos, valor);
                    if (key == "Subtipos") resultado.Subtipo = AsignarClave(subtipos, valor);
                    if (key == "CatGral10Cult")
                    {
                        float cat = float.Parse(valor);
                        if (cat >= 1 && cat < 2) resultado.Categoria = "I";
                        else if (cat >= 2 && cat < 3) resultado.Categoria = "II";
                        else if (cat >= 3 && cat < 3.7) resultado.Categoria = "III";
                        else resultado.Categoria = "IV";
                    };
                    if (key == "Profundidad") resultado.Profundidad = valor;

                    // arreglar estas tres cosas.
                    if (key == "Pedregosidad") resultado.Pedregosidad = AsignarClave(pedregosidad, valor);
                    if (key == "Rocosidad") resultado.Rocosidad = AsignarClave(rocosidad, valor);
                    if (key == "Salinidad") resultado.Salinidad = AsignarClave(salinidad, valor);

                    if (key == "Arroz_Frio") resultado.Arroz = valor;
                    if (key == "CafÃ©") resultado.Cafe = valor;
                    if (key == "CÃ­trico") resultado.Citrico = valor;
                    if (key == "CaÃ±a_Soca") resultado.Caña = valor;
                    if (key == "Frijoles") resultado.Frijol = valor;
                    if (key == "Papa") resultado.Papa = valor;
                    if (key == "Pasto_Artificial") resultado.Pasto = valor;
                    if (key == "Platano_Fruta") resultado.Platano = valor;
                    if (key == "Tomate") resultado.Tomate = valor;
                    if (key == "Tabaco") resultado.Tabaco = valor;
                    Trace.WriteLine($"Fila {i}: {key} - {valor}");
                }
            }
            AsignarFactores(resultado, ganaderia);
            AsignarMedidas(resultado, ganaderia);
            return resultado;
        }

        private KeyValuePair<string, string> AsignarClave(Dictionary<string, string> dict, string valor)
        {
            if (dict.ContainsValue(valor))
            {
                return dict.Where(t => t.Value == valor).First();
            }
            return new KeyValuePair<string, string>("", "No presente");
        }

        private void AsignarFactores(InfoSuelo resultado, bool ganaderia)
        {
            var lista = asignacionFactores.First(k => k.Key == resultado.Tipo.Key).Value;
            resultado.Limitantes = new Dictionary<int, string>();
            foreach (CondicionFactores cond in lista)
            {
                if (cond.condicion == "")
                {
                    resultado.Limitantes[cond.factor] = factores[cond.factor];
                }
                else
                {
                    var condiciones = cond.condicion.Split(',');
                    foreach (var s in condiciones)
                    {
                        if (s[0] == '+')
                        {
                            if (resultado.Subtipo.Key == s[1].ToString())
                                if (!resultado.Limitantes.ContainsKey(cond.factor))
                                    resultado.Limitantes[cond.factor] = factores[cond.factor];
                        }
                        if (s[0] == '-')
                        {
                            if (resultado.Subtipo.Key != s[1].ToString())
                                if (!resultado.Limitantes.ContainsKey(cond.factor))
                                    resultado.Limitantes[cond.factor] = factores[cond.factor];
                        }
                    }
                }
            }
        }

        private void AsignarMedidas(InfoSuelo resultado, bool ganaderia)
        {
            resultado.Medidas = new Dictionary<int, string>();
            if (resultado.Limitantes.Count!=0)
                foreach (var factor in resultado.Limitantes)
                {
                    if (FactoresMedidas.ContainsKey(factor.Key))
                    {
                        var lista = FactoresMedidas[factor.Key];
                        foreach (int i in lista)
                        {
                            if (!resultado.Medidas.ContainsKey(i))
                                resultado.Medidas[i] = medidas[i];
                        }
                    }
                }
            if (resultado.Medidas.ContainsKey(4) && resultado.Medidas.ContainsKey(2))
                resultado.Medidas.Remove(2);
            if ((resultado.Categoria == "I" || resultado.Categoria == "II") && ganaderia)
                resultado.Medidas[0] = $"No se recomienda para ganaderia. Categoria {resultado.Categoria}";
            if (resultado.Medidas.ContainsKey(3) && Int32.Parse(resultado.Profundidad) < 50)
            {
                resultado.Medidas.Remove(3);
                resultado.Medidas[4] = medidas[4];
            }
            if (resultado.Salinidad.Key != "")
            {
                resultado.Medidas[7] = medidas[7];
                resultado.Medidas[8] = medidas[8];
            }
            if (resultado.Pedregosidad.Key != "")
            {
                resultado.Medidas[9] = medidas[9];
            }

        }
    }
}
