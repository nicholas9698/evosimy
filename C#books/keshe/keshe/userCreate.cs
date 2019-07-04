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
    public partial class userCreate : Form
    {
        private int _au;
        public int AU
        {
            get { return _au; }
            set { _au = value; }
        }
        public userCreate()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userCreate_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Common.Con);
            conn.Open();
            long anu;
            if(textBox1.Text.Length==0 || textBox2.Text.Length==0||textBox3.Text.Length==0||textBox4.Text.Length !=11 || !long.TryParse(textBox4.Text, out anu))
            {
                if(textBox1.Text.Length == 0) MessageBox.Show("用户名为空");
                else if (textBox2.Text.Length == 0) MessageBox.Show("密码为空");
                else if (textBox3.Text.Length == 0) MessageBox.Show("姓名为空");
                else MessageBox.Show("手机号错误");
            }
            else
            {

                string ConnectionString = "persist security info=false;user id=sa;password=123;initial catalog=library;data source=127.0.0.1";
                SqlConnection con = new SqlConnection(ConnectionString);
                string gender;
                if (man.Checked == true) gender = "男";
                else gender = "女";
                string add_user = "";
                bool no_ex = true;
                if (AU == 0)
                {
                    SqlCommand cmmd = new SqlCommand("select * from users where uId='" + textBox1.Text + "'", conn);
                    if (cmmd.ExecuteScalar() != null) { no_ex = false; MessageBox.Show("已存在的用户名"); }
                    else
                    {
                       add_user = "insert into users(uId,uPwd,uName,uGender,uPhoNum,uBan) values('" +
                       textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + gender + "','" +
                       textBox4.Text + "',0)";
                    }
                }
                else
                {
                    SqlCommand cmmd = new SqlCommand("select * from admin where aId='" + textBox1.Text + "'", conn);
                    if (cmmd.ExecuteScalar() != null) { no_ex = false; MessageBox.Show("已存在的用户名"); }
                    else
                    {
                        add_user = "insert into admin(aId,aPwd,aName,aGender,aPhoNum,aPower,aBan) values('" +
                        textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + gender + "','" +
                        textBox4.Text + "',0,0)";
                    }
                }
                if(no_ex)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(add_user, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    this.Close();
                }
                conn.Close();   
            }
        }
    }
}
