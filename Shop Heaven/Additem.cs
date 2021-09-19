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
    public partial class Additem : Form
    {
        public Additem()
        {
            InitializeComponent();
        }
        internal DataAccess Da { get; set; }

        internal DataSet Ds { get; set; }

        internal string Sql { get; set; }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Please fill all the value");
            }
            else
            {




                try
                {
                    SqlConnection con = new SqlConnection(@"Data Source=AYSH-STAR;Initial Catalog=S;Integrated Security=True");
                    con.Open();

                    int i = 0;



                    this.Sql = @"INSERT INTO item(Item_Name,Unit_Price,Qantity) VALUES ('" + textBox2.Text + "','" + textBox5.Text + "','" + textBox3.Text + "')";

                    SqlCommand cmd = new SqlCommand(Sql, con);

                    i = cmd.ExecuteNonQuery();



                    if (i > 0)
                    {
                        MessageBox.Show("Succesfully Added");

                    }
                    else
                    { MessageBox.Show("Server Busy"); }


                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }







            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Visible = true;
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel f = new panel(label3.Text);
            f.Visible = true;
            this.Visible = false;
        }
    }
}
