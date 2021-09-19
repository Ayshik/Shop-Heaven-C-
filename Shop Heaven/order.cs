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
    public partial class order : Form
    {
        public order()
        {
            InitializeComponent();
            this.Da = new DataAccess();
            this.PopulateGridView();
            button3.Visible = false;

        }
        internal DataAccess Da { get; set; }

        internal DataSet Ds { get; set; }

        internal string Sql { get; set; }
        private void order_Load(object sender, EventArgs e)
        {

            string constr = @"Data Source=AYSH-STAR;Initial Catalog=S;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from Billingsl where Status='Active';"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        string currentno = sdr["Billingno"].ToString();
                        var newno = (Convert.ToInt16(currentno) + Convert.ToInt16(1)).ToString();
                        label6.Text = newno;


                    }
                    con.Close();
                }
            }

        }
        private void PopulateGridView(string sql = "select * from item")
        {
            this.Ds = this.Da.ExecuteQuery(sql);

            this.dataGridView1.AutoGenerateColumns = true;
            this.dataGridView1.DataSource = this.Ds.Tables[0];
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            this.Sql = @"select * from item where Item_Name like '" + this.textBox4.Text + "%';";
            this.PopulateGridView(this.Sql);
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            label15.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            label11.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

            label8.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            label12.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            label13.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            if (domainUpDown1.Text.Length > 0)
            {
                label8.Text = (Convert.ToInt16(domainUpDown1.Text) * Convert.ToInt16(label12.Text)).ToString();
            }
            else
            {
                MessageBox.Show("Quantity cannot be null or 0");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label11.Text == "..." || label8.Text == "...")
            {
                MessageBox.Show("Please select a Product");
            }
            else
            {




                try
                {
                    SqlConnection con = new SqlConnection(@"Data Source=AYSH-STAR;Initial Catalog=S;Integrated Security=True");
                    con.Open();

                    int i = 0;



                    this.Sql = @"INSERT INTO Cart(Billno,Item_Name,Totalprice,Qantity) VALUES ('" + label6.Text + "','" + label11.Text + "','" + label8.Text + "','" + domainUpDown1.Text + "')";

                    SqlCommand cmd = new SqlCommand(Sql, con);

                    i = cmd.ExecuteNonQuery();



                    if (i > 0)
                    {
                        MessageBox.Show("Added To Cart");
                        button3.Visible = true;


                        //quantity update ekhantheke



                        label14.Text = (Convert.ToInt16(label13.Text) - Convert.ToInt16(domainUpDown1.Text)).ToString();

                        this.Sql = @"Update Item set Qantity='" + label14.Text + "' where Sl='"+label15.Text+"';";
                        int count = this.Da.ExecuteUpdateQuery(this.Sql);




                        this.PopulateGridView();
                        //ses quantity updated
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

        private void button3_Click(object sender, EventArgs e)
        {
            checkout f = new checkout(label6.Text);
            f.Visible = true;
            this.Visible = false;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
