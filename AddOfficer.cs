using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.IO;

namespace SecureTransferMI
{
    public partial class AddOfficer : Form
    {
        BaseConnection con = new BaseConnection();
        public AddOfficer()
        {
            InitializeComponent();
        } 

        private void btbbrwse_Click(object sender, EventArgs e)
        {
            OpenFileDialog obj = new OpenFileDialog();
            if (obj.ShowDialog() == DialogResult.OK)
            {
                picbxadd.Image = Image.FromFile(obj.FileName);
                txtbrwse.Text = obj.FileName;
            }

        }
        public string getaccount()
        {
            int c = 0;
            con.dr = con.ret_dr("select isnull(max(id)+1,100) from regtb");
            if (con.dr.Read())
            {
                c = Convert.ToInt32(con.dr[0].ToString());
            }
            return c.ToString();
        }
       
        public void insertdb(String p)
        {

            int status = 0;
            string query = "insert into regtb values(" + textBox1.Text + ",'" + txtdes.Text + "','" + txtcateg.Text + "','" + textBox5.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox9.Text+ "','" + textBox8.Text + "','" + p + "'," + status + ")";
            con.exec1(query);
            MessageBox.Show("Details Added Successfully");

        }
        public void insertlogin()
        {

            int status = 0;
            string query = "insert into login values('" + txtcateg.Text + "','" + textBox5.Text + "','" + status.ToString() + "')";
            con.exec1(query);
           // MessageBox.Show("Details Added Successfully");

        }
        private void btbdesc_Click(object sender, EventArgs e)
        {
            if (txtdes.Text == string.Empty)
            {
                MessageBox.Show("Please enter Name");
            }
            if (txtcateg.Text == string.Empty)
            {
                MessageBox.Show("Please enter Username");
            }
            if (textBox5.Text == string.Empty)
            {
                MessageBox.Show("Please enter Password");
            }
            if (textBox2.Text == string.Empty)
            {
                MessageBox.Show("Please enter Age");
            }
            if (textBox3.Text == string.Empty)
            {
                MessageBox.Show("Please enter Date of Birth");
            }
            if (textBox4.Text == string.Empty)
            {
                MessageBox.Show("Please enter Phone Number");
            }
            if (textBox6.Text == string.Empty)
            {
                MessageBox.Show("Please enter Email ID");
            }
            if (textBox8.Text == string.Empty)
            {
                MessageBox.Show("Please enter Address");
            }
            if (txtbrwse.Text == string.Empty)
            {
                MessageBox.Show("Please browse Photo");
            }
            else
            {

                string paths = Application.StartupPath + "\\Account";

                System.IO.Directory.CreateDirectory(paths);
                string path1 = paths + "\\" + textBox1.Text + ".jpg";
                picbxadd.Image.Save(path1);
                string p = textBox1.Text + ".jpg";

                insertdb(p);
                insertlogin();
                
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                string privateKey = rsa.ToXmlString(true);
                DirectoryInfo dirinfo = Directory.CreateDirectory(Application.StartupPath + "\\userkeys\\" + txtcateg.Text + "\\");
                File.WriteAllText(dirinfo.FullName + "PrivateKey.xml", privateKey);
                string prvpath = "\\userkeys\\" + txtcateg.Text + "\\" + "PrivateKey.xml";
                string publicKey = rsa.ToXmlString(false);
                File.WriteAllText(dirinfo.FullName + "PublicKey.xml", publicKey);
                string pubpath = "\\userkeys\\" + txtcateg.Text + "\\" + "PublicKey.xml";
                textBox1.Text = "";
                txtdes.Text = "";
                txtcateg.Text = "";
                textBox5.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox6.Text = "";
                textBox8.Text = "";
                txtbrwse.Text = "";
                this.Close();
            }


        }

        private void AddOfficer_Load(object sender, EventArgs e)
        {
            textBox1.Text = getaccount();
           


        }

        private void txtdes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || e.KeyChar >= ' ')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtcateg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar >= '/')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
