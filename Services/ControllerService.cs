using RobotGame.Enums;
using RobotGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotGame.Services
{
    public class ControllerService
    {
        GameService _gameService;
        PrinterService _printerService;

        public ControllerService(GameService gameService, PrinterService printerService)
        {
            _gameService = gameService;
            _printerService = printerService;
        }

        /// <summary>
        /// Figures out what to do based on the latest move added to the Game Service
        /// </summary>
        public void ProcessMove()
        {
            PositionModel latestMove = _gameService.FetchLatestMove();

            if (!_gameService.HasGameStarted())
            {
                _printerService.InvalidMove(1);

                return;
            }

            switch (latestMove.MoveType)
            {
                case MoveType.place:
                    _gameService.StorePlayerPosition(latestMove);

                    _printerService.PrintGrid(latestMove);
                    break;
                case MoveType.turn:
                    var turnPosition = _gameService.TurnPosition(latestMove.CharacterDirection);

                    _printerService.PrintGrid(turnPosition);
                    break;
                case MoveType.move:
                    var movePosition = _gameService.MovePosition();

                    if(movePosition == null)
                    {
                        _printerService.InvalidMove(2);
                        break;
                    }

                    _printerService.PrintGrid(movePosition);
                    break;
                case MoveType.print:
                    _printerService.PrintPosition(_gameService.FetchPlayerPosition());
                    break;
                default:
                    _printerService.InvalidMove(0);
                    break;
            }

            return;
        }
    }
}
