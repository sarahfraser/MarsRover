using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover
{
    class MainClass
    {
        public static int[] Grid;
        public static List<Robot> RobotList = new List<Robot>();

        public static void Main(string[] args)
        {
            SetGrid();
            AddRobot();
            Execute();
        }
        
        public static void SetGrid()
        {
            Console.WriteLine("Please input Gridsize: x y");
            var gridString = Console.ReadLine();
            Grid = Array.ConvertAll(gridString.Split(' '), int.Parse);
        }

        public static void AddRobot()
        {
            Console.WriteLine("Please enter robot initial state and path: (x, y, N) FRL...");
            var str = Console.ReadLine();
            var words = str.Split('(', ')', ',', ' ').Where(s => !string.IsNullOrEmpty(s)).ToArray(); // Split string on delimiters

            var robot = new Robot(int.Parse(words[0]), int.Parse(words[1]), char.Parse(words[2]), words[3]);
            RobotList.Add(robot);

            Console.WriteLine("Press 'E' to execute, 'A' to add another robot");
            var cki = Console.ReadKey();

            if (cki.KeyChar == 'E' || cki.KeyChar == 'e')
            {
                // Go back to main and Execute
                return;

            }
            else if (cki.KeyChar == 'A' || cki.KeyChar == 'a')
            {
                AddRobot();

            }

        }

        public static void Execute()
        {
            Console.WriteLine("Executing...");

            foreach(var robot in RobotList)
            {
                foreach (var step in robot.Path)
                {
                    if (step == 'F')
                    {
                        int y = robot.Y;
                        int x = robot.X;

                        if (robot.Orientation == 'N')
                        {
                            y += 1;
                        }
                        else if (robot.Orientation == 'E')
                        {
                            x += 1;
                        }
                        else if (robot.Orientation == 'S')
                        {
                            y += -1;
                        }
                        else if (robot.Orientation == 'W')
                        {
                            x += -1;
                        }

                        if (x > Grid[0] || x < 0 || y > Grid[1] || y < 0)
                        {
                            robot.IsLost = true;
                            break;
                        }
                        else
                        {
                            // Update robot position properties
                            robot.X = x;
                            robot.Y = y;
                        }
                    }
                    if (step == 'R')
                    {
                        if (robot.Orientation == 'N')
                        {
                            robot.Orientation = 'E';
                        }
                        else if (robot.Orientation == 'E')
                        {
                            robot.Orientation = 'S';
                        }
                        else if (robot.Orientation == 'S')
                        {
                            robot.Orientation = 'W';
                        }
                        else if (robot.Orientation == 'W')
                        {
                            robot.Orientation = 'N';
                        }
                    }
                    if (step == 'L')
                    {
                        if (robot.Orientation == 'N')
                        {
                            robot.Orientation = 'W';
                        }
                        else if (robot.Orientation == 'W')
                        {
                            robot.Orientation = 'S';
                        }
                        else if (robot.Orientation == 'S')
                        {
                            robot.Orientation = 'E';
                        }
                        else if (robot.Orientation == 'E')
                        {
                            robot.Orientation = 'N';
                        }
                    }
                }
                var outputString = robot.IsLost ?
                    "(" + robot.X.ToString() + ", " + robot.Y.ToString() + ", " + robot.Orientation.ToString() + ") LOST" :
                    "(" + robot.X.ToString() + ", " + robot.Y.ToString() + ", " + robot.Orientation.ToString() + ")";
                Console.WriteLine(outputString + System.Environment.NewLine);
            }
        }
    }

    public class Robot
    {
        // Backing stores
        private int _x;  
        private int _y;
        private char _orientation;
        private bool _isLost;
        private string _path;


        public Robot(int x, int y, char orientation, string path)
        {
            _x = x;
            _y = y;
            _orientation = orientation;
            _path = path;
        }


        // Public properties
        public string Path => _path;

        public int X
        {
            get => _x;
            set
            {
                _x = value;
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                _y = value;
            }
        }

        public char Orientation
        {
            get => _orientation;
            set
            {
                _orientation = value;
            }
        }

        public bool IsLost
        {
            get => _isLost;
            set
            {
                _isLost = value;
            }
        }
    }
}
