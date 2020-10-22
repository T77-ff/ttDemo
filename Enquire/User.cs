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
using System.Data;
using System.IO;
namespace Enquire
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void User_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strConn = "SERVER=.;DATABASE=Book;USER ID=sa;PASSWORD=123456";
            SqlConnection sqlconn = new SqlConnection(strConn);
            sqlconn.Open();

            string sqlText = string.Format("SELECT * FROM book WHERE Id = '{0}'", this.textBox1.Text);
            SqlCommand sqlcmd = new SqlCommand(sqlText, sqlconn);
            SqlDataReader dr = sqlcmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    this.textBox2.Text = dr["BookName"].ToString();
                    this.textBox3.Text = dr["BookType"].ToString();
                    this.textBox4.Text = dr["BookAuthor"].ToString();
                    this.textBox5.Text = dr["BookPrice"].ToString();
                }
            }
            else
            {
                MessageBox.Show("用户不存在！");
            }

            sqlconn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strConn = "SERVER=.;DATABASE=Book;USER ID=sa;PASSWORD=123456";
            SqlConnection sqlconn = new SqlConnection(strConn);
            sqlconn.Open();

            string sqlText = string.Format("INSERT INTO book (Id,BookName,BookType,BookAuthor,BookPrice)VALUES('{0}','{1}','{2}','{3}','{4}')",
                    this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, this.textBox5.Text
                    );

            SqlCommand sqlcmd = new SqlCommand(sqlText, sqlconn);

            int n = sqlcmd.ExecuteNonQuery();
            if (n > 0)
            {
                MessageBox.Show("添加成功！");
            }
            sqlconn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strConn = "SERVER=.;DATABASE=Book;USER ID=sa;PASSWORD=123456";
            SqlConnection sqlconn = new SqlConnection(strConn);
            sqlconn.Open();

            string sqlText = string.Format("DELETE FROM book WHERE Id='{0}'",
                    this.textBox1.Text
                    );

            SqlCommand sqlcmd = new SqlCommand(sqlText, sqlconn);

            int n = sqlcmd.ExecuteNonQuery();
            if (n > 0)
            {
                MessageBox.Show("删除成功！");
            }
            sqlconn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string strConn = "SERVER=.;DATABASE=Book;USER ID=sa;PASSWORD=123456";
            SqlConnection sqlconn = new SqlConnection(strConn);
            sqlconn.Open();

            string sqlText = string.Format("UPDATE book SET BookName='{0}',BookType='{1}',BookAuthor='{2}',BookPrice='{3}' WHERE Id='{4}'",
                    this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, this.textBox5.Text, this.textBox1.Text
                    );

            SqlCommand sqlcmd = new SqlCommand(sqlText, sqlconn);

            int n = sqlcmd.ExecuteNonQuery();
            if (n > 0)
            {
                MessageBox.Show("更新成功！");
            }
            sqlconn.Close();
        }

        SqlDataAdapter da;
        DataSet ds;

        private void button5_Click(object sender, EventArgs e)
        {
            string strConn = "SERVER=.;DATABASE=Book;USER ID=sa;PASSWORD=123456";
            SqlConnection sqlconn = new SqlConnection(strConn);
            da = new SqlDataAdapter("SELECT * FROM book", sqlconn);
            ds = new DataSet();
            da.Fill(ds, "book");
            this.dataGridView1.DataSource = ds.Tables["book"];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder sb = new SqlCommandBuilder(da);
            da.Update(ds, "book");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string strConn = "SERVER=.;DATABASE=Book;USER ID=sa;PASSWORD=123456";
            SqlConnection sqlconn = new SqlConnection(strConn);
            sqlconn.Open();

            string sqlText = "SELECT Id,BookName,BookType,BookAuthor,BookPrice FROM book";
            SqlCommand sqlcmd = new SqlCommand(sqlText, sqlconn);
            SqlDataReader dr = sqlcmd.ExecuteReader();
            if (dr.HasRows)
            {
                FileStream fs = new FileStream("data.txt", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                while (dr.Read())
                {
                    string line = string.Format("{0}\t{1}\t{2}\t{3}\t{4}",
                        dr["Id"],
                        dr["BookName"],
                        dr["BookType"],
                        dr["BookAuthor"],
                        dr["BookPrice"]);
                    sw.WriteLine(line);
                }
                sw.Close();
                fs.Close();
            }

            sqlconn.Close();
        }
    }
}
