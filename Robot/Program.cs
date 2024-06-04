using System;

namespace Robot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Robot Game!");
            Console.WriteLine("Select the name of the robot: Terminator or KillerBot");
            string robotName = Console.ReadLine();

            while (robotName != "Terminator" && robotName != "KillerBot")
            {
                Console.WriteLine("Invalid choice. Please select either 'Terminator' or 'KillerBot':");
                robotName = Console.ReadLine();
            }

            Robot robot = new Robot(robotName, 500);

            Console.WriteLine("Select the planet: Earth, Mars or Jupiter");
            string planetChoice = Console.ReadLine();

            while (planetChoice != "Earth" && planetChoice != "Mars" && planetChoice != "Jupiter")
            {
                Console.WriteLine("Invalid choice. Please select either 'Earth', 'Mars', or 'Jupiter':");
                planetChoice = Console.ReadLine();
            }

            robot.CurrentTarget = (Robot.Planet)Enum.Parse(typeof(Robot.Planet), planetChoice);

            if (robot.CurrentTarget == Robot.Planet.Earth)
            {
                robot.KillInitialPopulation();
                robot.DisplayPopulationStats();
                if (robot.Fight(new Superman(300, true)) > 0)
                {
                    robot.KilledPopulation = robot.Population;
                    Console.WriteLine($"{robot.Name} killed the whole population.");
                    Console.WriteLine($"{robot.Name} destroyed the planet. {robot.Name} won!");
                }
                else
                {
                    Console.WriteLine("The planet has been saved.");
                }
            }
            else
            {
                Console.WriteLine($"{robot.Name} arrived on planet {robot.CurrentTarget}.");
                Console.WriteLine("There is no life on this planet. (You should choose Earth if you want some action)");
                Console.WriteLine($"{robot.Name} destroyed {robot.CurrentTarget}. {robot.Name} won!");
            }
        }
    }
}

 