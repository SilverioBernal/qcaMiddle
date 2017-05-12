using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Deployment;
using System.CodeDom.Compiler;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.CSharp;

namespace BP
{
    public partial class frmMenuPrincipal : Form
    {
        DataSet curmenu = new DataSet();
        public frmMenuPrincipal(String unaCompañia)
        {
            InitializeComponent();
            this.tslLabel.Text = "Conectado a: " + unaCompañia+" Con el Ingeniero: "+loging.usrCode;
        } 
        private void button3_Click(object sender, EventArgs e)
        {

            string cSql;
            DataSet oDs = ClaseDatos.procesaDataSet("select * from [@CSS_PREVENTAS_lines] where u_costo is not null");

            if (oDs.Tables[0].Rows.Count > 0)
            {
                //actualizo el costo segun el margen donde no hay variacion de costo
                cSql = 
                    "update " +
	                "    [@CSS_PREVENTAS_lines]  " +
                    "set  " +
                    "    u_precio = u_costo + ((u_costo*u_margen)/100) " +
                    "where  " +
	                "    u_precio is null " +
                    "    and u_costo is not null " +
	                "    and (u_mesvar1 = 0 and u_mesvar2 = 0 and u_mesvar3 = 0 )";

                tslLabel.Text = ClaseDatos.nonQuery(cSql);

                //actualizo el precio segun el margen a para los que tienen variacion de costo 1


            }
        }

        private void frmMenuPrincipal_Load(object sender, EventArgs e)
        {
            #region Definicion de consultas
            string cQqery;
            if (loging.usrCode.ToUpper() == "B_JEFTIC")//== "QCA01")
            {
                cQqery =
                    "SELECT " +
                    "    a.* " +
                    "FROM [@CSS_MENU_MIDDLE] a " +
                    "ORDER BY  U_hijo, U_padre";
            }
            else
            {
                cQqery =
                    "SELECT " +
                    "    a.* " +
                    "FROM [@CSS_MENU_MIDDLE] a " +
                    "    inner join  [@css_usuarios_middle] b on a.U_hijo = b.U_modulo  " +
                    "    and U_usuario = '" + loging.usrCode + "' " +
                    "ORDER BY  U_hijo, U_padre ";
            }
            #endregion 

            curmenu  = ClaseDatos.procesaDataSet(cQqery);
            CrearNodosDelPadre("1_", null);

            TreeNode nuevoNodo = new TreeNode();
            trwMenu.Nodes.Add(nuevoNodo);
            nuevoNodo.Text = "Salir";
            nuevoNodo.ImageIndex = 4;
        }

        private void CrearNodosDelPadre(string indicePadre, TreeNode nodePadre)
        {
            string FILTRO ;
            // Crear un DataView con los Nodos que dependen del Nodo padre pasado como parámetro.
            DataView dataViewHijos = new DataView(curmenu.Tables[0]);
            FILTRO = curmenu.Tables[0].Columns["U_PADRE"].ColumnName + " = '" + indicePadre + "'";

            dataViewHijos.RowFilter = FILTRO;

            // Agregar al TreeView los nodos Hijos que se han obtenido en el DataView.
            foreach (DataRowView dataRowCurrent in dataViewHijos)
            {
                TreeNode nuevoNodo = new TreeNode();
                nuevoNodo.Text = dataRowCurrent["U_TEXTO"].ToString().Trim();
                nuevoNodo.ImageIndex = Int32.Parse(dataRowCurrent["U_ICONO"].ToString());
                nuevoNodo.Name = dataRowCurrent["U_COMANDO"].ToString();

                // si el parámetro nodoPadre es nulo es porque es la primera llamada, son los Nodos
                // del primer nivel que no dependen de otro nodo.
                if (nodePadre == null)
                {
                    trwMenu.Nodes.Add(nuevoNodo);
                }
                // se añade el nuevo nodo al nodo padre.
                else
                {
                    nodePadre.Nodes.Add(nuevoNodo);
                }

                // Llamada recurrente al mismo método para agregar los Hijos del Nodo recién agregado.

                CrearNodosDelPadre(dataRowCurrent["U_HIJO"].ToString(), nuevoNodo);
            }
        }

        public void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trwMenu.SelectedNode.Name.ToString() != "")
            {              
                ClaseDatos.lanzaForm(trwMenu.SelectedNode.Name.ToString(),this);
            }
            else if (trwMenu.SelectedNode.Text.ToString() == "Salir")
            {
                ClaseDatos.objCompany.Disconnect();
                Application.Exit();
            }
        }
        private void editarOpciones_Click(object sender, EventArgs e)
        {
            ClaseDatos.lanzaForm("frmEditMenuOptions",this);
        }
        private void importarMenu_Click(object sender, EventArgs e)
        {
            string cQuery, rslQuery;

            DataSet cMenuImp = null;
            cQuery = "select * from [@css_menu_middle] where 1 = 2";
            cMenuImp = ClaseDatos.procesaDataSet(cQuery);

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "XML files (*.xml)|*.XML";
            openFileDialog1.FilterIndex = 2;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName.ToString() != "")
                {
                    cMenuImp.Tables[0].ReadXml(openFileDialog1.FileName.ToString());
                    DialogResult result =
                        MessageBox.Show("Esta Accion Borrara el menú, \nLos permisos asignados a los usuarios\nDebe reiniciar la aplicacion para hacer efectivos los cambios", "Limpiar Menu", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        cQuery = "delete from [@css_menu_middle]";
                        rslQuery = ClaseDatos.nonQuery(cQuery);

                        cQuery = "delete from [@css_usuarios_middle]";
                        rslQuery = ClaseDatos.nonQuery(cQuery);

                        cQuery = "delete from [@CSS_MIDDLE_CTRL_USR]";
                        rslQuery = ClaseDatos.nonQuery(cQuery);
                        foreach (DataRow renglon in cMenuImp.Tables[0].Rows)
                        {

                            cQuery =
                                "Insert into [@css_menu_middle] (code, name, u_padre, U_hijo, u_texto, u_comando, U_icono)" +
                                "values (" + renglon[0].ToString() + ", " + renglon[1].ToString() + ", '" + renglon[2].ToString() +
                                "', '" + renglon[3].ToString() + "', '" + renglon[4].ToString() + "', '" + renglon[5].ToString() +
                                "', " + renglon[6].ToString() + ")";

                            tslLabel.Text = ClaseDatos.nonQuery(cQuery);
                        }
                        Application.Restart();
                    }
                }
            }
        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClaseDatos.objCompany.Disconnect();
            Application.Exit();
        }

        private void tsmiCascada_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void tsmiVertical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void tsmiHorizontal_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal); 
        }

        private void tsmiCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form miFormularioHijo in this.MdiChildren)
                {
                    miFormularioHijo.Close();
                }
            }
            catch (Exception miExcepcion)
            {
                MessageBox.Show(miExcepcion.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmMenuPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClaseDatos.objCompany.Disconnect();
            Application.Exit();
        }

        private void cambiarDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loging nuevo = new loging();
            nuevo.Show();
            
            this.Hide();

        }           

    }
}
