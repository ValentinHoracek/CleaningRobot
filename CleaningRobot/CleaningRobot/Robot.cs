using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleaningRobot
{
    class Robot
    {
        [JsonPropertyName("map")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string[][]? Map { get; set; }

        [JsonPropertyName("visited")]
        public List<Position> Visited { get; set; } = new();

        [JsonPropertyName("cleaned")]
        public List<Position> Cleaned { get; set; } = new();

        [JsonPropertyName("final")]
        public Position? FinalPosition { get; set; }

        [JsonPropertyName("start")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Position? StartPosition { get; set; }

        [JsonPropertyName("commands")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string[]? Commands { get; set; }

        [JsonPropertyName("battery")]
        public int Battery { get; set; }



    }
}
