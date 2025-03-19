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

        public void TurnRight()
        {
            switch(Facing)
            {
                case Orientation.North:
                    Facing = Orientation.East;
                    break;
                case Orientation.South:
                    Facing = Orientation.West;
                    break;
                case Orientation.West:
                    Facing = Orientation.North;
                    break;
                case Orientation.East:
                    Facing = Orientation.South;
                    break;
                default:
                    break;
            }
        }

        public void TurnLeftt()
        {
            switch (Facing)
            {
                case Orientation.North:
                    Facing = Orientation.West;
                    break;
                case Orientation.South:
                    Facing = Orientation.East;
                    break;
                case Orientation.West:
                    Facing = Orientation.South;
                    break;
                case Orientation.East:
                    Facing = Orientation.North;
                    break;
                default:
                    break;
            }
        }
    }
}
