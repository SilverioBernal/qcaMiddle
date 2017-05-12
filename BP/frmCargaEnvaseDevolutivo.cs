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
    public partial class frmCargaEnvaseDevolutivo : Form
    {
        DataSet miDataSet = new DataSet();

        public frmCargaEnvaseDevolutivo()
        {
            InitializeComponent();
        }

        private void btnFindFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Archivos de texto separados por tabulador (*.txt)|*.TXT";
            openFileDialog1.FilterIndex = 2;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName.ToString() != "")
                {
                    this.txtPath.Text = openFileDialog1.FileName.ToString();
                }
            }
        }

        private void btnProccess_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtPath.Text.Length > 0)
                {
                    btnCancel.Enabled = false;
                    btnFindFile.Enabled = false;
                    btnProccess.Enabled = false;
                    toolStripStatusLabel1.Text = "Leyendo el archivo...";

                    miDataSet = ClaseDatos.importTabFile(txtPath.Text);

                    toolStripProgressBar1.Value = 0;
                    SaveWkr.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un archivo", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show("Error al cargar el archivo: " + miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveWkr_DoWork(object sender, DoWorkEventArgs e)
        {
            //EnvaseDevolutivo envaseDevolutivo = new EnvaseDevolutivo();

            //int totalRecords = miDataSet.Tables[0].Rows.Count;
            //int processedRecords = 1;
            //int advancePerc = 0;

            //foreach (DataRow item in miDataSet.Tables[0].Rows)
            //{
            //    ItemDetail newItem = new ItemDetail()
            //    {
            //        //U_date = DateTime.Parse(item[0].ToString()),
            //        U_objType = int.Parse(item[1].ToString()),
            //        U_docNum = item[2].ToString(),
            //        U_itemCode = item[3].ToString(),                    
            //        U_returned = int.Parse(item[4].ToString()),
            //        U_maintenance = int.Parse(item[5].ToString()),
            //        U_ready = int.Parse(item[6].ToString()),
            //        U_targetType = 500                  
            //    };

            //    envaseDevolutivo.Create(newItem);

            //    advancePerc = (100 * processedRecords) / totalRecords;

            //    SaveWkr.ReportProgress(advancePerc, "Guardando Información");
            //    processedRecords++;
            //}
        }

        private void SaveWkr_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
            toolStripStatusLabel1.Text = e.UserState.ToString();
        }

        private void SaveWkr_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripProgressBar1.Value = 0;
            toolStripStatusLabel1.Text = "Proceso finalizado";

            MessageBox.Show("Proceso finalizado");

            btnCancel.Enabled = true;
            btnFindFile.Enabled = true;
            btnProccess.Enabled = true;
            txtPath.Text = "";
        }
    }
}
