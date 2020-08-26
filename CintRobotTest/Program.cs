using CintRobot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CintRobotTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Take Input
            RobotModel robot = new RobotModel();
            List<string> givenCommands = new List<string>();
            Console.WriteLine("Number of commands: ");
            string numberOfCommands = Console.ReadLine();
            Console.WriteLine("Starting Coordinates: ");
            string coordinates = Console.ReadLine();
            for (int i = 1; i <= Convert.ToInt32(numberOfCommands); i++)
            {
                Console.WriteLine($"Enter command {i}:");
                givenCommands.Add(Console.ReadLine());
            }

            //Execute cleaning and display results
            var service = new RobotService();
            int cleanedSpots = service.ExecuteCommands(givenCommands, coordinates);
            Console.WriteLine($"=> Cleaned: {cleanedSpots}");
            Console.ReadLine();
        }
    }

    public class RobotService
    {

        public int ExecuteCommands(List<string> commands, string startLocation)
        {
            string currentLocation = startLocation;
            List<string> coordinatesTouched = new List<string>
            {
                startLocation
            };
            foreach (string command in commands)
            {
                var commandParsed = command.Split(' ');
                int moveAmount = Convert.ToInt32(commandParsed[1]);
                string moveDirection = commandParsed[0];
                var touchedCoordinates = CalculateCoordinatesTouched(currentLocation, moveDirection, moveAmount, out currentLocation);
                coordinatesTouched.AddRange(touchedCoordinates);
            }

            //Get unique spots cleaned
            coordinatesTouched = coordinatesTouched.Distinct().ToList();
            return coordinatesTouched.Count;
        }

        public List<string> CalculateCoordinatesTouched(string currentLocation, string direction, int moveAmount, out string currentPosition)
        {
            var parseCurrentLocation = currentLocation.Split(' ');
            var x = Convert.ToInt32(parseCurrentLocation[0]);
            var y = Convert.ToInt32(parseCurrentLocation[1]);
            List<string> coordinatesTouched = new List<string>();
            
            for(int i = 0; i < moveAmount; i++)
            {
                switch (direction)
                {
                    case "N":
                        x++;
                        coordinatesTouched.Add($"{x} {y}");
                        break;
                    case "S":
                        x--;
                        coordinatesTouched.Add($"{x} {y}");
                        break;
                    case "E":
                        y++;
                        coordinatesTouched.Add($"{x} {y}");
                        break;
                    case "W":
                        y--;
                        coordinatesTouched.Add($"{x} {y}");
                        break;
                }
            }
            currentPosition = $"{x} {y}";
            return coordinatesTouched;
        }
    }
}
