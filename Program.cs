using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdlDotNet.Core;
using SdlDotNet.Graphics;
using SdlDotNet.Input;

namespace NAME_UNWN
{
    class Program
    {
        public static int width = 800;
        public static int height = 600;
        public static Surface videoScreen;
        public static Direction direction;
        static void Main(string[] args)
        {
            videoScreen = Video.SetVideoMode(width, height, false, false, false);
            Events.Tick += Update;
            Events.Tick += displayDebugInfo;
            Events.KeyboardDown += KeyDown;
            Events.KeyboardUp += KeyUp;
            Events.Quit += Quit;
            Events.Run();
        }

        public static void Update(object sender, TickEventArgs e)
        {
              
        }

        public static void Quit(object sender, QuitEventArgs e)
        {
            Events.QuitApplication();
        }

        public static void KeyDown(object sender, KeyboardEventArgs e)
        {
            switch(e.Key)
            {
                case Key.W:
                    direction = Direction.Up;
                    break;
                case Key.A:
                    direction = Direction.Left;
                    break;
                case Key.S:
                    direction = Direction.Down;
                    break;
                case Key.D:
                    direction = Direction.Right;
                    break;
            }
        }

        public static void KeyUp(object sender, KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    if(direction == Direction.Up)
                    {
                        direction = Direction.None;
                    }
                    break;
                case Key.A:
                    if (direction == Direction.Left)
                    {
                        direction = Direction.None;
                    }
                    break;
                case Key.S:
                    if (direction == Direction.Down)
                    {
                        direction = Direction.None;
                    }
                    break;
                case Key.D:
                    if (direction == Direction.Right)
                    {
                        direction = Direction.None;
                    }
                    break;
            }
        }

        public static void displayDebugInfo(object sender, TickEventArgs e)
        {
            Console.Clear();
            Console.WriteLine("Current Direction: " + direction);
        }

        public enum Direction
        {
            Up,
            Down,
            Right,
            Left,
            None
        }
    }
}
