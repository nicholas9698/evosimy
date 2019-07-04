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
    public partial class updateBook : Form
    {
        private IList<string> _list;
        public IList<string> _List
        {
            get { return _list; }
            set { _list = value; }
        }
        private IList<string> _Slist;
        public IList<string> _SList
        {
            get { return _Slist; }
            set { _Slist = value; }
        }
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public updateBook()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            long anu = 0;
            SqlConnection conn = new SqlConnection(Common.Con);
            SqlCommand cmmd = new SqlCommand("select * from books where bookISBN='" + textBox9.Text + "' and bookId!='"+Id+"'", conn);
            conn.Open();
            if (cmmd.ExecuteScalar()!=null||textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0 || textBox9.Text.Length == 0 || textBox4.Text.Length == 0 || textBox5.Text.Length == 0 || textBox6.Text.Length == 0 || textBox8.Text.Length == 0 || !long.TryParse(textBox8.Text, out anu))
            {
                if (cmmd.ExecuteScalar() != null) MessageBox.Show("已存在书籍ISBN");
                else if (textBox1.Text.Length == 0) MessageBox.Show("图书名称为空");
                else if (textBox2.Text.Length == 0) MessageBox.Show("作者为空");
                else if (textBox3.Text.Length == 0) MessageBox.Show("出版社为空");
                else if (textBox9.Text.Length == 0) MessageBox.Show("ISBN为空");
                else if (textBox4.Text.Length == 0) MessageBox.Show("市场价为空");
                else if (textBox5.Text.Length == 0) MessageBox.Show("会员价为空");
                else if (textBox6.Text.Length == 0) MessageBox.Show("图书内容为空");
                else if (textBox8.Text.Length == 0) MessageBox.Show("数量为空");
                else MessageBox.Show("数量错误");
            }
            else
            {
                int i = 0;
                for (i = 0; i < _SList.Count; i++)
                {
                    if (comboBox1.Text == _SList[i]) break;
                }
                SqlConnection con = new SqlConnection(Common.Con);
                string UpdateBook = "update books set bookName='" + textBox1.Text + "',bookAuthor='" + textBox2.Text + "',bookPublish='" + textBox3.Text +
                    "',bookISBN='" + textBox9.Text + "',bookOprice='" + textBox4.Text + "',bookVprice='" + textBox5.Text +
                    "',bookSnum='" + textBox8.Text + "',bookContent='" + textBox6.Text + "',bookDcontent='" + textBox7.Text + "',bcNum='" + _List[i] +
                    "' where bookId=" + Id;
                SqlCommand cmd = new SqlCommand(UpdateBook, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                conn.Close();
                this.Close();
            }
            conn.Close();
        }

        private void updateBook_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Common.Con);
            SqlCommand cmd = new SqlCommand("Select * from bookclass", con);
            con.Open();
            if (cmd.ExecuteScalar() == null)
            {
                MessageBox.Show("无图书分类，请先添加图书分类");
                con.Close();
                this.Close();
            }
            else
            {
                IList<string> list1 = new List<string>();
                IList<string> list2 = new List<string>();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list1.Add(dr[0].ToString());
                    list2.Add(dr[1].ToString());
                }
                dr.Close();
                comboBox1.DataSource = list2;
                _List = list1;
                _SList = list2;
                string BookSearch = "select * from books where bookId=" + Id;
                SqlCommand cmmd = new SqlCommand(BookSearch, con);
                dr = cmmd.ExecuteReader();
                while(dr.Read())
                {
                    textBox1.Text = dr[1].ToString();
                    textBox2.Text = dr[2].ToString();
                    textBox3.Text = dr[3].ToString();
                    textBox9.Text = dr[4].ToString();
                    textBox4.Text = dr[5].ToString();
                    textBox5.Text = dr[6].ToString();
                    textBox8.Text = dr[7].ToString();
                    textBox6.Text = dr[8].ToString();
                    textBox7.Text = dr[9].ToString();
                    int i;
                    for(i =0;i<list2.Count;i++)
                    {
                        if (list1[i] == dr[10].ToString()) break;
                    }
                    if (i != list2.Count) comboBox1.SelectedIndex = i;
                    else comboBox1.SelectedIndex = -1;
                }
                dr.Close();

            }
            con.Close();
        }
    }
}
