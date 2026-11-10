using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
   
    public class FormBai5 : Form
    {
        public FormBai5()
        {
            Text = "Bai5 - Hôm nay ăn gì?";
            ClientSize = new Size(320, 140);
            StartPosition = FormStartPosition.CenterScreen;

            Button btnServer = new Button();
            btnServer.Text = "Open TCP Server";
            btnServer.Location = new Point(30, 20);
            btnServer.Size = new Size(260, 40);
            btnServer.Click += delegate { new Bai5Server().Show(); };

            Button btnClient = new Button();
            btnClient.Text = "Open new TCP Client";
            btnClient.Location = new Point(30, 80);
            btnClient.Size = new Size(260, 40);
            btnClient.Click += delegate { new Bai5Client().Show(); };

            Controls.Add(btnServer);
            Controls.Add(btnClient);
        }
    }
}
