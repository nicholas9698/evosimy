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
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private string _id; //账号
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;   //姓名
        public string Name1
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _tag; //权限
        public string _Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        private string _power;
        public string Power
        {
            get { return _power; }
            set { _power = value; }
        }
        private string _b;  //锁定标记 1为锁定
        public string B
        {
            get { return _b; }
            set { _b = value; }
        }
        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help h = new help();
            h.ShowDialog();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            if (Power != "1")
            {
                超级管理员ToolStripMenuItem.Enabled = false;
                管理员ToolStripMenuItem.Enabled = false;
            }
            if (B == "1")
            {
                系统管理ToolStripMenuItem.Enabled = false;
                信息查询ToolStripMenuItem.Enabled = false;
                书籍管理ToolStripMenuItem.Enabled = false;
                订单管理ToolStripMenuItem.Enabled = false;
                分类管理ToolStripMenuItem.Enabled = false;
            }
            if (_Tag == "user")
            {
                ttsl1.Text = Name1;
                ttsl2.Text = "权限级别：普通用户";
                if (B == "1")
                {
                    ttsl3.Text = " 状态：被锁定";
                    MessageBox.Show("账户被锁定，请尽快联系管理员", "警告");
                }
                新建用户ToolStripMenuItem.Enabled = false;
                其他用户ToolStripMenuItem.Enabled = false;
                书籍管理ToolStripMenuItem.Enabled = false;
                订单删除ToolStripMenuItem.Enabled = false;
                订单修改ToolStripMenuItem.Enabled = false;
                分类管理ToolStripMenuItem1.Enabled = false;
                所有订单ToolStripMenuItem.Enabled = false;
                所有用户ToolStripMenuItem.Enabled = false;
            }
            else
            {
                购买书籍ToolStripMenuItem.Enabled = false;
                取消订单ToolStripMenuItem.Enabled = false;
                ttsl1.Text = Name1;
                if (Power == "1")
                { ttsl2.Text = "权限级别：超级管理员"; }
                else
                { ttsl2.Text = "权限级别：管理员用户"; }
                if (B == "1")
                {
                    ttsl3.Text = "状态：被锁定";
                    MessageBox.Show("账户被锁定，请尽快联系超级管理员", "警告");
                }
            }
        }
        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ttsl4.Text = DateTime.Now.ToString();
        }
        //退出时关闭所有窗口
        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void 用户信息修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_Tag == "user")
            {
                info user_info = new info();
                user_info._Tag = _Tag;
                string info_search = "select * from users where uId='" + Id + "'";
                string ConnectionString = "persist security info=false;user id=sa;password=123;initial catalog=library;data source=127.0.0.1";
                SqlConnection sqlConnection = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand(info_search, sqlConnection);
                sqlConnection.Open();
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
            else
            {
                info user_info = new info();
                user_info._Tag = _Tag;
                string info_search = "select * from admin where aId='" + Id + "'";
                string ConnectionString = "persist security info=false;user id=sa;password=123;initial catalog=library;data source=127.0.0.1";
                SqlConnection sqlConnection = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand(info_search, sqlConnection);
                sqlConnection.Open();
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

        private void 新建用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 注销用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userDel uDel = new userDel();
            uDel.Id = Id;
            uDel._Tag = _Tag;
            uDel.ShowDialog();
        }

        private void 用户信息查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userCreate ucreate = new userCreate();
            ucreate.AU = 0;
            ucreate.ShowDialog();
        }

        private void 管理员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userCreate ucreate = new userCreate();
            ucreate.AU = 1;
            ucreate.ShowDialog();
        }

        private void 书籍查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bookSearch BookSearch = new bookSearch();
            BookSearch.ShowDialog();
            textBox1.Text = Common.Text;
            Common.Text = "";
        }

        private void 订单查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            orderSearch OrderSearch = new orderSearch();
            OrderSearch.Id = Id;
            OrderSearch._Tag = _Tag;
            OrderSearch.ShowDialog();
            textBox1.Text = Common.Text;
            Common.Text = "";
        }

        private void 新书入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addBook AddBook = new addBook();
            AddBook.ShowDialog();
        }

        private void 书籍修改删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bookNum BookNum = new bookNum();
            BookNum.ShowDialog();
        }

        private void 分类管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delBook DelBook = new delBook();
            DelBook.ShowDialog();
        }

        private void 取消订单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delOrder DelOrder = new delOrder();
            DelOrder._Id = Id;
            DelOrder._User = 0;
            DelOrder.ShowDialog();
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newBookClass NewBookClass = new newBookClass();
            NewBookClass.ShowDialog();
        }

        private void 购买书籍ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buy Buy = new buy();
            Buy.Id = Id;
            Buy.ShowDialog();
        }

        private void 指定删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delOrder DelOrder = new delOrder();
            DelOrder._User = 1;
            DelOrder.ShowDialog();
        }

        private void 订单修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            orderState OrderState = new orderState();
            OrderState.ShowDialog();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delBookClass DelBookClass = new delBookClass();
            DelBookClass.ShowDialog();
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateBookClass UpadteBookClass = new updateBookClass();
            UpadteBookClass.ShowDialog();
        }

        private void 此用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_Tag == "user")
            {
                infoUser user_info = new infoUser();
                user_info._Tag = _Tag;
                string info_search = "select * from users where uId='" + Id + "'";
                string ConnectionString = "persist security info=false;user id=sa;password=123;initial catalog=library;data source=127.0.0.1";
                SqlConnection sqlConnection = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand(info_search, sqlConnection);
                sqlConnection.Open();
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
            else
            {
                infoUser user_info = new infoUser();
                user_info._Tag = _Tag;
                user_info.Pow = Power;
                string info_search = "select * from admin where aId='" + Id + "'";
                string ConnectionString = "persist security info=false;user id=sa;password=123;initial catalog=library;data source=127.0.0.1";
                SqlConnection sqlConnection = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand(info_search, sqlConnection);
                sqlConnection.Open();
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

        private void 其他用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            userSearch UserSearch = new userSearch();
            UserSearch.ShowDialog();
        }

        private void 完成订单删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            orderdelSelect od = new orderdelSelect();
            od.ShowDialog();
        }

        private void 用户锁定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lockaccount LockAccount = new lockaccount();
            LockAccount.ShowDialog();
        }

        private void 用户删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            adminDelete AdminDelete = new adminDelete();
            AdminDelete.ShowDialog();
        }

        private void 所有书籍ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Common.Con);
            SqlCommand cmd = new SqlCommand("select * from books", con);
            con.Open();
            if (cmd.ExecuteScalar() == null)
            {
                Common.Text = "无书籍";
                con.Close();
            }
            else
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Common.Text += "商品序号：" + dr[0].ToString() + "\r\n书籍名称：" + dr[1].ToString() +
                        "\r\n作者：" + dr[2].ToString() + "\r\n出版社：" + dr[3].ToString() + "\r\nISBN：" + dr[4].ToString() +
                        "\r\n市场价：" + dr[5].ToString() + "\r\n会员价：" + dr[6].ToString() + "\r\n剩余数量：" + dr[7].ToString() +
                        "\r\n书籍内容：" + dr[8].ToString() + "\r\n详细信息：" + dr[9].ToString() + "\r\n\r\n";
                }
            }
            textBox1.Text = Common.Text;
            Common.Text = "";
        }

        private void 所有订单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Common.Con);
            SqlCommand cmd = new SqlCommand("select * from orderinfo", con);
            con.Open();
            if (cmd.ExecuteScalar() == null)
            {
                Common.Text = "无订单信息";
                con.Close();
            }
            else
            {
                SqlDataReader dr = cmd.ExecuteReader();
                string res = "";
                while (dr.Read())
                {
                    res += ("订单号：" + dr[0].ToString() + "\r\n");
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
            }
            textBox1.Text = Common.Text;
            Common.Text = "";
        }

        private void 所有用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Common.Con);
            SqlCommand cmd = new SqlCommand("select * from users", con);
            con.Open();
            if (cmd.ExecuteScalar() == null)
            {
                Common.Text = "无用户";
                con.Close();
            }
            else
            {
                SqlDataReader dr = cmd.ExecuteReader();
                string res = "";
                while (dr.Read())
                {
                    res += ("id：" + dr[0].ToString() + "\r\n");
                    res += ("密码：" + dr[1].ToString() + "\r\n");
                    res += ("姓名：" + dr[2].ToString() + "\r\n");
                    res += ("性别：" + dr[3].ToString() + "\r\n");
                    res += ("手机号：" + dr[4].ToString() + "\r\n");
                    res += ("锁定状态：" + dr[5].ToString() + "\r\n\r\n");
                    Common.Text += res;
                    res = "";
                }
                dr.Close();
                con.Close();
            }
            textBox1.Text = Common.Text;
            Common.Text = "";
        }

        private void 所有管理员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Common.Con);
            SqlCommand cmd = new SqlCommand("select * from admin", con);
            con.Open();
            if (cmd.ExecuteScalar() == null)
            {
                Common.Text = "无用户";
                con.Close();
            }
            else
            {
                SqlDataReader dr = cmd.ExecuteReader();
                string res = "";
                while (dr.Read())
                {
                    res += ("id：" + dr[0].ToString() + "\r\n");
                    res += ("密码：" + dr[1].ToString() + "\r\n");
                    res += ("姓名：" + dr[2].ToString() + "\r\n");
                    res += ("性别：" + dr[3].ToString() + "\r\n");
                    res += ("手机号：" + dr[4].ToString() + "\r\n");
                    res += ("权限(1 超级管理员 / 0 普通管理员)：" + dr[5].ToString() + "\r\n");
                    res += ("锁定状态：" + dr[6].ToString() + "\r\n\r\n");
                    Common.Text += res;
                    res = "";
                }
                dr.Close();
                con.Close();
            }
            textBox1.Text = Common.Text;
            Common.Text = "";
        }
    }
}
