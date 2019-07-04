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
    public partial class lockaccount : Form
    {
        public lockaccount()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int user = 0;
            if (radioButton1.Checked == true) user = 0;
            else user = 1;
            SqlConnection con = new SqlConnection(Common.Con);
            con.Open();
            if(user == 0)
            {
                SqlCommand cmd = new SqlCommand("select * from users where uId='" + textBox1.Text + "'", con);
                if(cmd.ExecuteScalar() == null)
                {
                    con.Close();
                    MessageBox.Show("账户不存在");
                }
                else
                {
                    cmd = new SqlCommand("update users set uBan=" + "1" + " where uId='" + textBox1.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("已锁定");
                }
            }
            else
            {
                if(textBox1.Text == "root") { MessageBox.Show("root账户无法锁定"); }
                else
                {
                    SqlCommand cmd = new SqlCommand("select * from admin where aId='" + textBox1.Text + "'", con);
                    if (cmd.ExecuteScalar() == null)
                    {
                        con.Close();
                        MessageBox.Show("账户不存在");
                    }
                    else
                    {
                        cmd = new SqlCommand("update admin set aBan=" + "1" + " where aId='" + textBox1.Text + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("已锁定");
                    }
                }
                
            }
        }

        private void lockaccount_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int user = 0;
            if (radioButton1.Checked == true) user = 0;
            else user = 1;
            SqlConnection con = new SqlConnection(Common.Con);
            con.Open();
            if (user == 0)
            {
                SqlCommand cmd = new SqlCommand("select * from users where uId='" + textBox1.Text + "'", con);
                if (cmd.ExecuteScalar() == null)
                {
                    con.Close();
                    MessageBox.Show("账户不存在");
                }
                else
                {
                    cmd = new SqlCommand("update users set uBan=" + "0" + " where uId='" + textBox1.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("已解锁");
                }
            }
            else
            {
                if (textBox1.Text == "root") { MessageBox.Show("root账户不存在解锁"); }
                else
                {
                    SqlCommand cmd = new SqlCommand("select * from admin where aId='" + textBox1.Text + "'", con);
                    if (cmd.ExecuteScalar() == null)
                    {
                        con.Close();
                        MessageBox.Show("账户不存在");
                    }
                    else
                    {
                        cmd = new SqlCommand("update admin set aBan=" + "0" + " where aId='" + textBox1.Text + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("已解锁");
                    }
                }

            }
        }
    }
}
