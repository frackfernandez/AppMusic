namespace Presentation
{
    partial class UIAlbum
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBoxAllPlaylist = new System.Windows.Forms.ListBox();
            this.pictureBoxAdmin = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBoxWeather = new System.Windows.Forms.PictureBox();
            this.labelPlaylist = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelSongNow = new System.Windows.Forms.Label();
            this.labelCurrent = new System.Windows.Forms.Label();
            this.labelTotal = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelAuthorNow = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAdmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWeather)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel1.Controls.Add(this.listBoxAllPlaylist);
            this.panel1.Controls.Add(this.pictureBoxAdmin);
            this.panel1.Controls.Add(this.pictureBox7);
            this.panel1.Controls.Add(this.labelUsername);
            this.panel1.Controls.Add(this.pictureBox6);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(325, 537);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // listBoxAllPlaylist
            // 
            this.listBoxAllPlaylist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.listBoxAllPlaylist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxAllPlaylist.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxAllPlaylist.ForeColor = System.Drawing.Color.Black;
            this.listBoxAllPlaylist.FormattingEnabled = true;
            this.listBoxAllPlaylist.ItemHeight = 22;
            this.listBoxAllPlaylist.Location = new System.Drawing.Point(25, 120);
            this.listBoxAllPlaylist.Name = "listBoxAllPlaylist";
            this.listBoxAllPlaylist.Size = new System.Drawing.Size(275, 374);
            this.listBoxAllPlaylist.TabIndex = 4;
            this.listBoxAllPlaylist.SelectedIndexChanged += new System.EventHandler(this.listBoxAllPlaylist_SelectedIndexChanged);
            // 
            // pictureBoxAdmin
            // 
            this.pictureBoxAdmin.Image = global::Presentation.Properties.Resources.carpeta;
            this.pictureBoxAdmin.Location = new System.Drawing.Point(200, 10);
            this.pictureBoxAdmin.Name = "pictureBoxAdmin";
            this.pictureBoxAdmin.Size = new System.Drawing.Size(40, 40);
            this.pictureBoxAdmin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxAdmin.TabIndex = 3;
            this.pictureBoxAdmin.TabStop = false;
            this.pictureBoxAdmin.Click += new System.EventHandler(this.pictureBoxAdmin_Click);
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::Presentation.Properties.Resources.cerrar_sesion;
            this.pictureBox7.Location = new System.Drawing.Point(270, 10);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(40, 40);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 2;
            this.pictureBox7.TabStop = false;
            this.pictureBox7.Click += new System.EventHandler(this.pictureBox7_Click);
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Font = new System.Drawing.Font("Candara Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsername.Location = new System.Drawing.Point(56, 23);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(91, 26);
            this.labelUsername.TabIndex = 1;
            this.labelUsername.Text = "labeluser";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::Presentation.Properties.Resources.usuario;
            this.pictureBox6.Location = new System.Drawing.Point(10, 10);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(40, 40);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 0;
            this.pictureBox6.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel2.Controls.Add(this.pictureBoxWeather);
            this.panel2.Controls.Add(this.labelPlaylist);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.listBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(747, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(325, 537);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // pictureBoxWeather
            // 
            this.pictureBoxWeather.Image = global::Presentation.Properties.Resources.sol;
            this.pictureBoxWeather.Location = new System.Drawing.Point(185, 10);
            this.pictureBoxWeather.Name = "pictureBoxWeather";
            this.pictureBoxWeather.Size = new System.Drawing.Size(75, 75);
            this.pictureBoxWeather.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxWeather.TabIndex = 2;
            this.pictureBoxWeather.TabStop = false;
            // 
            // labelPlaylist
            // 
            this.labelPlaylist.AutoSize = true;
            this.labelPlaylist.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlaylist.Location = new System.Drawing.Point(25, 135);
            this.labelPlaylist.Name = "labelPlaylist";
            this.labelPlaylist.Size = new System.Drawing.Size(131, 24);
            this.labelPlaylist.TabIndex = 4;
            this.labelPlaylist.Text = "labelplaylist";
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.ForeColor = System.Drawing.Color.Black;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(20, 170);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(285, 345);
            this.listBox1.TabIndex = 3;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(397, 350);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(300, 5);
            this.progressBar1.TabIndex = 6;
            // 
            // labelSongNow
            // 
            this.labelSongNow.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSongNow.Location = new System.Drawing.Point(357, 310);
            this.labelSongNow.Name = "labelSongNow";
            this.labelSongNow.Size = new System.Drawing.Size(380, 24);
            this.labelSongNow.TabIndex = 8;
            this.labelSongNow.Text = "namesong";
            this.labelSongNow.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelCurrent
            // 
            this.labelCurrent.AutoSize = true;
            this.labelCurrent.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrent.Location = new System.Drawing.Point(358, 350);
            this.labelCurrent.Name = "labelCurrent";
            this.labelCurrent.Size = new System.Drawing.Size(34, 16);
            this.labelCurrent.TabIndex = 9;
            this.labelCurrent.Text = "00:00";
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotal.Location = new System.Drawing.Point(703, 350);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(34, 16);
            this.labelTotal.TabIndex = 10;
            this.labelTotal.Text = "00:00";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Presentation.Properties.Resources.cancion;
            this.pictureBox5.Location = new System.Drawing.Point(422, 35);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(250, 250);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 7;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Presentation.Properties.Resources.boton_de_pausa;
            this.pictureBox4.Location = new System.Drawing.Point(525, 475);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(50, 50);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 5;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Presentation.Properties.Resources.boton_de_salto;
            this.pictureBox3.Location = new System.Drawing.Point(581, 475);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(50, 50);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Presentation.Properties.Resources.anterior;
            this.pictureBox2.Location = new System.Drawing.Point(469, 475);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Presentation.Properties.Resources.boton_de_play;
            this.pictureBox1.Location = new System.Drawing.Point(525, 475);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // labelAuthorNow
            // 
            this.labelAuthorNow.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAuthorNow.Location = new System.Drawing.Point(357, 365);
            this.labelAuthorNow.Name = "labelAuthorNow";
            this.labelAuthorNow.Size = new System.Drawing.Size(380, 24);
            this.labelAuthorNow.TabIndex = 11;
            this.labelAuthorNow.Text = "namesauthor";
            this.labelAuthorNow.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // UIAlbum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 561);
            this.Controls.Add(this.labelAuthorNow);
            this.Controls.Add(this.labelTotal);
            this.Controls.Add(this.labelCurrent);
            this.Controls.Add(this.labelSongNow);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "UIAlbum";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.UIAlbum_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAdmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWeather)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxWeather;
        private System.Windows.Forms.Label labelSongNow;
        private System.Windows.Forms.Label labelCurrent;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label labelPlaylist;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBoxAdmin;
        private System.Windows.Forms.ListBox listBoxAllPlaylist;
        private System.Windows.Forms.Label labelAuthorNow;
    }
}

