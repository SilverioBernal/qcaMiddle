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
    public partial class frmCtrlUsrMiddle : Form
    {
        public frmCtrlUsrMiddle()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }
        DataSet dsUsr = null;
        DataSet dsVldUsr = null;
        DataSet dsUsrMod = null;

        private void frmCtrlUsrMiddle_Load(object sender, EventArgs e)
        {
            getusers();
            toolStripStatusLabel1.Text = "";
        }

        private void grdusr_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string cQuery;

            if (grdusr.CurrentRow.Cells[1].Value.ToString() == "True")
            {
                int code;
                cQuery = "select isnull(Max(cast(code as int)),0) +1 from [@css_middle_ctrl_usr]";
                code = ClaseDatos.scalarIntSql(cQuery);
                cQuery =
                    "INSERT INTO [@css_middle_ctrl_usr] (code, name, u_usuario)" +
                    "VALUES(" + code.ToString() + ", " + code.ToString() + ", '" + grdusr.CurrentRow.Cells[0].Value.ToString() + "')";
            }
            else { cQuery = "delete from [@css_usuarios_middle] where u_usuario = '" + grdusr.CurrentRow.Cells[0].Value.ToString() + "'"; }

            ClaseDatos.nonQuery(cQuery);
            
            getusers();            
        }

        private void getusers()
        {
            dsUsr = ClaseDatos.procesaDataSet("select * from CSS_USR");
            grdusr.DataSource = dsUsr.Tables[0];
            grdusr.Columns[0].Width = 90;
            grdusr.Columns[1].Width = 32;

            grdusr.Columns[0].HeaderText = "Usuario";
            grdusr.Columns[1].HeaderText = "Act";

            dsVldUsr = ClaseDatos.procesaDataSet("select '' AS U_USUARIO UNION ALL select DISTINCT U_USUARIO from [@css_middle_ctrl_usr] order by U_USUARIO");
            cboVldUsr.DataSource = dsVldUsr.Tables[0];
            cboVldUsr.DisplayMember = "U_USUARIO";
            cboVldUsr.ValueMember  = "U_USUARIO";
        }

        private void cboVldUsr_Click(object sender, EventArgs e) { }

        private void cboVldUsr_SelectedIndexChanged(object sender, EventArgs e)
        {           
            string cQuery;
            string cValCombo;
            bool buscar;
            buscar = false ;
            cValCombo = cboVldUsr.Text;

            if (cValCombo.Length > 6 && cValCombo.ToUpper().Substring(0, 6) != "SYSTEM") { if (cValCombo != "") { buscar = true; } }

            if (cValCombo.Length  <= 6 && cValCombo != ""){buscar = true;}

            try
            {
                dsUsrMod = null;
                if (buscar == true)
                {
                    cQuery =
                        "SELECT " +
                        "	u_hijo, u_texto,  " +
                        "	CAST (case when u_modulo is null then 0 else 1 end AS BIT) checked  " +
                        "FROM [@CSS_MENU_MIDDLE] a  " +
                        "	left join  [@css_usuarios_middle] b on a.u_hijo = b.u_modulo " +
                        "and u_usuario = '" + cValCombo + "' ORDER BY  u_hijo, u_padre ";

                    dsUsrMod = ClaseDatos.procesaDataSet(cQuery);
                    grdUsrMod.DataSource = dsUsrMod.Tables[0];
                }
            }
            catch (Exception er) { toolStripStatusLabel1.Text = er.Message; }
        }

        private void grdUsrMod_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string cQuery, cModulo, cTmpMol;
            int nIndice;
            if (grdUsrMod.CurrentCell.Value.ToString() == "True")
            {
                cQuery = "select isnull(max(cast(code as int)),0) + 1 from [@css_usuarios_middle]";
                nIndice = ClaseDatos.scalarIntSql(cQuery);

                cQuery =
                    "INSERT INTO [@css_usuarios_middle] (code, name, u_usuario, u_modulo) " +
                    "values (" + nIndice + ", " + nIndice + ", '" + cboVldUsr.SelectedValue.ToString() + "', '" +
                    grdUsrMod.CurrentRow.Cells[0].Value.ToString() + "')";

                ClaseDatos.nonQuery(cQuery);
                // si el registro tiene padre le doy permisos por jerarquia

                cModulo = grdUsrMod.CurrentRow.Cells[0].Value.ToString();
                if (cModulo.Length - 2 > 0)
                {
                    for (int i = 0; i < grdUsrMod.RowCount - 1; i++)
                    {
                        cTmpMol = grdUsrMod.Rows[i].Cells[0].Value.ToString();

                        if (cModulo.Length >= cTmpMol.Length)
                        {
                            if (cTmpMol == cModulo.Substring(0, cTmpMol.Length)) //cTmpMol.Substring(0,cModulo.Length) == cModulo
                            {
                                if (grdUsrMod.Rows[i].Cells[2].Value.ToString() == "False")
                                {
                                    grdUsrMod.Rows[i].Cells[2].Value = true;
                                    cQuery = "select isnull(max(cast(code as int)),0) + 1 from [@css_usuarios_middle]";
                                    nIndice = ClaseDatos.scalarIntSql(cQuery);

                                    cQuery =
                                        "INSERT INTO [@css_usuarios_middle] (code, name, u_usuario, u_modulo) " +
                                        "values (" + nIndice + ", " + nIndice + ", '" + cboVldUsr.SelectedValue.ToString() + "', '" +
                                        cTmpMol + "')";

                                    ClaseDatos.nonQuery(cQuery);
                                }
                            }
                        }
                    }
                }
            }
            else if (grdUsrMod.CurrentCell.Value.ToString() == "False")
            {
                // Si el permiso tiene hijos los quito por jerarquia... 
                cModulo = grdUsrMod.CurrentRow.Cells[0].Value.ToString();
                for (int i = 0; i < grdUsrMod.RowCount - 1; i++)
                {
                    cTmpMol = grdUsrMod.Rows[i].Cells[0].Value.ToString();
                    if (cTmpMol.Length >= cModulo.Length && cTmpMol.Substring(0,cModulo.Length) == cModulo)
                    {
                        grdUsrMod.Rows[i].Cells[2].Value = false;

                        cQuery =
                            "delete from [@css_usuarios_middle] where u_usuario = '" + cboVldUsr.SelectedValue.ToString() + "' and " +
                            "u_modulo = '" + cTmpMol + "' ";

                        ClaseDatos.nonQuery(cQuery);
                    }
                }
            }
        }

        private void grdUsrMod_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            grdUsrMod.EndEdit();
        }

        private void grdUsrMod_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
