using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_Heaven
{
    public partial class Form1 : Form
    {
       
        Boolean path = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=AYSH-STAR;Initial Catalog=S;Integrated Security=True");
            con.Open();

            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Username or passward cannot be empty");
            }
            else
            {




                {
                    if (path == false)
                    {
                        string query = string.Format("Select * from loginStudent where username= '" + textBox1.Text + "' and pass='" + textBox2.Text + "'");
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataAdapter sa = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sa.Fill(dt);
                        con.Close();


                        if (dt.Rows.Count == 1)
                        {
                            path = true;
                            panel c = new panel(textBox1.Text);
                            this.Visible = false;
                            c.Visible = true;

                        }
                        else
                        {
                            path = false;
                        }
                    }
                    if (path == false)
                    {
                        MessageBox.Show("Your username Password not found");
                    }
                }
            }
        }
    }
}