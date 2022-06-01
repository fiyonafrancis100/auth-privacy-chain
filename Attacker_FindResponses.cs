using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace SecureTransferMI
{
    public partial class Attacker_FindResponses : Form
    {
        public Attacker_FindResponses()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "\\Duplicate\\hashfile.txt"))
            {

                string text = File.ReadAllText(Application.StartupPath + "\\temphash\\hashfile.txt", Encoding.UTF8);
                string[] lines = Regex.Split(text, "\r\n");
                for (int i = 0; i < lines.Count(); i++)
                {
                    Responses.Items.Add(lines[i]);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            richTextBox1.Text = Responses.SelectedItem.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string res = GetStringFromHash(Encoding.ASCII.GetBytes(richTextBox1.Text));
            richTextBox2.Text = res;
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();

          //  return Encoding.ASCII.GetString(hash);
        }
    }
}
