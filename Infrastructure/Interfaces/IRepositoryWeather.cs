﻿using CrossCutting;
using CrossCutting.Enums;
using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface IRepositoryWeather
    {
        List<Weather> ReadWeather();
        void CreateWeather(Code code, string description);
        void UpdateWeather(int id, Code code, string description);
        void DeleteWeather(int id);
        Weather GetWeather(int id);
        Weather GetWeather(string code);
    }
}
