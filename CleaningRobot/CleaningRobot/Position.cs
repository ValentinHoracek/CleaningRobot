using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleaningRobot
{
    class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        [JsonPropertyName("facing")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Facing { get; set; }


        public Position()
        {
        }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position(int x, int y, string facing)
        {
            X = x;
            Y = y;
            Facing = facing;
        }
    }
}
