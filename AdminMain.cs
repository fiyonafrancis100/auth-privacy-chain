using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecureTransferMI
{
    public partial class AdminMain : Form
    {
        public AdminMain()
        {
            InitializeComponent();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            AddOfficer obj = new AddOfficer();
            obj.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DeleteOfficer obj = new DeleteOfficer();
            obj.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            UpdateOfficer obj = new UpdateOfficer();
            obj.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ViewUser obj = new ViewUser();
            obj.Show();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            AdminAuthenticate obj = new AdminAuthenticate();
            obj.Show();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            AdminViewBlock obj = new AdminViewBlock();
            obj.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            AdminShareApproval obj = new AdminShareApproval();
            obj.Show();
        }
    }
}
