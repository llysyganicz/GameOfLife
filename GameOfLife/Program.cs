﻿using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameOfLife
{
    class Program
    {
        private const int Width = 100;
        private const int Height = 100;
        private static GameBoard _board;
        private static SadConsole.Console _console;

        static void Main()
        {
            // Setup the engine and creat the main window.
            SadConsole.Game.Create("Cheepicus12.font", Width, Height);
            
            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Hook the update event that happens each frame so we can trap keys and respond.
            SadConsole.Game.OnUpdate = Update;

            SadConsole.Game.OnDraw = Draw;

            SadConsole.Game.Instance.IsFixedTimeStep = true;
            SadConsole.Game.Instance.TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 250);
            
            // Start the game.
            SadConsole.Game.Instance.Run();
            
            //
            // Code here will not run until the game window closes.
            //
            
            SadConsole.Game.Instance.Dispose();
        }
        
        private static void Update(GameTime time)
        {
            // Called each logic update.
            Task.Run(() =>
            {
                _board.Update();
            });
            
            // As an example, we'll use the F5 key to make the game full screen
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.F5))
            {
                SadConsole.Settings.ToggleFullScreen();
            }
            
            if (SadConsole.Global.KeyboardState.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                SadConsole.Game.Instance.Exit();
            }
        }

        private static void Draw(GameTime time)
        {
            PrintBoard();
        }

        private static void Init()
        {
            // Any custom loading and prep. We will use a sample console for now

            _console = new SadConsole.Console(Width, Height);

            _board = new GameBoard(Width, Height);
            var random = new Random();
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    _board.Board[i, j] = random.Next(2) == 1;
                }
            }
            PrintBoard();
            
            // Set our new console as the thing to render and process
            SadConsole.Global.CurrentScreen = _console;
        }

        private static void PrintBoard()
        {
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    _console.Fill(new Rectangle(i, j, 1, 1), null, _board.Board[i, j] ? Color.Wheat : Color.Blue, 0);
                }
            }
        }
    }
}