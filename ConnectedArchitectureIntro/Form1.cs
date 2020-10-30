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
using System.Configuration;

namespace ConnectedArchitectureIntro
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        void Reset()
        {
            textID.Text = textFirstName.Text = textLastName.Text = textEmail.Text = textPhone.Text = "";
            textID.Focus();
        }
        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            Reset();
            string command = "select max(Id) + 1 from customer";
            cmd = new SqlCommand(command, con);
            con.Open();
            string id = cmd.ExecuteScalar().ToString();
            con.Close();
            textID.Text = id;
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string id = textID.Text;
            string firstName = textFirstName.Text;
            string lastName = textLastName.Text;
            string email = textEmail.Text;
            string phone = textPhone.Text;
            string command = string.Format("insert into customer values({0} ,'{1}','{2}','{3}',{4})",id,firstName,lastName,email,phone);
            cmd = new SqlCommand(command, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Reset();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string id = textID.Text;
            string command = "select * from customer where Id ="+id;
            cmd = new SqlCommand(command, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                textFirstName.Text = dr[1].ToString();
                textLastName.Text = dr[2].ToString();
                textEmail.Text = dr[3].ToString();
                textPhone.Text = dr[4].ToString();
            }
            else
            {
                MessageBox.Show("cannot find the recored..");
                Reset();
            }
            dr.Close();
            con.Close();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            string id = textID.Text;
            string firstName = textFirstName.Text;
            string lastName = textLastName.Text;
            string email = textEmail.Text;
            string phone = textPhone.Text;
            string command = string.Format("update customer set Id={0} , FirstName='{1}', LastName= '{2}', EmailAdress='{3}',PhoneNumber= {4}",id,firstName,lastName,email,phone);
            cmd = new SqlCommand(command, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Recored Update..");
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            string id = textID.Text;
            string command = "delete from customer where Id="+id;
            cmd = new SqlCommand(command, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Recored Delete..");
        }
    }
}
