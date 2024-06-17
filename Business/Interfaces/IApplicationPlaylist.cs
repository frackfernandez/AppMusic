﻿using CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    internal interface IApplicationPlaylist
    {
        List<Playlist> ReadPlaylist();
        void CreatePlaylist(string name, Weather weather, List<Song> songs);
        void UpdatePlaylist(int id, string name, Weather weather, List<Song> songs);
        void DeletePlaylist(int id);
        Playlist GetPlaylist(int id);
    }
}
