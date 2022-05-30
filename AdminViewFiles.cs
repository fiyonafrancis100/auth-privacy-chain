using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace SecureTransferMI
{
    public partial class AdminViewFiles : Form
    {
        BaseConnection con = new BaseConnection();
        public AdminViewFiles()
        {
            InitializeComponent();
        }
        public void fileid()
        {

            con.dr = con.ret_dr("select * from datatb where status=1");
            while (con.dr.Read())
            {

                
                    listBox2.Items.Add(con.dr[2].ToString());
                

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ss = decode(richTextBox1.Text);
            richTextBox1.Text = "";
            richTextBox1.Text = ss.ToString();
        }

        private void AdminViewFiles_Load(object sender, EventArgs e)
        {
           
            fileid();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string text = File.ReadAllText(Application.StartupPath + "\\Files\\" + listBox2.SelectedItem.ToString());

                richTextBox1.Text = text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Not Proper Selection");
            }
        }
        public static string decode(string text)
        {
            byte[] mybyte = System.Convert.FromBase64String(text);
            string returntext = System.Text.Encoding.UTF8.GetString(mybyte);
            return returntext;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
