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
                case nameof(Orientation.N):
                    Facing = nameof(Orientation.E);
                    break;
                case nameof(Orientation.S):
                    Facing = nameof(Orientation.W);
                    break;
                case nameof(Orientation.W):
                    Facing = nameof(Orientation.N);
                    break;
                case nameof(Orientation.E):
                    Facing = nameof(Orientation.S);
                    break;
                default:
                    break;
            }
        }

        public void TurnLeftt()
        {
            switch (Facing)
            {
                case nameof(Orientation.N):
                    Facing = nameof(Orientation.W);
                    break;
                case nameof(Orientation.S):
                    Facing = nameof(Orientation.E);
                    break;
                case nameof(Orientation.W):
                    Facing = nameof(Orientation.S);
                    break;
                case nameof(Orientation.E):
                    Facing = nameof(Orientation.N);
                    break;
                default:
                    break;
            }
        }
    }
}
