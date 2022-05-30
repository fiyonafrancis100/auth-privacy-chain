using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SecureTransferMI
{
    public partial class AdminShareApproval : Form
    {
        BaseConnection con = new BaseConnection();
        BaseConnection ob = new BaseConnection();
        public static string shareid = "";
        public AdminShareApproval()
        {
            InitializeComponent();
        }
        public void fillcombo()
        {

            con.dr = con.ret_dr("select fid from sharetb where status=0");

            while (con.dr.Read())
            {
                comboBox1.Items.Add(con.dr[0].ToString());
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.dr = con.ret_dr("select * from sharetb where fid='" + comboBox1.SelectedItem.ToString() + "' and status=0");

            if (con.dr.Read())
            {
                textBox9.Text = con.dr[1].ToString();
                textBox2.Text = con.dr[6].ToString();
                textBox7.Text = con.dr[4].ToString();
                shareid = con.dr[0].ToString();
            }
        }
        public void updatedb()
        {
           // MessageBox.Show(shareid.ToString());
            ob.exec("update sharetb set status=1 where sid=" + shareid.ToString() + "");
        }
        public void insertdb()
        {

            int status = 0;

            string query = "insert into transdb values(" + textBox6.Text + "," + comboBox1.SelectedItem.ToString() + ",'" + textBox9.Text + "'," + textBox1.Text + ",'" + textBox2.Text + "'," + status + ",'" + textBox3.Text + "')";
            con.exec1(query);
            MessageBox.Show("Transcation Successfully");

        }
        public string getaccount()
        {
            int c = 0;
            con.dr = con.ret_dr("select isnull(max(transid)+1,500) from transdb");
            if (con.dr.Read())
            {
                c = Convert.ToInt32(con.dr[0].ToString());
            }
            return c.ToString();
        }
        private void AdminShareApproval_Load(object sender, EventArgs e)
        {
            fillcombo();
            textBox3.Text = DateTime.Now.ToString();
            textBox6.Text = getaccount();
        }

        private void btbdesc_Click(object sender, EventArgs e)
        {
            string str = textBox7.Text;

            int count = 0;
            int chunkSize = 100;
            int stringLength = str.Length;
            for (int i = 0; i < stringLength; i += chunkSize)
            {
                count++;
                if (i + chunkSize > stringLength)
                {
                    chunkSize = stringLength - i;
                    str.Substring(i, chunkSize);

                }

            }
            textBox1.Text = count.ToString();
            insertdb();
            updatedb();
            this.Close();
        }
    }
}
