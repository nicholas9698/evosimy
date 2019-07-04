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
    public partial class buy : Form
    {
        private string _userpho;
        public string _UserPho
        {
            get { return _userpho; }
            set { _userpho = value; }
        }
        private int _state;
        public int _State
        {
            get { return _state; }
            set { _state = value; }
        }
        private string _isbn;
        public string _ISBN
        {
            get { return _isbn; }
            set { _isbn = value; }
        }
        private string _booknum;
        public string _BookNum
        {
            get { return _booknum; }
            set { _booknum = value; }
        }
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public buy()
        {
            InitializeComponent();
        }

        private void buy_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Common.Con);
            string str = "select uPhoNum from users where uId='" + Id + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            _UserPho = cmd.ExecuteScalar().ToString();
            con.Close();
            comboBox2.SelectedIndex = 1;
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int num = 0;
            if(int.TryParse(textBox5.Text,out num)&& num != 0)
            {
                SqlConnection con = new SqlConnection(Common.Con);
                string BookSerach = "select bookName, bookAuthor, bookPublish, bookISBN, bookVprice, bookSnum from books where bookId=" +
                    textBox4.Text;
                SqlCommand cmd = new SqlCommand(BookSerach, con);
                con.Open();
                if (cmd.ExecuteScalar() == null)
                {
                    MessageBox.Show("书籍不存在");
                }
                else
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        textBox1.Text = dr[0].ToString();
                        textBox2.Text = dr[1].ToString();
                        textBox3.Text = dr[2].ToString();
                        textBox7.Text = dr[3].ToString();
                        _ISBN = dr[3].ToString();
                        textBox6.Text = (double.Parse(dr[4].ToString()) * int.Parse(textBox5.Text)).ToString();
                        _BookNum = dr[5].ToString();
                    }
                    if(int.Parse(textBox5.Text) > int.Parse(_BookNum))
                    {
                        MessageBox.Show("库存不足");
                    }
                    else
                    {
                        _State = 1;
                    }
                }
            }
            else
            {
                MessageBox.Show("数量错误");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string _orderMath, _payMath;
            if (comboBox2.SelectedIndex == 0) _payMath = "网银";
            else if (comboBox2.SelectedIndex == 1) _payMath = "支付宝";
            else _payMath = "微信";
            if (comboBox1.SelectedIndex == 0) _orderMath = "快递上门";
            else _orderMath = "线下书店自提";
            if(_State != 1)
            {
                MessageBox.Show("订单信息错误");
            }
            else
            {
                string AddOrder = "insert into orderinfo(uId,uPhoNum,uAddress,orderValue,bookName,bookAuthor,bookPublish,bookISBN,bookNum,orderState,orderMath,payMath,orderTime) " +
                    "values('" + Id + "','" + _UserPho + "','" + textBox8.Text + "',"+textBox6.Text+",'" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox7.Text + "'," + textBox5.Text + ",'未完成','" + _orderMath + "','" + _payMath + "','"+DateTime.Now.ToString()+"')";
                SqlConnection con = new SqlConnection(Common.Con);
                SqlCommand cmd = new SqlCommand(AddOrder, con);
                con.Open();
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("update books set bookSnum="+(int.Parse(_BookNum)-int.Parse(textBox5.Text)).ToString()+" where bookISBN='"+_ISBN+"'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("下单成功");
                this.Close();
            }
        }
    }
}
