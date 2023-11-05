using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WinformWithADOdata
{
    public partial class Form1 : Form
    {
        private SqlConnection connect;
        public Form1()
        {
            InitializeComponent();
            connect = new SqlConnection("Data Source=DESKTOP-4SK39GD;Initial Catalog=ITacademy;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] queries = textBox1.Text.Split(';');
            foreach (string query in queries)
            {
                TabPage newTab = new TabPage(query);
                tabControl1.TabPages.Add(newTab);
                SqlDataAdapter da = new SqlDataAdapter(query, connect);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ListView lv = new ListView();
                lv.Dock = DockStyle.Fill;
                lv.View = View.Details;
                foreach (DataColumn col in dt.Columns)
                {
                    lv.Columns.Add(col.ColumnName);
                }
                foreach (DataRow row in dt.Rows)
                {
                    ListViewItem item = new ListViewItem(row[0].ToString());
                    for (int i = 1; i < row.ItemArray.Length; i++)
                    {
                        item.SubItems.Add(row[i].ToString());
                    }
                    lv.Items.Add(item);
                }
                newTab.Controls.Add(lv);
                lv.MouseDoubleClick += Lv_MouseDoubleClick;
            }
        }
        private void Lv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView lv = (ListView)sender;
            if (lv.View == View.Details)
            {
                lv.View = View.List;
            }
            else
            {
                lv.View = View.Details;
            }
        }
    }
}
//використовуйте такі команди:
//select * from Students
//select * from Teachers
//select * from K
//select * from Audience

//create table Students
//(
//Id int primary key identity(1,1),
//Name nvarchar(50) not null
//);
//go
//create table K
//(
//Id int primary key identity(1,1),
//Name nvarchar(50) not null
//);
//go
//create table Teachers
//(
//Id int primary key identity(1,1),
//Name nvarchar(50) not null
//);
//go
//create table Audience
//(
//Id int primary key identity(1,1),
//Name nvarchar(50) not null
//);
