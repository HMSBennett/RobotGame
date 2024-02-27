using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotGame.Enums;
using RobotGame.Models;

namespace RobotGame.Services
{
    public class PrinterService
    {
        GameService _gameService;

        public PrinterService(GameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Prints out initial message and allowing user to chose between game, rules and quitting
        /// </summary>
        /// <returns></returns>
        public bool PrintStartUp()
        {
            bool startUp = true;

            while (startUp)
            {
                Console.WriteLine("Welcome to Robot Game");
                Console.WriteLine("---------------------");
                Console.WriteLine("1 | Play Game");
                Console.WriteLine("2 | Help");
                Console.WriteLine("3 | Quit");

                var readInput = Console.ReadLine();

                switch (readInput)
                {
                    case "1":
                        _gameService.StartEndGame();
                        return true;
                    case "2":
                        PrintHelp();
                        break;
                    case "3":
                        startUp = false;
                        break;
                }
            }

            return false;
        }

        /// <summary>
        /// Prints out a wall of game instructions
        /// </summary>
        public void PrintHelp()
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("Objective:");
            Console.WriteLine("---------------------");
            Console.WriteLine("Move a robot around a 0 based index 3x3 board");
            Console.WriteLine("---------------------");
            Console.WriteLine("Controls:");
            Console.WriteLine("---------------------");
            Console.WriteLine("PLACE <X> <Y> <DIR> | Places the robot on a location on the board, for example 'PLACE 2 0 NORTH'");
            Console.WriteLine("TURN <DIR> | Rotates the robot in its current position, for example 'TURN NORTH'");
            Console.WriteLine("MOVE | Makes the robot take one step forward (if not at edge of board)");
            Console.WriteLine("PRINT | Tells you the current position of the robot");
            Console.WriteLine("---------------------");
        }

        /// <summary>
        /// Prints actual game screen depending on which move is selected
        /// </summary>
        /// <param name="position"></param>
        public void PrintGrid(PositionModel position)
        {
            var gridSize = _gameService.FetchGridSize();
            Array grid = Array.CreateInstance(typeof(string), gridSize);

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid.SetValue("[ ]", i, j);

                    if (i == position.Position.Y && j == position.Position.X)
                    {
                        var characterSprite = _gameService.GenerateCharacterSprite(position.CharacterDirection);

                        grid.SetValue(characterSprite, i, j);
                    }
                    Console.Write(grid.GetValue(i, j) + "\t");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints players current position
        /// </summary>
        /// <param name="position"></param>
        public void PrintPosition(PositionModel position)
        {
            Console.WriteLine($"{position.Position.X} {position.Position.Y} {position.CharacterDirection}");
        }

        /// <summary>
        /// Throws a soft error depending on what went wrong
        /// </summary>
        /// <param name="invalidType"></param>
        public void InvalidMove(int invalidType)
        {
            switch (invalidType)
            {
                case 0:
                    Console.WriteLine("Invalid Instruction");
                    break;
                case 1:
                    Console.WriteLine("Error: First instruction must be PLACE");
                    break;
                case 2:
                    Console.WriteLine("Stop! Going to fall!");
                    break;
            }
        }
    }
}
