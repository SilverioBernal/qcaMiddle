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
    public partial class frmEnvaseDevolutivo : Form
    {
        EnvaseDevolutivo envaseDevolutivo = new EnvaseDevolutivo();
        List<MktDocHeader> lsSaleDocuments = new List<MktDocHeader>();
        List<MktDocHeader> lsPurchaseDocuments = new List<MktDocHeader>();

        public frmEnvaseDevolutivo()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void frmEnvaseDevolutivo_Load(object sender, EventArgs e)
        {                        
            ///* Delivery */
            //toolStripStatusLabel1.Text = "Buscando informacion de ventas...";
            //toolStripProgressBar1.Value = 0;
            //lsSaleDocuments.AddRange(envaseDevolutivo.GetBaseDocumentsForSync(15));
            
            ///* Goods receipt purchase order */
            //toolStripStatusLabel1.Text = "Buscando informacion de compras...";
            //toolStripProgressBar1.Value = 50;
            //lsPurchaseDocuments.AddRange(envaseDevolutivo.GetBaseDocumentsForSync(20));

            //toolStripProgressBar1.Value = 0;

            SaveWkr.RunWorkerAsync();
        }

        private void SaveWkr_DoWork(object sender, DoWorkEventArgs e)
        {
            lsSaleDocuments.AddRange(envaseDevolutivo.GetBaseDocumentsForSync(15));
            lsPurchaseDocuments.AddRange(envaseDevolutivo.GetBaseDocumentsForSync(20));

            int totalRecords = lsSaleDocuments.Count + lsPurchaseDocuments.Count;
            int processedRecords = 1;
            int advancePerc = 0;

            foreach (MktDocHeader item in lsSaleDocuments)
            {
                //si no existe el documento se crea con sus productos en tabla de envase devolutivo
                if (!envaseDevolutivo.ExistsBaseDocument(item))                
                    envaseDevolutivo.SaveBaseDocument(item);
                
                //busqueda de notas credito a sincronizar
                List<MktDocHeader> lsCreditNotes = envaseDevolutivo.GetCreditNoteDocumentsForSync(item.objType, item.docEntry);

                foreach (MktDocHeader itemNC in lsCreditNotes)
                {
                    if (!envaseDevolutivo.ExistsChildDocument(itemNC))
                        envaseDevolutivo.SaveCreditNoteDocument(itemNC);
                }

                advancePerc = (100 * processedRecords) / totalRecords;
                SaveWkr.ReportProgress(advancePerc, "Guardando Información de clientes");
                processedRecords++;
            }

            foreach (MktDocHeader item in lsPurchaseDocuments)
            {
                //si no existe el documento se crea con sus productos en tabla de envase devolutivo
                if (!envaseDevolutivo.ExistsBaseDocument(item))                
                    envaseDevolutivo.SaveBaseDocument(item);
                

                advancePerc = (100 * processedRecords) / totalRecords;
                SaveWkr.ReportProgress(advancePerc, "Guardando Información de proveedores");
                processedRecords++;
            }
        }

        private void SaveWkr_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
            toolStripStatusLabel1.Text = e.UserState.ToString();
        }

        private void SaveWkr_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }       
    }
}
