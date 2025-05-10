using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Aptitudes
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Estado.LoadList();
            cmbMes.Items.AddRange(Enumerable.Range(1, 12).Select(m => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m)).ToArray());
            cmbMes.SelectedIndex = DateTime.Today.Month;

            // Para el año
            int currentYear = DateTime.Now.Year;
            cmbAño.Items.AddRange(Enumerable.Range(currentYear - 10, 20).Select(y => y.ToString()).ToArray());
            cmbAño.SelectedItem = DateTime.Now.Year.ToString();

            aptitudBindingSource.DataSource = AptitudesContext.Instancia().Aptitudes.ToList();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var aptitudId = ((sender as DataGridView).CurrentRow.DataBoundItem as Aptitud).Id;
            var objeto = AptitudesContext.Instancia().Aptitudes.Include("Medidas").Include("Factores").First(t => t.Id == aptitudId);
            new AptitudForm(objeto).ShowDialog();
            aptitudBindingSource.DataSource = AptitudesContext.Instancia().Aptitudes.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AptitudForm().ShowDialog();
            aptitudBindingSource.DataSource = AptitudesContext.Instancia().Aptitudes.ToList();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Estado.GuardarLista();
        }
    }
}
