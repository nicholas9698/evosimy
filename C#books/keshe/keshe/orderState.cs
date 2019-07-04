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
    public partial class orderState : Form
    {
        public orderState()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ch = "";
            if (radioButton1.Checked == true) ch = "未完成";
            else ch = "已完成";
            SqlConnection con = new SqlConnection(Common.Con);
            string str = "update orderinfo set orderState='" + ch + "' where orderNum=" + textBox1.Text;
            SqlCommand cmd = new SqlCommand(str, con);
            SqlCommand cmmd = new SqlCommand("select * from orderinfo where orderNum=" + textBox1.Text, con);
            con.Open();
            if (cmmd.ExecuteScalar() == null)
            {
                MessageBox.Show("订单不存在");
                con.Close();
            }
            else
            {
                cmd.ExecuteNonQuery();
                con.Close();
            }
            this.Close();
        }
    }
}
