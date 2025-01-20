using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Business.Implementations;
using CrossCutting.DTO;
using CrossCutting.Enums;
using NAudio.Wave;

namespace Presentation
{
    public partial class UIAlbum : Form
    {
        private readonly ApplicationPlaylist appPlaylist;
        private readonly ApplicationSong appSong;
        private readonly ApplicationServiceWeather ASWeather;
        private readonly Random random;
        private readonly UIAdmin adminForm;

        private IWavePlayer waveOut;
        private AudioFileReader audioFileReader;
        private Timer progressTimer;

        private bool isPaused = false;

        private int songindex = -1;
        //private int playlistindex = -1;
        public string songPlaying = "";
        public string playlistPlaying = "";

        public int userUsing = -1;

        public UIAlbum(User user)
        {
            appPlaylist = new ApplicationPlaylist();
            appSong = new ApplicationSong();
            ASWeather = new ApplicationServiceWeather();
            random = new Random();
            adminForm = new UIAdmin(this);

            InitializeComponent();
            DesignForm();
            SetUser(user);
            userUsing = user.Id;

            SetWeather();
            SetAllPlaylist();

            IniProgressTimer();

            pictureBox4.Visible = false; // boton pausa

            SelectPlaylistWeather();
        }        

        private void IniProgressTimer()
        {
            progressTimer = new Timer();
            progressTimer.Interval = 100; // 100 milisegundos
            progressTimer.Tick += new EventHandler(UpdateProgressBar); // suscribirse a eventos
        }

        // DISEÑO FORM                
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

        // INICIO
        private void SetUser(User user)
        {
            labelUsername.Text = user.Name;
            if (user.UserType == CrossCutting.Enums.UserType.User) 
            {
                pictureBoxAdmin.Visible = false;
                labelLibrary.Visible = false;
            }
        }
        private void SetWeather()
        {
            string tem = ASWeather.GetTemp();
            string wea = ASWeather.GetWeather();
            label1.Text = wea;
            label2.Text = $"{tem}°C";
            
            var hora = DateTime.Now.Hour - 1; // 0 a 23
            if (hora > 18 || hora < 6) // noche o madrugada
            {
                switch (wea.ToLower())
                {
                    case "clouds":
                        pictureBoxWeather.Image = Presentation.Properties.Resources.nubes;
                        break;
                    case "rain":
                        pictureBoxWeather.Image = Presentation.Properties.Resources.gotas_de_lluvia;
                        break;
                    case "wind":
                        pictureBoxWeather.Image = Presentation.Properties.Resources.viento;
                        break;
                    case "snow":
                        pictureBoxWeather.Image = Presentation.Properties.Resources.nevando;
                        break;
                    case "clear":
                        pictureBoxWeather.Image = Presentation.Properties.Resources.sol;
                        break;
                    case "haze":
                    case "mist":
                        pictureBoxWeather.Image = Presentation.Properties.Resources.nubes;
                        break;
                    default:
                        pictureBoxWeather.Image = Presentation.Properties.Resources.nubes;
                        break;
                }
            }
            else // dia
            {
                switch (wea.ToLower())
                {
                    case "clouds":
                        pictureBoxWeather.Image = Presentation.Properties.Resources.nubes;
                        break;
                    case "rain":
                        pictureBoxWeather.Image = Presentation.Properties.Resources.gotas_de_lluvia;
                        break;
                    case "wind":
                        pictureBoxWeather.Image = Presentation.Properties.Resources.viento;
                        break;
                    case "snow":
                        pictureBoxWeather.Image = Presentation.Properties.Resources.nevando;
                        break;
                    case "clear":
                        pictureBoxWeather.Image = Presentation.Properties.Resources.sol;
                        break;
                    case "haze":
                    case "mist":
                        pictureBoxWeather.Image = Presentation.Properties.Resources.nubes;
                        break;
                    default:
                        pictureBoxWeather.Image = Presentation.Properties.Resources.nubes;
                        break;
                }
            }            
        }
        public void SetAllPlaylist()
        {
            listBoxAllPlaylist.Items.Clear();

            var list = appPlaylist.ReadPlaylist();

            foreach (var item in list)
            {
                listBoxAllPlaylist.Items.Add(item.Weather.Code + "  -  " +item.Name);
            }
        }
        private void SelectPlaylistWeather()
        {
            var weather = ASWeather.GetWeather();
            var allPlaylist = appPlaylist.ReadPlaylist();

            if (allPlaylist.Count == 0)
            {
                listBox1.Items.Clear();
                labelPlaylist.Text = "NO EXISTEN PLAYLIST!";
                labelSongNow.Text = "...";
                labelAuthorNow.Text = "...";
                return;
            }

            Enum.TryParse(weather, out Code code);

            var list = allPlaylist.Where(x => x.Weather.Code == code).ToList();

            if (list.Count() == 0)
            {
                var playlistSelect = allPlaylist.First();
                ShowPlaylist(playlistSelect.Id);
            }
            else
            {
                int select = random.Next(0, list.Count());
                var playlistSelect = list[select];
                ShowPlaylist(playlistSelect.Id);
            }            
        }
        private void ShowPlaylist(int id)
        {
            listBox1.Items.Clear();          

            var playlist = appPlaylist.GetPlaylist(id);

            labelPlaylist.Text = playlist.Name;
            playlistPlaying = playlist.Name;

            var songs = playlist.Songs;

            songPlaying = songs[0].Name;

            foreach (var song in songs)
            {
                listBox1.Items.Add(song.Name + " - " + song.Author.Name);
            }

            labelSongNow.Text = songs[0].Name;
            labelAuthorNow.Text = songs[0].Author.Name;
        }

        // REPRODUCCION
        private void Play(string path)
        {
            if (waveOut == null)
            {
                if (!File.Exists(path))
                {
                    MessageBox.Show("The song was registered in other Pc.");
                    return;
                }

                waveOut = new WaveOutEvent();                

                audioFileReader = new AudioFileReader(path);
                waveOut.Init(audioFileReader);

                waveOut.PlaybackStopped += OnPlaybackStopped;

                waveOut.Play();
                isPaused = false;
                progressBar1.Maximum = (int)audioFileReader.TotalTime.TotalMilliseconds;
                labelTotal.Text = audioFileReader.TotalTime.ToString(@"mm\:ss");
                progressTimer.Start();

                var aux = Path.GetFileName(path);
                var aux2 = aux.Split('.');
                var id = Int32.Parse(aux2[0]);
                var song = appSong.GetSong(id);

                songPlaying = song.Name;

                labelSongNow.Text = song.Name;
                labelAuthorNow.Text = song.Author.Name;

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

            if (songindex == total) // LLEGA SOLA A LA SIGUIENTE PLAYLIST
            {
                SetWeather();
                SelectPlaylistWeather();

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
            if (listBox1.Items.Count <=0 )
            {
                return;
            }          
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

            if (songindex == -1)
                return;

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
            playlistPlaying = playlist.Name;

            listBox1.SelectedIndex = 0;

            string path = GetPathSong(listBox1.SelectedItem.ToString());

            songindex = listBox1.SelectedIndex;
            songPlaying = playlist.Songs[0].Name;

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

        private void pictureBox7_Click(object sender, EventArgs e) // LOG OUT
        {
            this.Close();
            Application.Restart();
        }
        private void pictureBoxAdmin_Click(object sender, EventArgs e) // UI ADMIN
        {
            adminForm.Show();
            adminForm.BringToFront();
        }

        private void pictureBox8_Click(object sender, EventArgs e) // REFRESHPLAYLISTSSS
        {
            SetAllPlaylist();
        }
    }
}
