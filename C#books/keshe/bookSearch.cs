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
    public partial class bookSearch : Form
    {
        public bookSearch()
        {
            InitializeComponent();
        }
        public string bInfo ="";
        private void bookSearch_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Common.Text = "";
            string ConnectionString = "persist security info=false;user id=sa;password=123;initial catalog=library;data source=127.0.0.1";
            SqlConnection con = new SqlConnection(ConnectionString);
            if (comboBox1.SelectedIndex == 0)
            {
                string book_check = "Select * from books where bookName='" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(book_check,con);
                con.Open();
                if(cmd.ExecuteScalar() == null)
                {
                    MessageBox.Show("未找到书籍信息");
                    this.Close();
                }
                else
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        bInfo += "商品序号：" + dr[0].ToString() + "\r\n书籍名称：" + dr[1].ToString() +
                            "\r\n作者：" + dr[2].ToString() + "\r\n出版社：" + dr[3].ToString() + "\r\nISBN：" + dr[4].ToString() +
                            "\r\n市场价：" + dr[5].ToString() + "\r\n会员价：" + dr[6].ToString() + "\r\n剩余数量：" + dr[7].ToString() +
                            "\r\n书籍内容：" + dr[8].ToString() + "\r\n详细信息：" + dr[9].ToString() + "\r\n\r\n";
                        MessageBox.Show(bInfo);
                        Common.Text += bInfo;
                        bInfo = "";
                    }
                    this.Close();
                }

                con.Close();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                string book_check = "Select * from books where bookId='" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(book_check, con);
                con.Open();
                if (cmd.ExecuteScalar() == null)
                {
                    MessageBox.Show("未找到书籍信息");
                    this.Close();
                }
                else
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        bInfo += "商品序号：" + dr[0].ToString() + "\r\n书籍名称：" + dr[1].ToString() +
                            "\r\n作者：" + dr[2].ToString() + "\r\n出版社：" + dr[3].ToString() + "\r\nISBN：" + dr[4].ToString() +
                            "\r\n市场价：" + dr[5].ToString() + "\r\n会员价：" + dr[6].ToString() + "\r\n剩余数量：" + dr[7].ToString() +
                            "\r\n书籍内容：" + dr[8].ToString() + "\r\n详细信息：" + dr[9].ToString() + "\r\n\r\n";
                        MessageBox.Show(bInfo);
                        Common.Text += bInfo;
                        bInfo = "";
                    }
                    this.Close();
                }

                con.Close();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                string book_check = "Select * from books where bookContent='" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(book_check, con);
                con.Open();
                if (cmd.ExecuteScalar() == null)
                {
                    MessageBox.Show("未找到书籍信息");
                    this.Close();
                }
                else
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        bInfo += "商品序号：" + dr[0].ToString() + "\r\n书籍名称：" + dr[1].ToString() +
                            "\r\n作者：" + dr[2].ToString() + "\r\n出版社：" + dr[3].ToString() + "\r\nISBN：" + dr[4].ToString() +
                            "\r\n市场价：" + dr[5].ToString() + "\r\n会员价：" + dr[6].ToString() + "\r\n剩余数量：" + dr[7].ToString() +
                            "\r\n书籍内容：" + dr[8].ToString() + "\r\n详细信息：" + dr[9].ToString() + "\r\n\r\n";
                        MessageBox.Show(bInfo);
                        Common.Text += bInfo;
                        bInfo = "";
                    }
                    this.Close();
                }

                con.Close();
            }
        }
    }
}
