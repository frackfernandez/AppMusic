using Business.Implementations;
using Business.Interfaces;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Presentation
{
    public partial class UILogin : Form
    {
        private readonly IApplicationUser appUser;
        public UILogin()
        {
            InitializeComponent();
            DesignForm();

            appUser = new ApplicationUser();
        }

        private void DesignForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.White;
            this.ShowIcon = false;
            this.Text = string.Empty;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int radius = 20;
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path.AddArc(new Rectangle(panel1.Width - radius, 0, radius, radius), -90, 90);
            path.AddArc(new Rectangle(panel1.Width - radius, panel1.Height - radius, radius, radius), 0, 90);
            path.AddArc(new Rectangle(0, panel1.Height - radius, radius, radius), 90, 90);
            path.CloseFigure();

            panel1.Region = new Region(path);
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text == "" || textBoxPassword.Text == "")
            {
                MessageBox.Show("Incomplete");
                return;
            }

            string name = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            var user = appUser.GetUser(name, password);

            if(user != null)
            {
                UIAlbum album = new UIAlbum(user);
                this.Hide();
                album.FormClosed += (s, args) => this.Close(); // Cierra el formulario actual cuando se cierre el nuevo formulario                         
                album.Show();
            }
            else
            {
                MessageBox.Show("Incorrect");
                textBoxUsername.Text = "";
                textBoxPassword.Text = "";
                return;
            }
        }
    }
}
