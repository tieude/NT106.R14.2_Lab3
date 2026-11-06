using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace lab3_bai3
{
    public partial class FormDashboard : Form
    {
        public FormDashboard()
        {
            InitializeComponent();
        }

        private void btnOpenServer_Click(object sender, EventArgs e)
        {
            FormServer serverForm = new FormServer();
            serverForm.Show();
        }

        private void btnOpenClient_Click(object sender, EventArgs e)
        {
            FormClient clientForm = new FormClient();
            clientForm.Show();
        }
    }
}
