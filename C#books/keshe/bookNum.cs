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
    public partial class bookNum : Form
    {
        public bookNum()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateBook UpdateBook = new updateBook();
            UpdateBook.Id = textBox1.Text;
            SqlConnection con = new SqlConnection(Common.Con);
            SqlCommand cmd = new SqlCommand("Select * from books where bookId='"+textBox1.Text+"'", con);
            con.Open();
            if(cmd.ExecuteScalar() == null)
            {
                MessageBox.Show("书籍不存在");
                con.Close();
                this.Close();
            }
            else
            {
                con.Close();
                UpdateBook.ShowDialog();
                this.Close();
            }
            
        }
    }
}
