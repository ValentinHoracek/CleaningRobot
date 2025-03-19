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
        public List<Position> Visited { get; set; } = [];

        [JsonPropertyName("cleaned")]
        public List<Position> Cleaned { get; set; } = [];

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
        private readonly Dictionary<string, int> Instructions = new()
        {
            { "TR", 1 },
            { "TL", 1 },
            { "A", 2 },
            { "B", 3 },
            { "C", 5 },
        };

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        private  Position CurrentPosition { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        private const string COLUMN = "C";

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        private string[][] BackupSequences { get; set; } = new string[5][];

        private int BackupLevel { get; set; } = 0;

        public Robot()
        {
            CurrentPosition = StartPosition ?? new Position();

            // Initialize backup sequences
            BackupSequences[0] = ["TR", "A", "TL"];
            BackupSequences[1] = ["TR", "A", "TR"];
            BackupSequences[2] = ["TR", "A", "TR"];
            BackupSequences[3] = ["TR", "B", "TR", "A"];
            BackupSequences[4] = ["TL", "TL", "TA"];
        }

        public void Run()
        {
            if (StartPosition is null || Commands is null || Map is null)
            {
                return;
            }

            // Visited start position
            Visited.Add(new Position(StartPosition.X, StartPosition.Y));


            foreach(string command in Commands)
            {
                ExecuteCommand(command);

                if (BackupLevel == BackupSequences.Length)
                {
                    break;
                }
            }
        }

        private void ExecuteCommand(string command)
        {
            if (Battery - Instructions[command] < 0)
            {
                return;
            }

            switch (command)
            {
                case "TR":
                    CurrentPosition?.TurnRight();
                    break;
                case "TL":
                    CurrentPosition?.TurnLeftt();
                    break;
                case "A":
                    Move(true);
                    Visited.Add(new Position(CurrentPosition.X, CurrentPosition.Y));
                    break;
                case "B":
                    Move(false);
                    Visited.Add(new Position(CurrentPosition.X, CurrentPosition.Y));
                    break;
                case "C":
                    Cleaned.Add(new Position(CurrentPosition.X, CurrentPosition.Y));
                    break;

            }


            Battery -= Instructions[command];
        }

        private void Move(bool forward)
        {
            int xOffset = 0;
            int yOffset = 0;

            switch(CurrentPosition.Facing) 
            {
                case nameof(Orientation.N):
                    xOffset++;
                    break;
                case nameof(Orientation.S):
                    xOffset--;
                    break;
                case nameof(Orientation.E):
                    yOffset++;
                    break;
                case nameof(Orientation.W):
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
                CurrentPosition = newPosition;
            }
            else
            {
                // backup sequence
                for (int i = BackupLevel; i < BackupSequences.Length; i++)
                {
                    foreach (string command in BackupSequences[i])
                    {
                        ExecuteCommand(command);
                    }
                }

                BackupLevel++;
            }
        }

        private bool CheckValidPosition(Position position)
        {
            if(position.X >= Map?.Length)
            {
                return false;
            }
            if(position.Y >= Map?[0].Length)
            { 
                return false;
            }

            if (Map?[position.X][position.Y] is null 
                || Map[position.X][position.Y].Equals(COLUMN))
            {
                return false;
            }

            return true;
        }
    }
}
