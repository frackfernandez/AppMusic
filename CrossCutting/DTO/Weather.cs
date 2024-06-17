using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting
{
    public class Weather
    {
        public Weather(int id, string code, string description)
        {
            Id = id;
            Code = code;
            Description = description;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
