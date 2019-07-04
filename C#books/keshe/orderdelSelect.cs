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
    public partial class orderdelSelect : Form
    {
        public orderdelSelect()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Common.Con);
            string _str = "delete from orderinfo where orderState='已完成'";
            SqlCommand cmd = new SqlCommand(_str, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            this.Close();
        }
    }
}
