using System;
using System.Windows.Forms;

namespace Lab03_Bai06
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();

            btnOpenServer.Click += btnOpenServer_Click;
            btnOpenClient.Click += btnOpenClient_Click;
        }

        private void btnOpenServer_Click(object sender, EventArgs e)
        {
            new FormServer().Show();
        }

        private void btnOpenClient_Click(object sender, EventArgs e)
        {
            new FormClient().Show(); // Bấm nhiều lần để giả lập nhiều người dùng
        }
    }
}
