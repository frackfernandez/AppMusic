using CrossCutting.Enums;

namespace CrossCutting
{
    public class Weather
    {
        public Weather(int id, Code code, string description)
        {
            Id = id;
            Code = code;
            Description = description;
        }

        public int Id { get; set; }
        public Code Code { get; set; }
        public string Description { get; set; }
    }
}
