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
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //获取账号和密码
            string Id = textBox1.Text;
            string Pwd = textBox2.Text;
            if(Id == ""||Pwd == "")
            {
                MessageBox.Show("请输入账号和密码");
            }
            else
            {
                string user_check;
                if (radioButton1.Checked == true)
                {
                    user_check = "select uName from users where uId='" + Id + "' and uPwd='" + Pwd + "'";
                }
                else
                {
                    user_check = "select aName from admin where aId='" + Id + "' and aPwd='" + Pwd + "'";
                }
                //构造链接对象
                string ConnectionString = "persist security info=false;user id=sa;password=123;initial catalog=library;data source=127.0.0.1";
                SqlConnection sqlConnection = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand(user_check, sqlConnection);
                sqlConnection.Open();
                // 执行查询语句,返回结果集第一行第一列               
                if(cmd.ExecuteScalar() == null)
                {
                    MessageBox.Show("账号或密码错误！", "登陆失败");
                }
                else
                {
                    string name = cmd.ExecuteScalar().ToString();
                    //登录窗体隐藏
                    this.Hide();
                    mainForm Mainform = new mainForm();
                    //将账号传给主窗体mainForm
                    Mainform.Id = Id;
                    if(radioButton1.Checked == true)
                    {
                        Mainform._Tag = "user";
                        Mainform.Name1 = name;
                        Mainform.Id = Id;
                        string uBan_check;
                        uBan_check = "select uBan from users where uId='" + Id + "' and uPwd='" + Pwd + "'";
                        cmd = new SqlCommand(uBan_check, sqlConnection);
                        string b = cmd.ExecuteScalar().ToString();
                        Mainform.B = b;
                    }
                    else
                    {
                        Mainform.Id = Id;
                        Mainform._Tag = "admin";
                        Mainform.Name1 = name;
                        string aBan_check;
                        aBan_check = "select aBan from admin where aId='" + Id + "' and aPwd='" + Pwd + "'";
                        cmd = new SqlCommand(aBan_check, sqlConnection);
                        string b = cmd.ExecuteScalar().ToString();
                        Mainform.B = b;
                        string aPower_check;
                        aPower_check = "select aPower from admin where aId='" + Id + "' and aPwd='" + Pwd + "'";
                        cmd = new SqlCommand(aPower_check, sqlConnection);
                        string p = cmd.ExecuteScalar().ToString();
                        Mainform.Power = p;
                    }
                    //显示主窗体
                    Mainform.ShowDialog();
                    this.Close();
                }
                sqlConnection.Close();
            }
        }

        private void loginForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            userCreate UserCreate = new userCreate();
            UserCreate.AU = 0;
            UserCreate.ShowDialog();
        }
    }
}
