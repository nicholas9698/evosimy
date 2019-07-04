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
    public partial class userSearch : Form
    {
        public userSearch()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                infoUser user_info = new infoUser();
                user_info._Tag = "user";
                string info_search = "select * from users where uId='" + textBox1.Text + "'";
                string ConnectionString = "persist security info=false;user id=sa;password=123;initial catalog=library;data source=127.0.0.1";
                SqlConnection sqlConnection = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand(info_search, sqlConnection);
                sqlConnection.Open();
                if(cmd.ExecuteScalar() == null)
                {
                    MessageBox.Show("无此用户信息");
                    sqlConnection.Close();
                    this.Close();
                }
                else
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        user_info.Id = dr[0].ToString();
                        user_info.PW = dr[1].ToString();
                        user_info.Name1 = dr[2].ToString();
                        user_info.Gender = dr[3].ToString();
                        user_info.Pnum = dr[4].ToString();
                    }
                    user_info.ShowDialog();
                    sqlConnection.Close();
                }
            }
            else
            {
                infoUser user_info = new infoUser();
                user_info._Tag = "admin";
                if (textBox1.Text == "root") user_info.Pow = "1";
                else user_info.Pow = "0";
                string info_search = "select * from admin where aId='" + textBox1.Text + "'";
                string ConnectionString = "persist security info=false;user id=sa;password=123;initial catalog=library;data source=127.0.0.1";
                SqlConnection sqlConnection = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand(info_search, sqlConnection);
                sqlConnection.Open();
                if (cmd.ExecuteScalar() == null)
                {
                    MessageBox.Show("无此用户信息");
                    sqlConnection.Close();
                    this.Close();
                }
                else
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        user_info.Id = dr[0].ToString();
                        user_info.PW = dr[1].ToString();
                        user_info.Name1 = dr[2].ToString();
                        user_info.Gender = dr[3].ToString();
                        user_info.Pnum = dr[4].ToString();
                    }
                    user_info.ShowDialog();
                    sqlConnection.Close();
                }
            }
        }
    }
}
