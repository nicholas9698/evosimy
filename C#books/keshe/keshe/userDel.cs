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
    public partial class userDel : Form
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _pw;
        public string Pw
        {
            get { return _pw; }
            set { _pw = value; }
        }
        private string _tag;
        public string _Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        public userDel()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userDel_Load(object sender, EventArgs e)
        {
            if(Id == "root")
            {
                MessageBox.Show("root账户无法删除");
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ConnectionString = "persist security info=false;user id=sa;password=123;initial catalog=library;data source=127.0.0.1";
            SqlConnection con = new SqlConnection(ConnectionString);
            if (_Tag == "user")
            {
                string checkpw = "select uPwd from users where uId='" + Id + "'";
                SqlCommand cmd = new SqlCommand(checkpw, con);
                con.Open();
                string pw = cmd.ExecuteScalar().ToString();
                if(pw != textBox1.Text)
                {
                    MessageBox.Show("密码错误");
                    con.Close();
                    this.Close();
                }
                else
                {
                    string deluser = "delete from users where uId='" + Id + "'";
                    cmd = new SqlCommand(deluser, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    System.Environment.Exit(0);
                }
                con.Close();

            }
            else
            {
                string checkpw = "select aPwd from admin where aId='" + Id + "'";
                SqlCommand cmd = new SqlCommand(checkpw, con);
                con.Open();
                string pw = cmd.ExecuteScalar().ToString();
                if (pw != textBox1.Text)
                {
                    MessageBox.Show("密码错误");
                    con.Close();
                    this.Close();
                }
                else
                {
                    string deluser = "delete from admin where aId='" + Id + "'";
                    cmd = new SqlCommand(deluser, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    System.Environment.Exit(0);
                }
                con.Close();

            }
        }
    }
}
