using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BP
{
    public partial class ArrayControlQCA : PopedCotainer 
    {
        int _mesId = 0;
        Array datos = null;
        DataTable dtSource = null;
        List<CityAndValue> _ciudadvalor = null;
        double _total = 0;
        
        public ArrayControlQCA()
        {
            InitializeComponent();
        }

        public List<CityAndValue> CiudadValor
        {
            get { return _ciudadvalor; }
        }

        public double Total
        {

            get { return _total; }
            set { _total = value; }
            

        }

        public int Mes {

            get { return _mesId; }
            set { _mesId = value; }
        
        }

        public Array Datos {

            get { return datos; }
            set { datos = value; }

        
        }

        public DataTable CbSource
        {
            get { return dtSource; }

            set { dtSource = value; }
        }

        private DataTable dt1, dt2,dt3, dt4, dt5;

        public ArrayControlQCA(DataTable dtCity)
        {
            InitializeComponent();
           
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            retTabla();
        }

        private void retTabla() {

            System.Globalization.NumberFormatInfo info = new System.Globalization.NumberFormatInfo();
            info.NumberDecimalSeparator = ".";
            info.NumberGroupSeparator = ",";
            
            if (dtSource != null)
            {
                Double total=0;
                List<CityAndValue> cityList = new List<CityAndValue>();
                CityAndValue ctN = null;

                if (textBox1.Text.Length > 0) {

                    ctN = new CityAndValue(comboBox6.Text, Convert.ToDouble(textBox1.Text,info));
                    cityList.Add(ctN);
                    ctN = null;
                    //total = total + Convert.ToDouble(textBox1.Text.Replace('.',','));
                    total = total + Convert.ToDouble(textBox1.Text, info);
                  }

                if (textBox2.Text.Length > 0)
                {
                    ctN = new CityAndValue(comboBox2.Text, Convert.ToDouble(textBox2.Text,info));
                    cityList.Add(ctN);
                    ctN = null;
                    //total = total + Convert.ToDouble(textBox2.Text.Replace('.',','));
                    total = total + Convert.ToDouble(textBox2.Text, info);
                }
                if (textBox3.Text.Length > 0)
                {

                    ctN = new CityAndValue(comboBox3.Text, Convert.ToDouble(textBox3.Text, info));
                    cityList.Add(ctN);
                    ctN = null;
                    //total = total + Convert.ToDouble(textBox3.Text.Replace('.', ','));
                    total = total + Convert.ToDouble(textBox3.Text, info);
                }
                if (textBox4.Text.Length > 0)
                {
                    ctN = new CityAndValue(comboBox4.Text, Convert.ToDouble(textBox4.Text, info));
                    cityList.Add(ctN);
                    ctN = null;
                    //total = total + Convert.ToDouble(textBox4.Text.Replace('.', ','));
                    total = total + Convert.ToDouble(textBox4.Text, info);
                }
                if (textBox5.Text.Length > 0)
                {
                    ctN = new CityAndValue(comboBox5.Text, Convert.ToDouble(textBox5.Text, info));
                    cityList.Add(ctN);
                    ctN = null;
                    //total = total + Convert.ToDouble(textBox5.Text.Replace('.', ','));
                    total = total + Convert.ToDouble(textBox5.Text, info);
                }
                _ciudadvalor = cityList;
                datos = cityList.ToArray();
                _total = total;
            }
        }

        private void ArrayUserControl_Load(object sender, EventArgs e)
        {
            if (dtSource != null)
            {
                
            }
            

        }

        public void resetControls() {

            if (dtSource != null)
            {
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                comboBox4.Items.Clear();
                comboBox5.Items.Clear();
                comboBox6.Items.Clear();

                foreach (DataRow row in dtSource.Rows)
                {
                    comboBox2.Items.Add(row["Name"].ToString());
                    comboBox3.Items.Add(row["Name"].ToString());
                    comboBox4.Items.Add(row["Name"].ToString());
                    comboBox5.Items.Add(row["Name"].ToString());
                    comboBox6.Items.Add(row["Name"].ToString());
                }


                //comboBox6.DataSource = dt1;//dtSource.Copy();
                //comboBox2.DataSource = dt2;//dtSource.Copy();
                //comboBox3.DataSource = dt3;//dtSource.Copy();
                //comboBox4.DataSource = dt4;//dtSource.Copy();
                //comboBox5.DataSource = dt5;//dtSource.Copy();

                //comboBox6.DisplayMember = "Name";
                //comboBox6.ValueMember = "Code";
                //comboBox6.SelectedIndex = 0;

                //comboBox2.DisplayMember = "Name";
                //comboBox2.ValueMember = "Code";
                comboBox2.Text = "";
                ////comboBox6.SelectedIndex = 0;

                //comboBox3.DisplayMember = "Name";
                //comboBox3.ValueMember = "Code";
                comboBox3.Text = "";
                ////comboBox6.SelectedIndex = 2;

                //comboBox4.DisplayMember = "Name";
                //comboBox4.ValueMember = "Code";
                comboBox4.Text = "";
                ////comboBox6.SelectedIndex = 0;

                //comboBox5.DisplayMember = "Name";
                //comboBox5.ValueMember = "Code";
                comboBox5.Text = "";
                ////comboBox6.SelectedIndex = 0;

                

                textBox1.Text = null;
                //comboBox2.DataSource = null;
                textBox2.Text = null;
                //comboBox3.DataSource = null;
                textBox3.Text = null;
                //comboBox4.DataSource = null;
                textBox4.Text = null;
                //comboBox5.DataSource = null;
                textBox5.Text = null;
                _ciudadvalor = null;
            }
        
        }
             
        private bool isNumeric(string Value) {
            bool resu = false;
            double result;
                       
            if (double.TryParse(Value, out result))
            {
                resu = true;
            }
            else
            {
                resu = false;
            }
            return resu;
        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (!isNumeric(textBox1.Text))
                {
                    textBox1.Text = null;

                }

                //if (textBox1.Text.Length > 0 && comboBox2.DataSource == null)
                //{

                //    //DataTable dtNew1 = dtSource.Copy();

                //    comboBox2.DataSource = dt2;
                //    comboBox2.DisplayMember = "Name";
                //    comboBox2.ValueMember = "Code";

                //}
            }
            catch (Exception ex) {

                MessageBox.Show("" + ex.Message);
            
            }

            

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!isNumeric(textBox2.Text))
                {
                    textBox2.Text = null;
                }

                //if (textBox2.Text.Length > 0 && comboBox3.DataSource == null)
                //{

                //    //DataTable dtNew1 = dtSource.Copy();

                //    comboBox3.DataSource = dt3;
                //    comboBox3.DisplayMember = "Name";
                //    comboBox3.ValueMember = "Code";

                //}
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex.Message);

            }




        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //if (!isNumeric(textBox3.Text))
            //{
            //    textBox3.Text = null;
            //}   
            
            try
            {
                if (!isNumeric(textBox3.Text))
                {
                    textBox3.Text = null;
                }

                //if (textBox3.Text.Length > 0 && comboBox4.DataSource == null)
                //{
                //    //DataTable dtNew1 = dtSource.Copy();

                //    comboBox4.DataSource = dt4;
                //    comboBox4.DisplayMember = "Name";
                //    comboBox4.ValueMember = "Code";
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
            }                
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //if (!isNumeric(textBox4.Text))
            //{
            //    textBox4.Text = null;
            //}

            try
            {
                if (!isNumeric(textBox4.Text))
                {
                    textBox4.Text = null;
                }

                //if (textBox4.Text.Length > 0 && comboBox5.DataSource == null)
                //{
                //    //DataTable dtNew1 = dtSource.Copy();

                //    comboBox5.DataSource = dt5;
                //    comboBox5.DisplayMember = "Name";
                //    comboBox5.ValueMember = "Code";
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message);
            }                
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!isNumeric(textBox5.Text))
            {
                textBox5.Text = null;              
            }
        }

        private void comboBox6_Click(object sender, EventArgs e)
        {
           
        }
    }
}
