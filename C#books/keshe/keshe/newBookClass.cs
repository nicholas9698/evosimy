using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace keshe
{
    public partial class newBookClass : Form
    {
        public newBookClass()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Common.Con);
            string InsertClass = "insert into bookclass(bkc) values('" + textBox1.Text + "')";
            SqlCommand cmd = new SqlCommand(InsertClass, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            this.Close();
        }
    }
}
