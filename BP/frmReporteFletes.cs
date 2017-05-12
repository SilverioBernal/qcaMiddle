using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BP
{
    public partial class frmReporteFletes : Form
    {
        public frmReporteFletes(Macroguia miReporte)
        {
            InitializeComponent();
            this.crystalReportViewer1.ReportSource = miReporte;
            this.crystalReportViewer1.Show();
        }        
    }
}
