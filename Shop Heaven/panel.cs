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
    public partial class panel : Form
    {
        public panel(string uid)
        {
            InitializeComponent();
            label3.Text = uid;
            this.Da = new DataAccess();

            this.PopulateGridView();
        }
        internal DataAccess Da { get; set; }

        internal DataSet Ds { get; set; }

        internal string Sql { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void PopulateGridView(string sql = "select * from item")
        {
            this.Ds = this.Da.ExecuteQuery(sql);

            this.dataGridView1.AutoGenerateColumns = true;
            this.dataGridView1.DataSource = this.Ds.Tables[0];
        }
        private void panel_Load(object sender, EventArgs e)
        {

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

        private void button3_Click(object sender, EventArgs e)
        {
            updatedelete f = new updatedelete();
            f.Visible = true;
            this.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Additem f = new Additem();
            f.Visible = true;
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            order f = new order();
            f.Visible = true;
            this.Visible = false;
        }
    }
}
