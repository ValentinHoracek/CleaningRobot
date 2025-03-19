using System.Text.Json;

namespace CleaningRobot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filesFolder = Path.Combine(Directory.GetCurrentDirectory(), "files");

            Robot? robot = ReadFile(Path.Combine(filesFolder, args[0]));

            if (robot is null)
            {
                return;
            }

            // Cals to execute commands
            


            // Add something to output
            robot.Visited.Add(new(0, 0));
            robot.Cleaned.Add(new(0, 1));
            robot.FinalPosition = new(1, 1, "N");

            WriteFile(Path.Combine(filesFolder, args[1]), robot);
        }

        static Robot? ReadFile(string path)
        {
            string jsonString = File.ReadAllText(path);
            Robot? robot = JsonSerializer.Deserialize<Robot>(jsonString);
            return robot;
        }

        static void WriteFile(string path, Robot robot)
        {
            robot.Map = null;
            robot.StartPosition = null;
            robot.Commands = null;

            string jsonString = JsonSerializer.Serialize(robot);
            File.WriteAllText(path, jsonString);
        }
    }
}
