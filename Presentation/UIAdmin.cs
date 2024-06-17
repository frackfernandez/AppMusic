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
        ApplicationAuthor appAuthor = new ApplicationAuthor();
        ApplicationSong appSong = new ApplicationSong();
        ApplicationWeather appWeather = new ApplicationWeather();
        ApplicationPlaylist appPlaylist = new ApplicationPlaylist();
        ApplicationUser appUser = new ApplicationUser();

        public UIAdmin()
        {
            InitializeComponent();
            DesignForm();
            LoadComboBox();
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

        private void LoadComboBox()
        {
            cBoxCategorySong.Items.Clear();
            var categoryList = Enum.GetNames(typeof(Category));
            foreach ( var category in categoryList )
            {
                cBoxCategorySong.Items.Add(category);
            } 
            
            cBoxAuthorSong.Items.Clear();
            var authorList = appAuthor.ReadAuthor();
            foreach ( var author in authorList )
            {
                cBoxAuthorSong.Items.Add(author.Name);
            }

            cBoxWeatherPlaylist.Items.Clear();
            var weatherList = appWeather.ReadWeather();
            foreach ( var weather in weatherList)
            {
                cBoxWeatherPlaylist.Items.Add(weather.Code);
            }

            listBoxAllSongPlaylist.Items.Clear();
            var listSong = appSong.ReadSong();
            foreach (var song in listSong)
            {
                listBoxAllSongPlaylist.Items.Add($"{song.Name.ToString()} - {song.Author.Name.ToString()}");
            }

            cBoxTypeUser.Items.Clear();
            var typeUserList = Enum.GetNames(typeof(UserType));
            foreach ( var type in typeUserList)
            {
                cBoxTypeUser.Items.Add(type);
            }
        }        

        private void btnRefreshAuthor_Click(object sender, EventArgs e)
        {
            dataGridAuthor.Rows.Clear();

            var listAuthor = appAuthor.ReadAuthor();

            foreach(var row in listAuthor)
            {
                dataGridAuthor.Rows.Add(row.Id.ToString(), row.Name.ToString());
            }
        }
        private void dataGridAuthor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var indexRow = e.RowIndex;
            DataGridViewRow row = dataGridAuthor.Rows[indexRow];
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
            if (textIdAuthor.Text == "")
            {
                MessageBox.Show("Id empty!");
                return;
            }

            int id = Convert.ToInt32(textIdAuthor.Text);
            appAuthor.DeleteAuthor(id);
            textIdAuthor.Text = "";
            textNameAuthor.Text = "";
            MessageBox.Show("Successful deleted!");
        }

        private void btnRefreshSong_Click(object sender, EventArgs e)
        {
            dataGridSong.Rows.Clear();

            var listSong = appSong.ReadSong();

            foreach ( var song in listSong)
            {
                dataGridSong.Rows.Add(song.Id.ToString(), song.Name.ToString(), song.Category.ToString(), song.Author.Name.ToString(), song.TotalDuration.ToString());
            }
        }
        private void dataGridSong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var indexRow = e.RowIndex;
            DataGridViewRow row = dataGridSong.Rows[indexRow];
            textIdSong.Text = row.Cells[0].Value.ToString();
            textNameSong.Text = row.Cells[1].Value.ToString();
            cBoxCategorySong.Text = row.Cells[2].Value.ToString();
            cBoxAuthorSong.Text = row.Cells[3].Value.ToString();
            textTotalDurationSong.Text = row.Cells[4].Value.ToString();

            string nameAuthor = row.Cells[3].Value.ToString();
            var listAuthor = appAuthor.ReadAuthor();
            var author = listAuthor.Where(x => x.Name == nameAuthor).First();

            string nameSong = row.Cells[1].Value.ToString();
            var listSong = appSong.ReadSong();
            var songs = listSong.Where(x => x.Name == nameSong).ToList();
            var song = songs.Where(x => x.Author.Id == author.Id).FirstOrDefault();

            string file = $@"{Directory.GetCurrentDirectory()}\Music\{song.Id}-{author.Id}.mp3";
            
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

            var listAuthor = appAuthor.ReadAuthor();
            var author = listAuthor.Where(x => x.Name == cBoxAuthorSong.Text).First();

            appSong.CreateSong(textNameSong.Text,category,author,textTotalDurationSong.Text);

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

            int.TryParse(textIdSong.Text, out int id);

            Enum.TryParse(cBoxCategorySong.Text, out Category category);

            var listAuthor = appAuthor.ReadAuthor();
            var author = listAuthor.Where(x => x.Name == cBoxAuthorSong.Text).First();

            appSong.UpdateSong(id, textNameSong.Text, category, author, textTotalDurationSong.Text);

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
            appSong.DeleteSong(id);

            textIdSong.Text = "";
            textNameSong.Text = "";
            cBoxCategorySong.SelectedIndex = -1;
            cBoxAuthorSong.SelectedIndex = -1;
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
            catch (ArgumentException ae)
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
            DataGridViewRow row = dataGridWeather.Rows[indexRow];
            textIdWeather.Text = row.Cells[0].Value.ToString();
            textCodeWeather.Text = row.Cells[1].Value.ToString();
            textDescriptionWeather.Text = row.Cells[2].Value.ToString();
        }
        private void btnRegisterWeather_Click(object sender, EventArgs e)
        {
            if (textIdWeather.Text != "")
            {
                MessageBox.Show("Id registered!");
                return;
            }
            if (textCodeWeather.Text == "")
            {
                MessageBox.Show("Code empty!");
                return;
            }
            if (textDescriptionWeather.Text == "")
            {
                MessageBox.Show("Description empty!");
                return;
            }

            appWeather.CreateWeather(textCodeWeather.Text, textDescriptionWeather.Text);

            textIdWeather.Text = "";
            textCodeWeather.Text = "";
            textDescriptionWeather.Text = "";
            MessageBox.Show("Successful registration!");
        }
        private void btnClearWeather_Click(object sender, EventArgs e)
        {
            textIdWeather.Text = "";
            textCodeWeather.Text = "";
            textDescriptionWeather.Text = "";
        }
        private void btnUpdateWeather_Click(object sender, EventArgs e)
        {
            if (textIdWeather.Text == "")
            {
                MessageBox.Show("Id empty!");
                return;
            }
            if (textCodeWeather.Text == "")
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
            appWeather.UpdateWeather(Id, textCodeWeather.Text, textDescriptionWeather.Text);

            textIdWeather.Text = "";
            textCodeWeather.Text = "";
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

            appWeather.DeleteWeather(Id);

            textIdWeather.Text = "";
            textCodeWeather.Text = "";
            textDescriptionWeather.Text = "";
            MessageBox.Show("Successful deleted!");
        }

        private void btnRefreshPlaylist_Click(object sender, EventArgs e)
        {
            dataGridPlaylist.Rows.Clear();

            var listPlaylist = appPlaylist.ReadPlaylist();

            foreach ( var playlist in listPlaylist )
            {
                dataGridPlaylist.Rows.Add(playlist.Id.ToString(), playlist.Name.ToString(), playlist.Weather.Code.ToString());
            }                       
        }
        private void dataGridPlaylist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var indexRow = e.RowIndex;
            DataGridViewRow row = dataGridPlaylist.Rows[indexRow];
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

            for (int i = 0; i < listBoxSongPlaylist.Items.Count; i++)
            {
                var all = itemsArray[i].Split('-'); 
                string songName = all[0].Trim();
                string songAuthor = all[1].Trim();

                var listAllSong = appSong.ReadSong();

                var songsname = listAllSong.Where(x => x.Name == songName).ToList();
                var song = songsname.Where(x => x.Author.Name == songAuthor).FirstOrDefault();

                listSong.Add(song);                
            }

            var listWeather = appWeather.ReadWeather();
            var weather = listWeather.Where(x => x.Code == cBoxWeatherPlaylist.Text).First();

            appPlaylist.CreatePlaylist(textNamePlaylist.Text, weather, listSong);

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

            appPlaylist.DeletePlaylist(id);

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

            int id = Convert.ToInt32(textIdPlaylist.Text);

            var listWeather = appWeather.ReadWeather();
            var weather = listWeather.Where(x => x.Code == cBoxWeatherPlaylist.Text).First();

            string[] itemsArray = new string[listBoxSongPlaylist.Items.Count];
            listBoxSongPlaylist.Items.CopyTo(itemsArray, 0);

            var listSong = new List<Song>();

            for (int i = 0; i < listBoxSongPlaylist.Items.Count; i++)
            {
                var all = itemsArray[i].Split('-');
                string songName = all[0].Trim();
                string songAuthor = all[1].Trim();

                var listAllSong = appSong.ReadSong();

                var songsname = listAllSong.Where(x => x.Name == songName).ToList();
                var song = songsname.Where(x => x.Author.Name == songAuthor).FirstOrDefault();

                listSong.Add(song);
            }

            appPlaylist.UpdatePlaylist(id, textNamePlaylist.Text, weather, listSong);

            textIdPlaylist.Text = "";
            textNamePlaylist.Text = "";
            cBoxWeatherPlaylist.SelectedIndex = -1;
            listBoxSongPlaylist.Items.Clear();
            MessageBox.Show("Successful update!");
        }

        private void btnRefreshUser_Click(object sender, EventArgs e)
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
            DataGridViewRow row = dataGridUser.Rows[indexRow];
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

            int id = Convert.ToInt32(textIdUser.Text);
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
            if (textIdUser.Text == "")
            {
                MessageBox.Show("Id empty!");
                return;
            }

            int id = Convert.ToInt32(textIdUser.Text);

            appUser.DeleteUser(id);

            textIdUser.Text = "";
            textNameUser.Text = "";
            cBoxTypeUser.SelectedIndex = -1;
            textPasswordUser.Text = "";
            MessageBox.Show("Successful deleted!");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadComboBox();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        
    }
}
