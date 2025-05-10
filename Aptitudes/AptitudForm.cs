using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace Aptitudes
{
    public partial class AptitudForm : Form
    {
        private bool isSaved = false;
        private Aptitud _aptitud;
        private bool isNueva { get; set; }
        public AptitudForm(Aptitud aptitud = null, bool nueva = true)
        {
            InitializeComponent();
            if (aptitud == null)
            {
                NuevaAptitud();
            }
            else
            {
                _aptitud = aptitud;
                isNueva = false;
            }
            AsignarControles();
        }

        private void AsignarControles()
        {
            aptitudBindingSource.DataSource = _aptitud;
            factoresBindingSource.DataSource = _aptitud.Factores;
            medidasBindingSource.DataSource = _aptitud.Medidas;
            listBox1.Refresh();
            listBox2.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x, y;
            if (!Int32.TryParse(coordXTextBox.Text, out x))
            {
                MessageBox.Show("Coordenada X no valida");
            }
            if (!Int32.TryParse(coordYTextBox.Text, out y))
            {
                MessageBox.Show("Coordenada Y no valida");
            }

            bool ganaderia = _aptitud.Linea.Contains("Ganaderia");
            var result = InfoGeografica.Instancia().DatosSuelo(x, y, ganaderia);
            llenarDatosSuelo(result);
            aptitudBindingSource.ResetBindings(false);
            factoresBindingSource.DataSource = _aptitud.Factores;
            medidasBindingSource.DataSource = _aptitud.Medidas;
            listBox1.Refresh();
            listBox2.Refresh();
        }

        private void llenarDatosSuelo(InfoSuelo datos)
        {
            _aptitud.Tipo = datos.Tipo.Value;
            _aptitud.Subtipo = datos.Subtipo.Value;
            _aptitud.Categoria = datos.Categoria;
            _aptitud.Profundidad = Int32.Parse(datos.Profundidad);

            if (datos.Salinidad.Key == "")
                _aptitud.Salinidad = "No Salino";
            else
                _aptitud.Salinidad = datos.Salinidad.Value;

            if (datos.Pedregosidad.Key == "")
                _aptitud.Pedregosidad = "No Pedregoso";
            else
                _aptitud.Pedregosidad = datos.Pedregosidad.Value;

            if (datos.Rocosidad.Key == "")
                _aptitud.Rocosidad = "No Rocoso";
            else
                _aptitud.Rocosidad = datos.Rocosidad.Value;

            _aptitud.Formula = $"{datos.Tipo.Key}{datos.Subtipo.Key}{datos.Salinidad.Key}" +
                $"{datos.Pedregosidad.Key}{datos.Rocosidad.Key}{datos.Profundidad}";

            _aptitud.Arroz = datos.Arroz;
            _aptitud.Cafe = datos.Cafe;
            _aptitud.Caña = datos.Caña;
            _aptitud.Citrico = datos.Citrico;
            _aptitud.Frijoles = datos.Frijol;
            _aptitud.Papa = datos.Papa;
            _aptitud.Pasto = datos.Pasto;
            _aptitud.Platano = datos.Platano;
            _aptitud.Tabaco = datos.Tabaco;
            _aptitud.Tomate = datos.Tomate;

            _aptitud.Factores = new List<Factores>();
            foreach (var f in datos.Limitantes)
            {
                _aptitud.Factores.Add(new Factores() { Id = f.Key, Factor = f.Value });
            }

            _aptitud.Medidas = new List<Medidas>();
            foreach (var f in datos.Medidas)
            {
                _aptitud.Medidas.Add(new Medidas() { Id = f.Key, Medida = f.Value });
            }
        }

        private void EjecutorLeaveFocus(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;
            var nombre = combo.Text;
            if (!combo.Items.Contains(nombre) && nombre != "")
                combo.Items.Add(nombre);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SalvarAptitud())
                Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool SalvarAptitud()
        {
            var contexto = AptitudesContext.Instancia();
            _aptitud.NumTenente = _aptitud.Carnet;

            // Validar Aptitud
            AptitudesValidation validator = new AptitudesValidation();
            var result = validator.Validate(_aptitud);
            if (!result.IsValid)
            {
                MessageBox.Show("Existen errores en la aptitud de Suelo" + Environment.NewLine + result.ToString(Environment.NewLine));
                isSaved = false;
                return false;
            }

            if (isNueva) contexto.Aptitudes.Add(_aptitud);
            contexto.SaveChanges();
            isSaved = true;
            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (SalvarAptitud())
            {
                NuevaAptitud();
                AsignarControles();
            }
        }

        private void NuevaAptitud()
        {
            _aptitud = new Aptitud();
            _aptitud.Municipio = "Sagua la Grande";
            _aptitud.MunTierra = "Sagua la Grande";
            _aptitud.Provincia = "Villa Clara";
            isNueva = true;
        }

        private void expedienteTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isNueva)
            {
                int expediente;
                if (!Int32.TryParse((sender as TextBox).Text, out expediente))
                {
                    MessageBox.Show("Numero de expediente invalido");
                    e.Cancel = true;
                }
                var aptitud = AptitudesContext.Instancia().Aptitudes
                    .Include("Medidas").Include("Factores")
                    .Where(a => a.Expediente == expediente).FirstOrDefault();
                if (aptitud != null)
                {
                    var result = MessageBox.Show($"Expediente numero {expediente} ya existe. Desea editarlo", "Aptitud", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        _aptitud = aptitud;
                        isNueva = false;
                        AsignarControles();
                    }
                }

            }
        }

        private void AptitudForm_Load(object sender, EventArgs e)
        {
            ejecutorComboBox.Items.AddRange(Estado.Ejecutores.ToArray());
        }

        private void AptitudForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSaved)
            {
                if (MessageBox.Show("Desea salir sin guardar cambios", "Aptitudes", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            Estado.Ejecutores = ejecutorComboBox.Items.Cast<string>().ToList();
        }
    }
}
