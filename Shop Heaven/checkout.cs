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
    public partial class checkout : Form
    {
        public checkout(string bill)
        {
            
            InitializeComponent();
            this.Da = new DataAccess();

            label6.Text = bill;
        }
        internal DataAccess Da { get; set; }

        internal DataSet Ds { get; set; }

        internal string Sql { get; set; }
        private void checkout_Load(object sender, EventArgs e)
        {
            this.Sql = @"select * from Cart where Billno='"+label6.Text+"'";
            this.Ds = this.Da.ExecuteQuery(Sql);

            this.dataGridView1.AutoGenerateColumns = true;
            this.dataGridView1.DataSource = this.Ds.Tables[0];


            
                    SqlConnection con2 = new SqlConnection(@"Data Source=AYSH-STAR;Initial Catalog=S;Integrated Security=True");
                con2.Open();
                string query2 = string.Format("SELECT SUM(Totalprice)FROM Cart where Billno='" + label6.Text + "';");
                SqlCommand cmd2 = new SqlCommand(query2, con2);
                SqlDataAdapter sa2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                sa2.Fill(dt2);
                con2.Close();

            if (dt2.Rows.Count == 1)
            {
                label8.Text = dt2.Rows[0][0].ToString();


            }




                }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = (Convert.ToInt16(textBox3.Text) - Convert.ToInt16(label8.Text)).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("please Write customer name ,phone and given money");
            }
            else
            {
                try
                {
                   
                    this.Sql = @"insert into history values ('" + this.textBox2.Text + "','" + this.textBox5.Text + "','" +
                           this.label8.Text + "','" + this.textBox1.Text + "','" + this.label6.Text + "','" + DateTime.Now + "');";
                    int count2 = this.Da.ExecuteUpdateQuery(this.Sql);

                    label13.Text = (Convert.ToInt16(label6.Text) + Convert.ToInt16(1)).ToString();

                    this.Sql = @"Update Billingsl set Billingno='" + label13.Text + "' where Status='Active';";
                    int count = this.Da.ExecuteUpdateQuery(this.Sql);


                    if (count2 == 1)
                    {
                        MessageBox.Show(" Payment Done!!!");
                        panel f = new panel(label3.Text);
                        f.Visible = true;
                        this.Visible = false;

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
