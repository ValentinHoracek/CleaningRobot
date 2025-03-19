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

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        private Dictionary<string, int> Instructions = new()
        {
            ("TR", 1),
            ("TL", 1),
            ("A", 2),
            ("B", 3),
            ("C", 5),
        };

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        private Position? CurrentPosition { get; set; } = StartPosition;

        private string COLUMN = "C";

        public void Run()
        {
            // Visited start position
            Visited.Add(new Position(StartPosition.X, StartPosition.Y));

            foreach(string command in Commands)
            {
                if (Battery - Instructions[command] < 0)
                {
                    return;
                }

                
                switch(command)
                {
                    case "TR":
                        CurrentPossition.TurnRight();
                        break;
                    case "TL":
                        CurrentPosition.TurnLeftt();
                        break;
                    case "A":
                        Move(true);
                        Visited.Add(CurrentPosition.X, CurrentPosition.Y);
                        break;
                    case "B":
                        Move(false);
                        Visited.Add(CurrentPosition.X, CurrentPosition.Y);
                        break;
                    case "C":
                        Cleaned.Add(new Position(CurrentPosition.X, CurrentPosition.Y));
                        break;

                }

                
                Battery -= Instructions[command];
            }

        }

        private void Move(bool forward)
        {
            int xOffset = 0;
            int yOffset = 0;

            switch(CurrentPosition.Facing) 
            {
                case Orientation.North:
                    xOffset++;
                    break;
                case Orientation.South:
                    xOffset--;
                    break;
                case Orientation.East:
                    yOffset++;
                    break;
                case Orientation.West:
                    yOffset--;
                    break;
                default:
                    break;

            }

            Position newPosition;
            if (forward)
            {
                newPosition = new(CurrentPosition.X + xOffset, CurrentPosition.Y + yOffset, CurrentPosition.Facing);
            }
            else
            {
                newPosition = new(CurrentPosition.X - xOffset, CurrentPosition.Y - yOffset, CurrentPosition.Facing);
            }

            

            if (CheckValidPosition(newPosition))
            {
                CurrentPossition = newPosition;
            }
            else
            {
                // backup sequence
            }
        }

        private bool CheckValidPosition(Position position)
        {
            if(position.X >= Map.Length)
            {
                return false;
            }
            if(position.Y >= Map[0].Length)
            { 
                return false;
            }

            if (Map[position.X][position.Y] is null 
                || Map[position.X][position.Y].Equals(COLUMN))
            {
                return false;
            }

            return true;
        }
    }
}
