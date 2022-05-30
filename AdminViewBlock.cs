using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.IO;

namespace SecureTransferMI
{
    public partial class AdminViewBlock : Form
    {
        public AdminViewBlock()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (StreamReader fileReader = new StreamReader(Application.StartupPath+ "//bchain.json"))
            {
                txtInput.Text = fileReader.ReadToEnd();
                Deserialize();
            }
        }
        private void Deserialize()
        {
          //  jsonExplorer.Nodes.Clear();
            JavaScriptSerializer js = new JavaScriptSerializer();

            try
            {
                Dictionary<string, object> dic = js.Deserialize<Dictionary<string, object>>(txtInput.Text);

                TreeNode rootNode = new TreeNode("Root");
               // jsonExplorer.Nodes.Add(rootNode);
               // BuildTree(dic, rootNode);
            }
            catch (ArgumentException argE)
            {
               // MessageBox.Show("JSON data is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
