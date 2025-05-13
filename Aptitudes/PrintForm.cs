using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aptitudes
{
    public partial class PrintForm : Form
    {
        private Aptitud _aptitud;
        public PrintForm(Aptitud apt)
        {
            InitializeComponent();
            _aptitud = apt;
        }

        private void AsignReport()
        {
            AptitudesReport1.SetDataSource(new List<Aptitud>() { _aptitud });
        }

        private void PrintForm_Load(object sender, EventArgs e)
        {
            AsignReport();
        }
    }
}
