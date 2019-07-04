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
    public partial class orderSearch : Form
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _tag; //权限
        public string _Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        public orderSearch()
        {
            InitializeComponent();
        }

        private void orderSearch_Load(object sender, EventArgs e)
        {
            if(_Tag == "user")
            {
                textBox1.ReadOnly = true;
                textBox1.Text = Id;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string state = "";
            if (radioButton1.Checked == true) state = "未完成";
            else state = "已完成";
            string str = "select * from orderinfo where uId='" + textBox1.Text + "' and orderState='" + state + "'";
            SqlConnection con = new SqlConnection(Common.Con);
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            if(cmd.ExecuteScalar() == null)
            {
                MessageBox.Show("订单不存在");
                con.Close();
                this.Close();
            }
            else
            {
                SqlDataReader dr = cmd.ExecuteReader();
                string res = "";
                while(dr.Read())
                {
                    res += ("订单号：" + dr[0].ToString()+"\r\n");
                    res += ("用户：" + dr[1].ToString() + "\r\n");
                    res += ("手机号：" + dr[2].ToString() + "\r\n");
                    res += ("收货地址：" + dr[3].ToString() + "\r\n");
                    res += ("总价：" + dr[4].ToString() + "\r\n");
                    res += ("书籍名：" + dr[5].ToString() + "\r\n");
                    res += ("作者：" + dr[6].ToString() + "\r\n");
                    res += ("出版社：" + dr[7].ToString() + "\r\n");
                    res += ("ISBN：" + dr[8].ToString() + "\r\n");
                    res += ("数量：" + dr[9].ToString() + "\r\n");
                    res += ("订单状态：" + dr[10].ToString() + "\r\n");
                    res += ("配送方式：" + dr[11].ToString() + "\r\n");
                    res += ("支付方式：" + dr[12].ToString() + "\r\n");
                    res += ("订单时间：" + dr[13].ToString() + "\r\n\r\n");
                    Common.Text += res;
                    res = "";
                }
                dr.Close();
                con.Close();
                this.Close();
            }
        }
    }
}
