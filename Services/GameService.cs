using RobotGame.Enums;
using RobotGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotGame.Services
{
    public class GameService
    {
        private int _gridRows;
        private int _gridColumns;

        private PositionModel _playerPosition;

        private List<PositionModel> _previousMoves;

        private bool _playingGame;

        public GameService(int gridRows, int gridColumns, bool playingGame)
        {
            _gridRows = gridRows;
            _gridColumns = gridColumns;
            _playingGame = playingGame;
            _playerPosition = new PositionModel {
                Position = new Vector2
                {

                }
            };
            _previousMoves = new List<PositionModel>();
        }

        /// <summary>
        /// Returns playing game variable
        /// </summary>
        /// <returns></returns>
        public bool IsGamePlaying()
        {
            return _playingGame;
        }

        /// <summary>
        /// Opens or closes the while loop
        /// </summary>
        public void StartEndGame()
        {
            _playingGame = !_playingGame;
        }

        /// <summary>
        /// Checks if the place move has been run yet
        /// </summary>
        /// <returns></returns>
        public bool HasGameStarted()
        {
            if (_previousMoves.Any(e => e.MoveType == MoveType.place))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Creates extensible grid size
        /// </summary>
        /// <returns></returns>
        public int[] FetchGridSize()
        {
            return new[] { _gridRows, _gridColumns };
        }

        /// <summary>
        /// Returns latest player position
        /// </summary>
        /// <returns></returns>
        public PositionModel FetchPlayerPosition()
        {
            return _playerPosition;
        }

        /// <summary>
        /// Store players current position
        /// </summary>
        /// <param name="position"></param>
        public void StorePlayerPosition(PositionModel position)
        {
            _playerPosition.Position.Y = position.Position.Y;
            _playerPosition.Position.X = position.Position.X;
            _playerPosition.CharacterDirection = position.CharacterDirection;
        }

        /// <summary>
        /// Returns latest move
        /// </summary>
        /// <returns></returns>
        public PositionModel FetchLatestMove()
        {
            return _previousMoves.Last();
        }

        /// <summary>
        /// Store input move to List of moves
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public bool StoreMove(string move)
        {
            var splitMove = move.Trim().Split(" ");
            try
            {
                if (splitMove.Length == 4)
                {
                    var possiblePosition = new PositionModel(splitMove[0], splitMove[3], splitMove[1], splitMove[2]);

                    if(possiblePosition.Position.Y > _gridRows || possiblePosition.Position.X > _gridColumns)
                    {
                        return false;
                    }

                    _previousMoves.Add(possiblePosition);
                }
                else if (splitMove.Length == 2)
                {
                    _previousMoves.Add(new PositionModel(splitMove[0], splitMove[1]));
                }
                else
                {
                    _previousMoves.Add(new PositionModel(splitMove[0]));
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Rotate character in place
        /// </summary>
        /// <param name="characterDirection"></param>
        /// <returns></returns>
        public PositionModel TurnPosition(CharacterDirection characterDirection)
        {
            _playerPosition.CharacterDirection = characterDirection;

            StorePlayerPosition(_playerPosition);

            return _playerPosition;
        }

        /// <summary>
        /// Move the player based on character direction, and catch hitting a wall
        /// </summary>
        /// <returns></returns>
        public PositionModel MovePosition()
        {
            switch (_playerPosition.CharacterDirection)
            {
                case CharacterDirection.north:
                    if(_playerPosition.Position.Y == 0)
                    {
                        return null;                        
                    }
                    _playerPosition.Position.Y -= 1;
                    break;
                case CharacterDirection.east:
                    if (_playerPosition.Position.X == _gridColumns - 1)
                    {
                        return null;
                    }
                    _playerPosition.Position.X += 1;
                    break;
                case CharacterDirection.south:
                    if (_playerPosition.Position.Y == _gridRows - 1)
                    {
                        return null;
                    }
                    _playerPosition.Position.Y += 1;
                    break;
                case CharacterDirection.west:
                    if (_playerPosition.Position.X == 0)
                    {
                        return null;
                    }
                    _playerPosition.Position.X -= 1;
                    break;
            }

            StorePlayerPosition(_playerPosition);

            return _playerPosition;
        }

        /// <summary>
        /// Replace the grid square with a directional V
        /// </summary>
        /// <param name="characterDirection"></param>
        /// <returns></returns>
        public string GenerateCharacterSprite(CharacterDirection characterDirection)
        {
            switch (characterDirection)
            {
                case CharacterDirection.south:
                    return "[v]";
                case CharacterDirection.east:
                    return "[>]";
                case CharacterDirection.west:
                    return "[<]";
                default:
                    return "[^]";
            }
        }
    }
}
