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
    public partial class delOrder : Form
    {
        private int _user;
        public int _User
        {
            get { return _user; }
            set { _user = value; }
        }
        private string _id;
        public string _Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _isbn;
        public string _ISBN
        {
            get { return _isbn; }
            set { _isbn = value; }
        }
        private string _onum;
        public string _Onum
        {
            get { return _onum; }
            set { _onum = value; }
        }
        private string _snum;
        public string _Snum
        {
            get { return _snum; }
            set { _snum = value; }
        }
        public delOrder()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(_User == 0)
            {
                SqlConnection con = new SqlConnection(Common.Con);
                string str = "select bookISBN,bookNum from orderinfo where uId='" + _Id + "' and orderNum=" + textBox1.Text;
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();
                if (cmd.ExecuteScalar() == null)
                {
                    MessageBox.Show("用户订单不存在");
                }
                else
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        _ISBN = dr[0].ToString();
                        _Onum = dr[1].ToString();
                    }
                    dr.Close();
                    cmd = new SqlCommand("select * from books where bookISBN='" + _ISBN + "'", con);
                    if (cmd.ExecuteScalar() == null)
                    {
                        string deo = "delete from orderinfo where orderNum=" + textBox1.Text;
                        SqlCommand cmmd = new SqlCommand(deo, con);
                        cmmd.ExecuteNonQuery();
                        MessageBox.Show("本书已下架，订单删除成功");
                        con.Close();
                        this.Close();
                    }
                    else
                    {
                        string bks = "select bookSnum from books where bookISBN='" + _ISBN + "'";
                        SqlCommand cmmd = new SqlCommand(bks, con);
                        SqlDataReader lr = cmmd.ExecuteReader();
                        while (lr.Read())
                        {
                            _Snum = lr[0].ToString();
                        }
                        lr.Close();
                        string deo = "delete from orderinfo where orderNum=" + textBox1.Text;
                        cmmd = new SqlCommand(deo, con);
                        cmmd.ExecuteNonQuery();
                        cmmd = new SqlCommand("update books set bookSnum=" + (int.Parse(_Snum) + int.Parse(_Onum)).ToString() + " where bookISBN='" + _ISBN + "'", con);
                        cmmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                this.Close();
            }
            else
            {
                SqlConnection con = new SqlConnection(Common.Con);
                string str = "select bookISBN,bookNum from orderinfo where orderNum=" + textBox1.Text;
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();
                if (cmd.ExecuteScalar() == null)
                {
                    MessageBox.Show("订单不存在");
                }
                else
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        _ISBN = dr[0].ToString();
                        _Onum = dr[1].ToString();
                    }
                    dr.Close();
                    cmd = new SqlCommand("select * from books where bookISBN='" + _ISBN + "'", con);
                    if (cmd.ExecuteScalar() == null)
                    {
                        string deo = "delete from orderinfo where orderNum=" + textBox1.Text;
                        SqlCommand cmmd = new SqlCommand(deo, con);
                        cmmd.ExecuteNonQuery();
                        MessageBox.Show("本书已下架，订单删除成功");
                        con.Close();
                        this.Close();
                    }
                    else
                    {
                        string bks = "select bookSnum from books where bookISBN='" + _ISBN + "'";
                        SqlCommand cmmd = new SqlCommand(bks, con);
                        SqlDataReader lr = cmmd.ExecuteReader();
                        while (lr.Read())
                        {
                            _Snum = lr[0].ToString();
                        }
                        lr.Close();
                        string deo = "delete from orderinfo where orderNum=" + textBox1.Text;
                        cmmd = new SqlCommand(deo, con);
                        cmmd.ExecuteNonQuery();
                        cmmd = new SqlCommand("update books set bookSnum=" + (int.Parse(_Snum) + int.Parse(_Onum)).ToString() + " where bookISBN='" + _ISBN + "'", con);
                        cmmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                this.Close();
            }
        }
    }
}
