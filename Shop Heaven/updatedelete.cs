using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_Heaven
{
    public partial class updatedelete : Form
    {
        public updatedelete()
        {
            InitializeComponent();
            this.Da = new DataAccess();
            this.PopulateGridView();
        }
        internal DataAccess Da { get; set; }

        internal DataSet Ds { get; set; }

        internal string Sql { get; set; }


        private void PopulateGridView(string sql = "select * from item")
        {
            this.Ds = this.Da.ExecuteQuery(sql);

            this.dataGridView1.AutoGenerateColumns = true;
            this.dataGridView1.DataSource = this.Ds.Tables[0];
        }
        private void updatedelete_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

            this.Sql = @"select * from item where Item_Name like '" + this.textBox4.Text + "%';";
            this.PopulateGridView(this.Sql);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
          
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
           
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox5.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Please select a row to Update");
            }

            else
            {

                try
                {

                    this.Sql = @"UPDATE item SET Item_Name='" + textBox2.Text + "',Unit_Price='" + textBox5.Text + "',Qantity='" + textBox3.Text + "' WHERE Sl='" + textBox1.Text + "'";
                    int count = this.Da.ExecuteUpdateQuery(this.Sql);


                    if (count == 1)
                    {
                        MessageBox.Show("Item info Updated!!!");
                        updatedelete sn = new updatedelete();
                        sn.Visible = true;
                        this.Visible = false;

                    }

                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res.Equals(DialogResult.Yes))
            {
                if (label1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Please select a Item to Delete");
                }

                else
                {


                    this.Sql = "DELETE FROM Item WHERE Sl ='" + textBox1.Text + "'";
                    int count = this.Da.ExecuteUpdateQuery(this.Sql);

                    if (count == 1)
                    {
                        MessageBox.Show("Item Deleted");
                        updatedelete sn = new updatedelete();
                        sn.Visible = true;
                        this.Visible = false;
                    }
                    else
                        MessageBox.Show("Error while deleting data");

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
