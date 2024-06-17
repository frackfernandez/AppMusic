using Business.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class UILogin : Form
    {
        ApplicationUser appUser = new ApplicationUser();
        public UILogin()
        {
            InitializeComponent();
            DesignForm();
        }

        private void UILogin_Load(object sender, EventArgs e)
        {

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

            var listUsers = appUser.ReadUser();

            bool resp = listUsers.Where(x => x.Name == name).Any();

            if (resp)
            {
                var user = listUsers.Where(x => x.Name == name).First();
                if (user.Password == password)
                {
                    if (user.UserType == CrossCutting.Enums.UserType.User)
                    {
                        UIAlbum album = new UIAlbum(user);
                        this.Hide();
                        album.FormClosed += (s, args) => this.Close(); // Cierra el formulario actual cuando se cierre el nuevo formulario                         
                        album.Show();
                    }
                    else
                    {
                        UIAlbum album = new UIAlbum(user);
                        this.Hide();
                        album.FormClosed += (s, args) => this.Close();
                        album.Show();
                    }                    
                }
                else
                {
                    MessageBox.Show("Incorrect");
                    textBoxUsername.Text = "";
                    textBoxPassword.Text = "";
                    return;
                }
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
