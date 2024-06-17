using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Business.Implementations;
using CrossCutting.DTO;
using NAudio.Wave;

namespace Presentation
{
    public partial class UIAlbum : Form
    {
        ApplicationPlaylist appPlaylist = new ApplicationPlaylist();
        ApplicationSong appSong = new ApplicationSong();
        ApplicationAuthor appAuthor = new ApplicationAuthor();
        ApplicationServiceWeather ASWeather = new ApplicationServiceWeather();

        Random random = new Random();

        UIAdmin adminForm = new UIAdmin();

        private IWavePlayer waveOut;
        private AudioFileReader audioFileReader;
        private bool isPaused = false;
        private Timer progressTimer;

        int songindex = -1;
        int playlistindex = -1;

        public UIAlbum(User user)
        {
            InitializeComponent();
            DesignForm();

            pictureBox4.Visible = false; // boton pausa            

            SetUser(user);
            SetWeather();            
            SetAllPlaylist();

            progressTimer = new Timer();
            progressTimer.Interval = 100; // 100 milisegundos
            progressTimer.Tick += new EventHandler(UpdateProgressBar); // suscribirse a eventos

            playlistindex = SelectPlaylistWeather();
            ShowPlaylist(playlistindex);

            labelSongNow.Text = GetNameSong(GetPathSong(listBox1.Items[0].ToString()));
            labelAuthorNow.Text = GetNameAuthor(GetPathSong(listBox1.Items[0].ToString()));
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
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            int radius = 20;
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path.AddArc(new Rectangle(panel2.Width - radius, 0, radius, radius), -90, 90);
            path.AddArc(new Rectangle(panel2.Width - radius, panel2.Height - radius, radius, radius), 0, 90);
            path.AddArc(new Rectangle(0, panel2.Height - radius, radius, radius), 90, 90);
            path.CloseFigure();

            panel2.Region = new Region(path);
        }

        private void UIAlbum_Load(object sender, EventArgs e)
        {

        }

        private void SetUser(User user)
        {
            labelUsername.Text = user.Name;
            if (user.UserType == CrossCutting.Enums.UserType.User) 
            {
                pictureBoxAdmin.Visible = false;
            }
        }
        private void SetWeather()
        {
            string tem = ASWeather.GetTemp();
            string wea = ASWeather.GetWeather();
            label1.Text = wea.ToUpper();
            label2.Text = $"{tem}°C";

            if (wea.ToLower() == "clouds")
            {
                pictureBoxWeather.Image = Presentation.Properties.Resources.nubes;
            }
            else if (wea.ToLower() == "rain")
            {
                pictureBoxWeather.Image = Presentation.Properties.Resources.gotas_de_lluvia;
            }
            else if (wea.ToLower() == "wind")
            {
                pictureBoxWeather.Image = Presentation.Properties.Resources.viento;
            }
            else if (wea.ToLower() == "snow")
            {
                pictureBoxWeather.Image = Presentation.Properties.Resources.nevando;
            }
            else if (wea.ToLower() == "clear")
            {
                pictureBoxWeather.Image = Presentation.Properties.Resources.sol;
            }
        }
        private void SetAllPlaylist()
        {
            var list = appPlaylist.ReadPlaylist();

            foreach (var item in list)
            {
                listBoxAllPlaylist.Items.Add(item.Weather.Code + "  -  " +item.Name);
            }
        }
        private int SelectPlaylistWeather()
        {
            var weather = ASWeather.GetWeather();
            var list = appPlaylist.ReadPlaylist().Where(x => x.Weather.Code == weather).ToList();

            int total = list.Count();
            int select = random.Next(0, total);

            var playlist = list[select].Id;
            return playlist;
        }
        private void ShowPlaylist(int id)
        {
            listBox1.Items.Clear();
            
            var playlist = appPlaylist.GetPlaylist(id);

            labelPlaylist.Text = playlist.Name;

            var songs = playlist.Songs;

            foreach (var song in songs)
            {
                listBox1.Items.Add(song.Name + " - " + song.Author.Name);
            }
        }

        private void Play(string path)
        {
            if (waveOut == null)
            {
                waveOut = new WaveOutEvent();
                audioFileReader = new AudioFileReader(path);
                waveOut.Init(audioFileReader);

                waveOut.PlaybackStopped += OnPlaybackStopped;

                waveOut.Play();
                isPaused = false;
                progressBar1.Maximum = (int)audioFileReader.TotalTime.TotalMilliseconds;
                labelTotal.Text = audioFileReader.TotalTime.ToString(@"mm\:ss");
                progressTimer.Start();

                labelSongNow.Text = GetNameSong(path);
                labelAuthorNow.Text = GetNameAuthor(path);
                pictureBox1.Visible = false;
                pictureBox4.Visible = true;
            }
            else if (isPaused)
            {
                waveOut.Play();
                isPaused = false;
                progressTimer.Start();
                pictureBox1.Visible = false;
                pictureBox4.Visible = true;
            }
        }
        private void Stop()
        {
            if (waveOut != null && waveOut.PlaybackState == PlaybackState.Playing)
            {
                waveOut.Pause();
                isPaused = true;
                progressTimer.Stop();
                pictureBox4.Visible = false;
                pictureBox1.Visible = true;
            }
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            // libera los recursos del archivo de audio y reproductor
            if (audioFileReader != null)
            {
                audioFileReader.Dispose();
                audioFileReader = null;
            }
            if (waveOut != null)
            {
                waveOut.Dispose();
                waveOut = null;
            }

            // Siguiente cancion
            int total = listBox1.Items.Count - 1;

            if (songindex == total)
            {
                SetWeather();

                // selecciona la nueva playlist
                int now = SelectPlaylistWeather();
                if (playlistindex == now)
                    now = SelectPlaylistWeather();

                ShowPlaylist(now);

                songindex = 0;

                listBox1.SelectedIndex = 0;
                Play(GetPathSong(listBox1.Items[0].ToString()));
            }
            else if (songindex < total)
            {
                int now = songindex + 1;
                songindex = now;

                listBox1.SelectedIndex = now;
                Play(GetPathSong(listBox1.Items[now].ToString()));
            }

        }
        private void UpdateProgressBar(object sender, EventArgs e)
        {
            if (waveOut.PlaybackState == PlaybackState.Playing)
            {
                progressBar1.Value = (int)audioFileReader.CurrentTime.TotalMilliseconds;
                labelCurrent.Text = audioFileReader.CurrentTime.ToString(@"mm\:ss");
            }
        }               

        private void pictureBox1_Click(object sender, EventArgs e) //PLAY
        {
            if (songindex == -1)
                songindex = 0; listBox1.SelectedIndex = 0;
            string path = GetPathSong(listBox1.Items[0].ToString());
            Play(path);
        }
        private void pictureBox4_Click(object sender, EventArgs e) //PAUSE
        {
            Stop();
        }

        private void pictureBox3_Click(object sender, EventArgs e) // SIGUIENTE
        {
            int total = listBox1.Items.Count - 1;

            if (songindex == total)
            {
                songindex = 0;

                Stop();
                waveOut = null;
                listBox1.SelectedIndex = 0;
                Play(GetPathSong(listBox1.Items[0].ToString()));
            }
            else if (songindex < total)
            {
                int now = songindex + 1;
                songindex = now;

                Stop();
                waveOut = null;
                listBox1.SelectedIndex = now;
                Play(GetPathSong(listBox1.Items[now].ToString()));
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e) // ANTERIOR
        {
            int total = listBox1.Items.Count - 1;

            if (songindex < 0)
                return;
            if (songindex == 0)
            {
                songindex = total;

                Stop();
                waveOut = null;
                listBox1.SelectedIndex = total;
                Play(GetPathSong(listBox1.Items[total].ToString()));
            }
            else
            {
                int now = songindex - 1;
                songindex = now;

                Stop();
                waveOut = null;
                listBox1.SelectedIndex = now;
                Play(GetPathSong(listBox1.Items[now].ToString()));
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) //CAMBIO SONG
        {
            string path = GetPathSong(listBox1.SelectedItem.ToString());            
            
            songindex = listBox1.SelectedIndex;

            Stop();
            waveOut = null;
            Play(path);
        }
        private void listBoxAllPlaylist_SelectedIndexChanged(object sender, EventArgs e) //CAMBIO PLAYLIST
        {
            var selected = listBoxAllPlaylist.SelectedItem.ToString().Split('-');
            var weather = selected[0].Trim();
            var namepl = selected[1].Trim();

            var listplaylist = appPlaylist.ReadPlaylist();
            var playlist = listplaylist.Where(x => x.Name == namepl).First();
            
            int id = playlist.Id;

            ShowPlaylist(id);

            listBox1.SelectedIndex = 0;

            string path = GetPathSong(listBox1.SelectedItem.ToString());

            songindex = listBox1.SelectedIndex;

            Stop();
            waveOut = null;
            Play(path);
        } 

        private string GetPathSong(string songSelected)
        {
            var all = songSelected.Split('-');
            var namesong = all[0].Trim();
            var nameauthor = all[1].Trim();

            var list = appSong.ReadSong();
            var songs = list.Where(x => x.Name == namesong);
            var song = songs.Where(x => x.Author.Name == nameauthor).First();

            string path = Directory.GetCurrentDirectory() + $@"\Music\{song.Id}.mp3";

            return path;
        }
        private string GetNameSong(string songFile)
        {
            var pre = Path.GetFileName(songFile).Split('.').First();
            int id = Convert.ToInt32(pre);
            var song = appSong.ReadSong().Where(x => x.Id == id).First();

            return song.Name;   
        }
        private string GetNameAuthor(string songFile)
        {
            var pre = Path.GetFileName(songFile).Split('.').First();
            int id = Convert.ToInt32(pre);
            var song = appSong.ReadSong().Where(x => x.Id == id).First();

            return song.Author.Name;
        }

        private void pictureBox7_Click(object sender, EventArgs e) // LOG OUT
        {
            Application.Restart();
        }
        private void pictureBoxAdmin_Click(object sender, EventArgs e) // UI ADMIN
        {
            adminForm.Show();
            adminForm.BringToFront();
        }        
    }
}
