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
    public partial class frmEditMenuOptions : Form
    {
        DataSet dsPadres = null;
        DataSet dsMenu = null;
        public frmEditMenuOptions()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void frmEditMenuOptions_Load(object sender, EventArgs e)
        {
            pnlEditMenu.Enabled = false;
            pnlEditMenu.Visible = false;

            buscaPadres();
            menu();
            campos("D");
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (txtDevPass.Text == "consenwall")
            {
                pnlDevPass.Visible = false;
                pnlDevPass.Enabled = false;
                pnlEditMenu.Enabled = true;
                pnlEditMenu.Visible = true;
            }
        }

        private void buscaPadres()
        {
            string cQuery;
            cQuery = "SELECT u_hijo FROM [@css_menu_middle] ORDER BY u_hijo";
            dsPadres =  ClaseDatos.procesaDataSet(cQuery);

            if(dsPadres.Tables[0].Rows.Count == 0)
            {
                cQuery = "SELECT '1_' as u_hijo";
                dsPadres =  ClaseDatos.procesaDataSet(cQuery);
            }

            cboPadre.DataSource = dsPadres.Tables[0];
            cboPadre.DisplayMember = "U_HIJO";
            cboPadre.ValueMember = "U_HIJO";
        }

        private void campos(string accion)
        {
            if (accion == "E")
            {
                cboPadre.Enabled = true;
                txthijo.Enabled = true;
                txtTextoMenu.Enabled = true;
                txtForm.Enabled = true;
                cboIcono.Enabled = true;
            }
            if (accion == "D")
            {
                cboPadre.Enabled = false;
                txthijo.Enabled = false;
                txtTextoMenu.Enabled = false;
                txtForm.Enabled = false;
                cboIcono.Enabled = false;
            }
        }

        private void menu()
        {
            string cQuery;
            cQuery =
                "SELECT u_padre, U_hijo, u_texto, u_comando, U_icono FROM [@css_menu_middle] order by  U_hijo, u_padre";
            dsMenu = ClaseDatos.procesaDataSet(cQuery);
            grdMenu.DataSource = dsMenu.Tables[0];
        }

        private void cboPadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cQuery;
            int next;
            cQuery = "Select Count(U_HIJO)+1 from [@css_menu_middle] where U_PADRE = '" + cboPadre.Text + "'";

            next = ClaseDatos.scalarIntSql(cQuery);
            txthijo.Text = cboPadre.Text + next.ToString() + "_";
        }

        private void newItem_Click(object sender, EventArgs e)
        {
            campos("E");
            newItem.Enabled = false;
            clearMenu.Enabled = false;
            saveData.Enabled = true;
        }

        private void clearMenu_Click(object sender, EventArgs e)
        {
            string cQuery, rslQuery;
            DialogResult result = 
            MessageBox.Show("Esta Accion Borrara el menú. \nLos permisos asignados a los usuarios", "Limpiar Menu", MessageBoxButtons.YesNo);

            newItem.Enabled = false;
            clearMenu.Enabled = false;
            saveData.Enabled = false;

            if (result == DialogResult.Yes)
            {
                cQuery = "delete from [@css_menu_middle]";
                rslQuery = ClaseDatos.nonQuery(cQuery);

                cQuery = "delete from [@css_usuarios_middle]";
                rslQuery = ClaseDatos.nonQuery(cQuery);

                cQuery = "delete from [@CSS_MIDDLE_CTRL_USR]";
                rslQuery = ClaseDatos.nonQuery(cQuery);

                menu();
            }

            newItem.Enabled = true;
            clearMenu.Enabled = false;
            saveData.Enabled = true;

        }

        private void saveData_Click(object sender, EventArgs e)
        {
            string cQuery, rsl;
            int next;

            if (cboPadre.Text != "" && txthijo.Text != "" && txtTextoMenu.Text != "" && cboIcono.Text != "")
            {
                cQuery =
                    "select MAX(cast(Code as int)) + 1 from [@css_menu_middle]";
                next = ClaseDatos.scalarIntSql(cQuery);

                cQuery =
                    "Insert into [@css_menu_middle] (code, name, u_padre, U_hijo, u_texto, u_comando, U_icono)" +
                    "values (" + next.ToString() + ", " + next.ToString() + ", '" + cboPadre.Text + "', '" +
                    txthijo.Text + "', '" + txtTextoMenu.Text + "', '" + txtForm.Text + "', " + cboIcono.Text + ")";

                rsl = ClaseDatos.nonQuery(cQuery);
                toolStripStatusLabel1.Text = rsl;
                menu();
                campos("D");
            }

            newItem.Enabled = true;
            clearMenu.Enabled = true;
            saveData.Enabled = false;

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            string cQuery;

            DataSet cMenuExp = null ;
            cQuery = "select * from [@css_menu_middle] order by  U_hijo, u_padre";
            cMenuExp = ClaseDatos.procesaDataSet(cQuery);

            SaveFileDialog guardarXml = new SaveFileDialog();
            guardarXml.InitialDirectory = "c:\\";
            guardarXml.Filter = "XML files (*.XML)|*.XML";
            guardarXml.FilterIndex = 2;            

            if (guardarXml.ShowDialog() == DialogResult.OK)
            {
                if (guardarXml.FileName.ToString() != "")
                {
                    cMenuExp.Tables[0].WriteXml(guardarXml.FileName.ToString());
                }
            }
        }
    }
}
