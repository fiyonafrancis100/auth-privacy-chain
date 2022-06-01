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
    public partial class ChangePwd : Form
    {
        BaseConnection con = new BaseConnection();
        public ChangePwd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "select pwd from login where username='" + Program.uname2 + "'";
                con.dr = con.ret_dr(str);
                if (con.dr.Read())
                {
                    if (told.Text == con.dr[0].ToString())
                    {
                        if (tnew.Text == tnewc.Text)
                        {
                            string st = "update login set pwd='" + tnewc.Text + "' where username='" + Program.uname2 + "'";
                            if (con.exec1(st) > 0)
                            {
                                MessageBox.Show("Password Updated..");
                                tnew.Text = "";
                                tnewc.Text = "";
                                told.Text = "";
                                this.Close();
                            }


                        }
                        else
                        {
                            MessageBox.Show("New Passwords dont match..");
                            tnew.Text = "";
                            tnewc.Text = "";
                        }

                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Occured..");
                told.Text = "";
                tnewc.Text = "";
                tnew.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
