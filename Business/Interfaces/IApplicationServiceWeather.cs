﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    internal interface IApplicationServiceWeather
    {
        string GetWeather();
        string GetTemp();
    }
}
