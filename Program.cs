using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotGame.Services;
using RobotGame.Enums;

GameService _gameService = new GameService(3, 3, false);

PrinterService _printerService = new PrinterService(_gameService);
ControllerService _controllerService = new ControllerService(_gameService, _printerService);

bool _appLoop = true;

while (_appLoop)
{
    _appLoop = _printerService.PrintStartUp();

    while (_gameService.IsGamePlaying())
    {
        var input = Console.ReadLine();

        if (!_gameService.StoreMove(input.ToLower()))
        {
            _printerService.InvalidMove(0);

            continue;
        }

        _controllerService.ProcessMove();
    }
}