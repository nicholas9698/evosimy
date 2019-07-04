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
    public partial class addBook : Form
    {
        private IList<string> _list;
        public IList<string> _List
        {
            get { return _list; }
            set { _list = value; }
        }
        public addBook()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addBook_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Common.Con);
            SqlCommand cmd = new SqlCommand("Select * from bookclass",con);
            con.Open();
            if(cmd.ExecuteScalar() == null)
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
                while(dr.Read())
                {
                    list1.Add(dr[0].ToString());
                    list2.Add(dr[1].ToString());
                }
                _List = list1;
                comboBox1.DataSource = list2;
                con.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            long anu = 0;
            SqlConnection conn = new SqlConnection(Common.Con);
            SqlCommand cmmd = new SqlCommand("select * from books where bookISBN='" + textBox9.Text + "'", conn);
            conn.Open();
            if (cmmd.ExecuteScalar()!=null||textBox1.Text.Length==0|| textBox2.Text.Length == 0 || textBox3.Text.Length == 0 || textBox9.Text.Length == 0 || textBox4.Text.Length == 0 || textBox5.Text.Length == 0 || textBox6.Text.Length == 0|| textBox8.Text.Length == 0||!long.TryParse(textBox8.Text,out anu))
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
                SqlConnection con = new SqlConnection(Common.Con);
                string AddBook = "insert into books(bookName,bookAuthor,bookPublish,bookISBN,bookOprice,bookVprice,bookSnum,bookContent,bookDcontent,bcNum) " +
                    "values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox9.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox8.Text + "','" + textBox6.Text + "','" + textBox7.Text + "'," +
                    _List[comboBox1.SelectedIndex] + ")";
                SqlCommand cmd = new SqlCommand(AddBook, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                conn.Close();
                this.Close();
            }
            conn.Close();
        }
    }
}
