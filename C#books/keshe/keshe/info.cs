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
    public partial class info : Form
    {
        private string _tag;
        public string _Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;
        public string Name1
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _pw;
        public string PW
        {
            get { return _pw; }
            set { _pw = value; }
        }
        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        private string _pnum;
        public string Pnum
        {
            get { return _pnum; }
            set { _pnum = value; }
        }
        public info()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void info_Load(object sender, EventArgs e)
        {
            textBox1.Text = Name1;
            textBox2.Text = PW;
            if (Gender == "男")
            {
                man.Checked = true;
                wman.Checked = false;
            }
            else
            {
                man.Checked = false;
                wman.Checked = true;
            }
            textBox3.Text = Pnum;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            long anu = 0;
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length != 11 || !long.TryParse(textBox3.Text, out anu))
            {
                if (textBox1.Text.Length == 0) MessageBox.Show("姓名为空");
                else if (textBox2.Text.Length == 0) MessageBox.Show("密码为空");
                else MessageBox.Show("手机号错误");
            }
            else
            {
                string sex;
                if (man.Checked == true) sex = "男";
                else
                {
                    sex = "女";
                }
                SqlConnection sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = "persist security info=false;user id=sa;password=123;initial catalog=library;data source=127.0.0.1";
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlConnection;
                cmd.CommandType = CommandType.Text;
                if (_Tag == "user")
                {
                    cmd.CommandText = "update users set uPwd='" + textBox2.Text + "', uName='" + textBox1.Text + "', uGender='"
                      + sex + "', uPhoNum='" + textBox3.Text + "' where uId='" + Id + "'";
                }
                else
                {
                    cmd.CommandText = "update admin set aPwd='" + textBox2.Text + "', aName='" + textBox1.Text + "', aGender='"
                      + sex + "', aPhoNum='" + textBox3.Text + "' where aId='" + Id + "'";
                }
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
