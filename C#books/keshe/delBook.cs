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
    public partial class delBook : Form
    {
        public delBook()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Common.Con);
            string BookSearch = "select * from books where bookId=" + textBox1.Text;
            SqlCommand cmd = new SqlCommand(BookSearch, con);
            con.Open();
            if(cmd.ExecuteScalar() == null)
            {
                MessageBox.Show("书籍不存在");
                con.Close();
                this.Close();
            }
            else
            {
                cmd = new SqlCommand("delete from books where bookId=" + textBox1.Text, con);
                cmd.ExecuteNonQuery();
                con.Close();
                this.Close();
            }
        }
    }
}
