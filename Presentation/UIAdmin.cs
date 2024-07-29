using Business.Implementations;
using CrossCutting;
using CrossCutting.Enums;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Presentation
{
    public partial class UIAdmin : Form
    {
        private readonly ApplicationAuthor appAuthor;
        private readonly ApplicationSong appSong;
        private readonly ApplicationWeather appWeather;
        private readonly ApplicationPlaylist appPlaylist;
        private readonly ApplicationUser appUser;

        private UIAlbum uiAlbum;

        public UIAdmin(UIAlbum uiAlbum)
        {
            InitializeComponent();
            DesignForm();

            this.uiAlbum = uiAlbum;

            appAuthor = new ApplicationAuthor();
            appSong = new ApplicationSong();
            appWeather = new ApplicationWeather();
            appPlaylist = new ApplicationPlaylist();
            appUser = new ApplicationUser();

            LoadAuthors();
        }

        private void UIAdmin_Load(object sender, EventArgs e)
        {
        }

        private void DesignForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormClosing += MainForm_FormClosing;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.White;
            this.ShowIcon = false;
            this.Text = string.Empty;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // evita que el formulario se cierre
            e.Cancel = true;
            // ocultar el formulario en lugar de cerrarlo
            this.Hide();
        }
                      
        private void LoadAuthors()
        {
            dataGridAuthor.Rows.Clear();

            var listAuthor = appAuthor.ReadAuthor();

            foreach (var row in listAuthor)
            {
                dataGridAuthor.Rows.Add(row.Id.ToString(), row.Name.ToString());
            }
        }
        private void btnRefreshAuthor_Click(object sender, EventArgs e)
        {
            LoadAuthors();
        }
        private void dataGridAuthor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var indexRow = e.RowIndex;
            if (indexRow == -1)
                return;
            DataGridViewRow row = dataGridAuthor.Rows[indexRow];
            if (row.Cells[0].Value is null)
                return;
            textIdAuthor.Text = row.Cells[0].Value.ToString();
            textNameAuthor.Text = row.Cells[1].Value.ToString();
        }
        private void btnRegisterAuthor_Click(object sender, EventArgs e)
        {
            if (textIdAuthor.Text != "")
            {
                MessageBox.Show("Id registered!");
                return;
            }
            if (textNameAuthor.Text == "")
            {
                MessageBox.Show("Name empty!");
                return;
            }

            appAuthor.CreateAuthor(textNameAuthor.Text);
            textNameAuthor.Text = "";
            MessageBox.Show("Successful registration!");
        }
        private void btnClearAuthor_Click(object sender, EventArgs e)
        {
            textIdAuthor.Text = "";
            textNameAuthor.Text = "";
        }
        private void btnUpdateAuthor_Click(object sender, EventArgs e)
        {
            if (textIdAuthor.Text == "")
            {
                MessageBox.Show("Id empty!");
                return;
            }
            if (textNameAuthor.Text == "")
            {
                MessageBox.Show("Name empty!");
                return;
            }

            int id = Convert.ToInt32(textIdAuthor.Text);
            appAuthor.UpdateAuthor(id,textNameAuthor.Text);
            textIdAuthor.Text = "";
            textNameAuthor.Text = ""; 
            MessageBox.Show("Successful update!");
        }
        private void btnDeleteAuthor_Click(object sender, EventArgs e)
        {    
            int id = Convert.ToInt32(textIdAuthor.Text);

            // Validación autor tiene canciones
            var songs = appSong.ReadSong();
            bool tienecanciones = songs.Any(x => x.Author.Id == id);

            if (tienecanciones)
            {
                MessageBox.Show("This author has songs.");
                return;
            }

            appAuthor.DeleteAuthor(id);
            textIdAuthor.Text = "";
            textNameAuthor.Text = "";
            MessageBox.Show("Successful deleted!");
        }

        private void btnRefreshSong_Click(object sender, EventArgs e)
        {
            LoadSongs();
        }
        private void LoadSongs()
        {
            dataGridSong.Rows.Clear();

            var listSong = appSong.ReadSong();

            foreach (var song in listSong)
            {
                dataGridSong.Rows.Add(song.Id.ToString(), song.Name.ToString(), song.Category.ToString(), song.Author.Name.ToString(), song.Album.ToString(),song.TotalDuration.ToString());
            }
        }
        private void dataGridSong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var indexRow = e.RowIndex;
            if (indexRow == -1)
                return;
            DataGridViewRow row = dataGridSong.Rows[indexRow];
            if (row.Cells[0].Value is null)
                return;
            textIdSong.Text = row.Cells[0].Value.ToString();
            textNameSong.Text = row.Cells[1].Value.ToString();
            cBoxCategorySong.Text = row.Cells[2].Value.ToString();
            cBoxAuthorSong.Text = row.Cells[3].Value.ToString();
            textAlbumSong.Text = row.Cells[4].Value.ToString();
            textTotalDurationSong.Text = row.Cells[5].Value.ToString();

            var id = row.Cells[0].Value.ToString();

            string file = $@"{Directory.GetCurrentDirectory()}\Music\{id}.mp3";
            
            if (File.Exists(file))
                textFileSong.Text = file;
            else
                textFileSong.Text = "File not found.";
        }
        private void btnRegisterSong_Click(object sender, EventArgs e)
        {
            if (textIdSong.Text != "")
            {
                MessageBox.Show("Id registered!");
                return;
            }
            if (textNameSong.Text == "")
            {
                MessageBox.Show("Name empty!");
                return;
            }
            if (cBoxCategorySong.Text == "")
            {
                MessageBox.Show("Category empty!");
                return;
            }
            if (cBoxAuthorSong.Text == "")
            {
                MessageBox.Show("Author empty!");
                return;
            }
            if (textAlbumSong.Text == "")
            {
                MessageBox.Show("Album empty!");
                return;
            }
            if (textTotalDurationSong.Text == "")
            {
                MessageBox.Show("Total Duration empty!");
                return;
            }
            if (textFileSong.Text == "")
            {
                MessageBox.Show("File empty!");
                return;
            }

            Enum.TryParse(cBoxCategorySong.Text, out Category category);
            
            var author = appAuthor.GetAuthor(cBoxAuthorSong.Text);

            appSong.CreateSong(textNameSong.Text,category,author,textAlbumSong.Text,textTotalDurationSong.Text);

            //here optimizar - Obtener el ultimo Id
            string nameSong = textNameSong.Text;
            var listSong = appSong.ReadSong();
            var songs = listSong.Where(x => x.Name == nameSong).ToList();
            var song = songs.Where(x => x.Author.Id == author.Id).FirstOrDefault();

            string newFile = $@"{Directory.GetCurrentDirectory()}\Music\{song.Id}.mp3";
            File.Copy(textFileSong.Text, newFile, true);

            textIdSong.Text = "";
            textNameSong.Text = "";
            cBoxCategorySong.SelectedIndex = -1;
            cBoxAuthorSong.SelectedIndex = -1;
            textAlbumSong.Text = "";
            textTotalDurationSong.Text = "";
            textFileSong.Text = "";
            MessageBox.Show("Successful registration!");
        }
        private void btnClearSong_Click(object sender, EventArgs e)
        {
            textIdSong.Text = "";
            textNameSong.Text = "";
            cBoxCategorySong.SelectedIndex = -1;
            cBoxAuthorSong.SelectedIndex = -1;
            textAlbumSong.Text = "";
            textTotalDurationSong.Text = "";
            textFileSong.Text = "";
        }
        private void btnUpdateSong_Click(object sender, EventArgs e)
        {
            if (textIdSong.Text == "")
            {
                MessageBox.Show("Id empty!");
                return;
            }

            int id = Convert.ToInt32(textIdSong.Text);

            var song = appSong.GetSong(id);
            if (song.Name == uiAlbum.songPlaying)
            {
                MessageBox.Show("This song is playing.");
                return;
            }

            if (textNameSong.Text == "")
            {
                MessageBox.Show("Name empty!");
                return;
            }
            if (cBoxCategorySong.Text == "")
            {
                MessageBox.Show("Category empty!");
                return;
            }
            if (cBoxAuthorSong.Text == "")
            {
                MessageBox.Show("Author empty!");
                return;
            }
            if (textAlbumSong.Text == "")
            {
                MessageBox.Show("Album empty!");
                return;
            }
            if (textTotalDurationSong.Text == "")
            {
                MessageBox.Show("Total Duration empty!");
                return;
            }
            if (textFileSong.Text == "")
            {
                MessageBox.Show("File empty!");
                return;
            }
            if (textFileSong.Text == "")
            {
                MessageBox.Show("File empty!");
                return;
            }
            if (textFileSong.Text == "File not found.")
            {
                MessageBox.Show("File empty!");
                return;
            }            

            Enum.TryParse(cBoxCategorySong.Text, out Category category);
                        
            var author = appAuthor.GetAuthor(cBoxAuthorSong.Text);                 

            string newFile = $@"{Directory.GetCurrentDirectory()}\Music\{id}.mp3";

            if(textFileSong.Text != newFile)
            {
                try
                {
                    File.Copy(textFileSong.Text, newFile, true);
                }
                catch (IOException)
                {
                    MessageBox.Show("The song is playing!");
                    return;
                }
            }            

            appSong.UpdateSong(id, textNameSong.Text, category, author, textAlbumSong.Text, textTotalDurationSong.Text);

            textIdSong.Text = "";
            textNameSong.Text = "";
            cBoxCategorySong.SelectedIndex = -1;
            cBoxAuthorSong.SelectedIndex = -1;
            textAlbumSong.Text = "";
            textTotalDurationSong.Text = "";
            textFileSong.Text = "";
            MessageBox.Show("Successful update!");
        }
        private void btnDeleteSong_Click(object sender, EventArgs e)
        {
            if (textIdSong.Text == "")
            {
                MessageBox.Show("Id empty!");
                return;
            }

            int id = Convert.ToInt32(textIdSong.Text);

            var songP = appSong.GetSong(id);
            if (songP.Name == uiAlbum.songPlaying)
            {
                MessageBox.Show("This song is playing.");
                return;
            }

            var playlists = appPlaylist.ReadPlaylist();
            foreach (var playlist in playlists)
            {
                foreach (var song in playlist.Songs)
                {
                    if (song.Id == id)
                    {
                        MessageBox.Show("This song is registered in a playlist.");
                        return;
                    }
                }
            }
            
            appSong.DeleteSong(id);

            textIdSong.Text = "";
            textNameSong.Text = "";
            cBoxCategorySong.SelectedIndex = -1;
            cBoxAuthorSong.SelectedIndex = -1;
            textAlbumSong.Text = "";
            textTotalDurationSong.Text = "";
            textFileSong.Text = "";
            MessageBox.Show("Successful deleted!");
        }
        private void btnUploadSong_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "file mp3 (*.mp3)| *.mp3";

                if (file.ShowDialog() == DialogResult.OK)
                {
                    textFileSong.Text = file.FileName;
                }

                using (var reader = new Mp3FileReader(file.FileName))
                {
                    TimeSpan duration = reader.TotalTime;
                    textTotalDurationSong.Text = duration.ToString(@"mm\:ss");
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Song no select");
            }
            catch (Exception ex)
            {
                MessageBox.Show("otro error: " + ex);
            }            
        }

        private void btnRefreshWeather_Click(object sender, EventArgs e)
        {
            LoadWeathers();
        }
        private void LoadWeathers()
        {
            dataGridWeather.Rows.Clear();

            var listWeather = appWeather.ReadWeather();

            foreach (var weather in listWeather)
            {
                dataGridWeather.Rows.Add(weather.Id.ToString(), weather.Code.ToString(), weather.Description.ToString());
            }
        }
        private void dataGridWeather_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var indexRow = e.RowIndex;
            if (indexRow == -1)
                return;
            DataGridViewRow row = dataGridWeather.Rows[indexRow];
            if (row.Cells[0].Value is null)
                return;
            textIdWeather.Text = row.Cells[0].Value.ToString();
            cBoxCodeWeather.Text = row.Cells[1].Value.ToString();
            textDescriptionWeather.Text = row.Cells[2].Value.ToString();
        }
        private void btnRegisterWeather_Click(object sender, EventArgs e)
        {
            if (textIdWeather.Text != "")
            {
                MessageBox.Show("Id registered!");
                return;
            }
            if (cBoxCodeWeather.Text == "")
            {
                MessageBox.Show("Code empty!");
                return;
            }
            if (textDescriptionWeather.Text == "")
            {
                MessageBox.Show("Description empty!");
                return;
            }

            Enum.TryParse(cBoxCodeWeather.Text, out Code code);

            // Validación Code unico
            var codeBd = appWeather.GetWeather(cBoxCodeWeather.Text);
            if (codeBd != null)
            {
                MessageBox.Show("This code already exists.");
                return;
            }

            appWeather.CreateWeather(code, textDescriptionWeather.Text);

            textIdWeather.Text = "";
            cBoxCodeWeather.SelectedIndex = -1;
            textDescriptionWeather.Text = "";
            MessageBox.Show("Successful registration!");
        }
        private void btnClearWeather_Click(object sender, EventArgs e)
        {
            textIdWeather.Text = "";
            cBoxCodeWeather.SelectedIndex = -1;
            textDescriptionWeather.Text = "";
        }
        private void btnUpdateWeather_Click(object sender, EventArgs e)
        {
            if (textIdWeather.Text == "")
            {
                MessageBox.Show("Id empty!");
                return;
            }
            if (cBoxCodeWeather.Text == "")
            {
                MessageBox.Show("Code empty!");
                return;
            }
            if (textDescriptionWeather.Text == "")
            {
                MessageBox.Show("Description empty!");
                return;
            }
            int Id = Convert.ToInt32(textIdWeather.Text);
            Enum.TryParse(cBoxCodeWeather.Text, out Code code);

            // Validacion mismo code
            var weather = appWeather.GetWeather(Id);
            if (weather.Code.ToString() != cBoxCodeWeather.Text)
            {
                // Validación Code unico
                var codeBd = appWeather.GetWeather(cBoxCodeWeather.Text);
                if (codeBd != null)
                {
                    MessageBox.Show("This code already exists.");
                    return;
                }
            }

            appWeather.UpdateWeather(Id, code, textDescriptionWeather.Text);

            textIdWeather.Text = "";
            cBoxCodeWeather.SelectedIndex = -1;
            textDescriptionWeather.Text = "";
            MessageBox.Show("Successful update!");
        }
        private void btnDeleteWeather_Click(object sender, EventArgs e)
        {
            if (textIdWeather.Text == "")
            {
                MessageBox.Show("Id empty!");
                return;
            }

            int Id = Convert.ToInt32(textIdWeather.Text);

            // Validacion Playlists con Clima registrado
            var allPlaylists = appPlaylist.ReadPlaylist();
            bool tiene = allPlaylists.Any(x => x.Weather.Id == Id);

            if (tiene)
            {
                MessageBox.Show("This weather has registered playlist.");
                return;
            }

            appWeather.DeleteWeather(Id);

            textIdWeather.Text = "";
            cBoxCodeWeather.SelectedIndex = -1;
            textDescriptionWeather.Text = "";
            MessageBox.Show("Successful deleted!");
        }

        private void btnRefreshPlaylist_Click(object sender, EventArgs e)
        {
            LoadPlaylists();                     
        }
        private void LoadPlaylists()
        {
            dataGridPlaylist.Rows.Clear();

            var listPlaylist = appPlaylist.ReadPlaylist();

            foreach (var playlist in listPlaylist)
            {
                dataGridPlaylist.Rows.Add(playlist.Id.ToString(), playlist.Name.ToString(), playlist.Weather.Code.ToString(), playlist.TotalDuration.ToString());
            }
        }
        private void dataGridPlaylist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var indexRow = e.RowIndex;     
            if (indexRow == -1)
                return;
            DataGridViewRow row = dataGridPlaylist.Rows[indexRow];
            if (row.Cells[0].Value is null)
                return;
            textIdPlaylist.Text = row.Cells[0].Value.ToString();
            textNamePlaylist.Text = row.Cells[1].Value.ToString();
            cBoxWeatherPlaylist.Text = row.Cells[2].Value.ToString();
            listBoxSongPlaylist.Items.Clear();
            var listSongs = appSong.GetListSongs(Convert.ToInt32(row.Cells[0].Value.ToString()));
            foreach ( var song in listSongs )
            {
                listBoxSongPlaylist.Items.Add($"{song.Name} - {song.Author.Name}");
            }
        }
        private void btnClearPlaylist_Click(object sender, EventArgs e)
        {
            textIdPlaylist.Text = "";
            textNamePlaylist.Text = "";
            cBoxWeatherPlaylist.SelectedIndex = -1;
            listBoxSongPlaylist.Items.Clear();
        }
        private void btnAddSongPlaylist_Click(object sender, EventArgs e)
        {
            if (listBoxAllSongPlaylist.SelectedItem != null)
            {
                string[] itemsArray = new string[listBoxSongPlaylist.Items.Count];
                listBoxSongPlaylist.Items.CopyTo(itemsArray, 0);

                foreach (var song in itemsArray)
                {
                    if (song.ToString().Equals(listBoxAllSongPlaylist.SelectedItem.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Song is already!");
                        return;
                    }
                }

                var songSelected = listBoxAllSongPlaylist.SelectedItem.ToString();

                listBoxSongPlaylist.Items.Add(songSelected);
            }
            else
            {
                MessageBox.Show("Song not selected!");
            }
        }
        private void btnRemoveSongPlaylist_Click(object sender, EventArgs e)
        {
            if (listBoxSongPlaylist.SelectedItem != null)
            {
                listBoxSongPlaylist.Items.Remove(listBoxSongPlaylist.SelectedItem);
            }
            else
            {
                MessageBox.Show("Song not selected!");
            }
        }
        private void btnRegisterPlaylist_Click(object sender, EventArgs e)
        {
            if (textIdPlaylist.Text != "")
            {
                MessageBox.Show("Id registered!");
                return;
            }
            if (textNamePlaylist.Text == "")
            {
                MessageBox.Show("Name empty!");
                return;
            }
            if (cBoxWeatherPlaylist.Text == "")
            {
                MessageBox.Show("Weather empty!");
                return;
            }
            if (listBoxSongPlaylist.Items.Count < 1)
            {
                MessageBox.Show("Chose one song pls!");
                return;
            }

            string[] itemsArray = new string[listBoxSongPlaylist.Items.Count];
            listBoxSongPlaylist.Items.CopyTo(itemsArray, 0);

            var listSong = new List<Song>();
            var durationPlaylist = "00:00";

            for (int i = 0; i < listBoxSongPlaylist.Items.Count; i++)
            {
                var all = itemsArray[i].Split('-'); 
                string songName = all[0].Trim();
                string songAuthor = all[1].Trim();

                var listAllSong = appSong.ReadSong();

                var songsname = listAllSong.Where(x => x.Name == songName).ToList();
                var song = songsname.Where(x => x.Author.Name == songAuthor).FirstOrDefault();

                durationPlaylist = DurationSuma(durationPlaylist, song.TotalDuration);

                listSong.Add(song);                
            }

            Enum.TryParse(cBoxWeatherPlaylist.Text, out Code code);

            var listWeather = appWeather.ReadWeather();
            var weather = listWeather.Where(x => x.Code == code).First();

            appPlaylist.CreatePlaylist(textNamePlaylist.Text, weather, durationPlaylist, listSong);

            textIdPlaylist.Text = "";
            textNamePlaylist.Text = "";
            cBoxWeatherPlaylist.SelectedIndex = -1;
            listBoxSongPlaylist.Items.Clear();
            MessageBox.Show("Successful registration!");
        }
        private void btnDeletePlaylist_Click(object sender, EventArgs e)
        {
            if (textIdPlaylist.Text == "")
            {
                MessageBox.Show("Id empty!");
                return;
            }

            int id = Convert.ToInt32(textIdPlaylist.Text);

            var plSelect = appPlaylist.GetPlaylist(id); 

            if (plSelect.Name == uiAlbum.playlistPlaying)
            {
                MessageBox.Show("This playlist is playing.");
                return;
            }

            appPlaylist.DeletePlaylist(id);

            uiAlbum.SetAllPlaylist();

            textIdPlaylist.Text = "";
            textNamePlaylist.Text = "";
            cBoxWeatherPlaylist.SelectedIndex = -1;
            listBoxSongPlaylist.Items.Clear();
            MessageBox.Show("Successful deleted!");
        }
        private void btnUpdatePlaylist_Click(object sender, EventArgs e)
        {
            if (textIdPlaylist.Text == "")
            {
                MessageBox.Show("Id empty!");
                return;
            }

            int id = Convert.ToInt32(textIdPlaylist.Text);

            var plSelect = appPlaylist.GetPlaylist(id);

            if (plSelect.Name == uiAlbum.playlistPlaying)
            {
                MessageBox.Show("This playlist is playing.");
                return;
            }

            if (textNamePlaylist.Text == "")
            {
                MessageBox.Show("Name empty!");
                return;
            }
            if (cBoxWeatherPlaylist.Text == "")
            {
                MessageBox.Show("Weather empty!");
                return;
            }
            if (listBoxSongPlaylist.Items.Count < 1)
            {
                MessageBox.Show("Chose one song pls!");
                return;
            }

            Enum.TryParse(cBoxWeatherPlaylist.Text, out Code code);

            var listWeather = appWeather.ReadWeather();
            var weather = listWeather.Where(x => x.Code == code).First();

            string[] itemsArray = new string[listBoxSongPlaylist.Items.Count];
            listBoxSongPlaylist.Items.CopyTo(itemsArray, 0);

            var listSong = new List<Song>();
            var durationPlaylist = "00:00";

            for (int i = 0; i < listBoxSongPlaylist.Items.Count; i++)
            {
                var all = itemsArray[i].Split('-');
                string songName = all[0].Trim();
                string songAuthor = all[1].Trim();

                var listAllSong = appSong.ReadSong();

                var songsname = listAllSong.Where(x => x.Name == songName).ToList();
                var song = songsname.Where(x => x.Author.Name == songAuthor).FirstOrDefault();

                durationPlaylist = DurationSuma(durationPlaylist, song.TotalDuration);

                listSong.Add(song);
            }

            appPlaylist.UpdatePlaylist(id, textNamePlaylist.Text, weather, durationPlaylist, listSong);

            uiAlbum.SetAllPlaylist();

            textIdPlaylist.Text = "";
            textNamePlaylist.Text = "";
            cBoxWeatherPlaylist.SelectedIndex = -1;
            listBoxSongPlaylist.Items.Clear();
            MessageBox.Show("Successful update!");
        }

        private void btnRefreshUser_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }
        private void LoadUsers()
        {
            dataGridUser.Rows.Clear();

            var listUser = appUser.ReadUser();

            foreach (var user in listUser)
            {
                dataGridUser.Rows.Add(user.Id, user.Name, user.UserType.ToString(), user.Password);
            }
        }
        private void dataGridUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var indexRow = e.RowIndex;
            if (indexRow == -1)
                return;
            DataGridViewRow row = dataGridUser.Rows[indexRow];
            if (row.Cells[0].Value is null)
                return;
            textIdUser.Text = row.Cells[0].Value.ToString();
            textNameUser.Text = row.Cells[1].Value.ToString();
            cBoxTypeUser.Text = row.Cells[2].Value.ToString();
            textPasswordUser.Text = row.Cells[3].Value.ToString();
        }
        private void btnRegisterUser_Click(object sender, EventArgs e)
        {
            if (textIdUser.Text != "")
            {
                MessageBox.Show("Id registered!");
                return;
            }
            if (textNameUser.Text == "")
            {
                MessageBox.Show("Name empty!");
                return;
            }
            if (cBoxTypeUser.Text == "")
            {
                MessageBox.Show("Type empty!");
                return;
            }
            if (textPasswordUser.Text == "")
            {
                MessageBox.Show("Password empty!");
                return;
            }

            bool nameRegistered = appUser.ReadUser().Where(x => x.Name == textNameUser.Text).Any();
            if (nameRegistered)
            {
                MessageBox.Show("Name registered!");
                return;
            }

            Enum.TryParse(cBoxTypeUser.Text, out UserType userType);

            appUser.CreateUser(textNameUser.Text, userType, textPasswordUser.Text);

            textIdUser.Text = "";
            textNameUser.Text = "";
            cBoxTypeUser.SelectedIndex = -1;
            textPasswordUser.Text = "";
            MessageBox.Show("Successful registration!");
        }
        private void btnClearUser_Click(object sender, EventArgs e)
        {
            textIdUser.Text = "";
            textNameUser.Text = "";
            cBoxTypeUser.SelectedIndex = -1;
            textPasswordUser.Text = "";
        }
        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if (textIdUser.Text == "4" || textIdUser.Text == "6")
            {
                MessageBox.Show("Default user.\nCannot be edited.");
                return;
            }

            int id = Convert.ToInt32(textIdUser.Text);

            if (id == uiAlbum.userUsing)
            {
                MessageBox.Show("This user is being used.");
                return;
            }

            if (textIdUser.Text == "")
            {
                MessageBox.Show("Id empty!");
                return;
            }
            if (textNameUser.Text == "")
            {
                MessageBox.Show("Name empty!");
                return;
            }
            if (cBoxTypeUser.Text == "")
            {
                MessageBox.Show("Type empty!");
                return;
            }
            if (textPasswordUser.Text == "")
            {
                MessageBox.Show("Password empty!");
                return;
            }
            
            Enum.TryParse(cBoxTypeUser.Text, out UserType userType);

            appUser.UpdateUser(id, textNameUser.Text, userType, textPasswordUser.Text);

            textIdUser.Text = "";
            textNameUser.Text = "";
            cBoxTypeUser.SelectedIndex = -1;
            textPasswordUser.Text = "";
            MessageBox.Show("Successful update!");
        }
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (textIdUser.Text == "4" || textIdUser.Text == "6")
            {
                MessageBox.Show("Default user.\nCannot be deleted.");
                return;
            }

            int id = Convert.ToInt32(textIdUser.Text);

            if (id == uiAlbum.userUsing)
            {
                MessageBox.Show("This user is being used.");
                return;
            }

            if (textIdUser.Text == "")
            {
                MessageBox.Show("Id empty!");
                return;
            }

            if (textIdUser.Text == "")
            {
                MessageBox.Show("Id empty!");
                return;
            }

            appUser.DeleteUser(id);

            textIdUser.Text = "";
            textNameUser.Text = "";
            cBoxTypeUser.SelectedIndex = -1;
            textPasswordUser.Text = "";
            MessageBox.Show("Successful deleted!");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var control = sender as TabControl;

            switch (control.SelectedIndex)
            {
                case 0:
                    LoadAuthors();
                    break;
                case 1:
                    LoadSongs();
                    cBoxCategorySong.Items.Clear();
                    var categoryList = Enum.GetNames(typeof(Category));
                    foreach (var category in categoryList)
                    {
                        cBoxCategorySong.Items.Add(category);
                    }

                    cBoxAuthorSong.Items.Clear();
                    var authorList = appAuthor.ReadAuthor();
                    foreach (var author in authorList)
                    {
                        cBoxAuthorSong.Items.Add(author.Name);
                    }
                    break;
                case 2:
                    cBoxCodeWeather.Items.Clear();
                    var codeList = Enum.GetNames(typeof(Code));
                    foreach (var code in codeList)
                    {
                        cBoxCodeWeather.Items.Add(code);
                    }
                    LoadWeathers();
                    break;
                case 3: 
                    LoadPlaylists();
                    cBoxWeatherPlaylist.Items.Clear();
                    var weatherList = appWeather.ReadWeather();
                    foreach (var weather in weatherList)
                    {
                        cBoxWeatherPlaylist.Items.Add(weather.Code);
                    }

                    listBoxAllSongPlaylist.Items.Clear();
                    var listSong = appSong.ReadSong();
                    foreach (var song in listSong)
                    {
                        listBoxAllSongPlaylist.Items.Add($"{song.Name.ToString()} - {song.Author.Name.ToString()}");
                    }
                    break;
                case 4:
                    LoadUsers();
                    cBoxTypeUser.Items.Clear();
                    var typeUserList = Enum.GetNames(typeof(UserType));
                    foreach (var type in typeUserList)
                    {
                        cBoxTypeUser.Items.Add(type);
                    }
                    break;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private string DurationSuma(string one, string two)
        {
            // Convertir las cadenas a TimeSpan, considerando que el formato es m:ss o mm:ss
            TimeSpan ts1 = ParseTimeSpan(one);
            TimeSpan ts2 = ParseTimeSpan(two);

            // Sumar los TimeSpan
            TimeSpan result = ts1 + ts2;

            // Convertir el resultado a cadena en formato m:ss
            string resultString = $"{(int)result.TotalMinutes}:{result.Seconds:D2}";

            return resultString;
        }
        private TimeSpan ParseTimeSpan(string time)
        {
            string[] parts = time.Split(':');
            int minutes = int.Parse(parts[0]);
            int seconds = int.Parse(parts[1]);
            return new TimeSpan(0, minutes, seconds);
        }
    }
}
