using RobotGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotGame.Models
{
    public class PositionModel
    {
        public PositionModel()
        {

        }

        /// <summary>
        /// Specifically for place
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="characterDirection"></param>
        /// <param name="moveType"></param>
        public PositionModel(string moveType, string characterDirection, string x, string y)
        {
            Position = new Vector2
            {
                X = ConvertCoordinate(x),
                Y = ConvertCoordinate(y),
            };
            CharacterDirection = Enum.Parse<CharacterDirection>(characterDirection);
            MoveType = Enum.Parse<MoveType>(moveType);
        }

        /// <summary>
        /// Specifically for turn
        /// </summary>
        /// <param name="characterDirection"></param>
        /// <param name="moveType"></param>
        public PositionModel(string moveType, string characterDirection)
        {
            CharacterDirection = Enum.Parse<CharacterDirection>(characterDirection);
            MoveType = Enum.Parse<MoveType>(moveType);
        }

        /// <summary>
        /// For either move or print
        /// </summary>
        /// <param name="moveType"></param>
        public PositionModel(string moveType)
        {
            MoveType = Enum.Parse<MoveType>(moveType);
        }

        public Vector2 Position { get; set; }
        public CharacterDirection CharacterDirection { get; set; }
        public MoveType MoveType { get; set; }

        /// <summary>
        /// Attempts to convert input into an int
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int ConvertCoordinate(string input)
        {
            bool success = int.TryParse(input, out int output);

            if(success)
            {
                return output;
            }

            return 0;
        }
    }

    public class Vector2
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
