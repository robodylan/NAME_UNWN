﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdlDotNet.Core;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Primitives;
using SdlDotNet.Input;
using System.Drawing;
using System.Windows;
using NAME_UNWN.Drawable;
using NAME_UNWN.Path;

namespace NAME_UNWN
{
    class Program
    {
        public static int width = 1366;
        public static int height = 768;
        public static Surface videoScreen;
        public static Direction direction;
        public static bool[] directionKeys = {false, false, false, false};
        public static List<Entity> entities;
        public static List<Bullet> bullets;
        public static List<Spray> sprays;
        public static List<NormalPath> normalPath;
        public static List<Explosive> explosives;
        public static List<Entity> toKill;
        public static Point mousePosition;
        public static bool mouseClicked;
        public static Random r = new Random();
        public static Point offset = new Point();
        static void Main(string[] args)
        {
            entities = new List<Entity>();
            toKill = new List<Entity>();
            sprays = new List<Spray>();
            bullets = new List<Bullet>();
            normalPath = new List<NormalPath>();
            explosives = new List<Explosive>();
            for(int i = 0; i < 100; i++)
            {
                entities.Add(new Entity((r.Next(0, width) / 32) * 32, (r.Next(0, height) / 32) * 32, Entity.entityType.Student));
            }
            direction = Direction.None;
            videoScreen = Video.SetVideoMode(width, height, false, false, false, false, true);
            Events.Tick += Update;
            Events.Tick += displayDebugInfo;
            Events.KeyboardDown += KeyDown;
            Events.KeyboardUp += KeyUp;
            Events.MouseMotion += MouseMotion;
            Events.MouseButtonDown += MouseButtonDown;
            Events.Quit += Quit;
            Events.TargetFps = 144;
            Mouse.ShowCursor = false;

            setupTmpLevel();

            Events.Run();
        }

        public static void MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.PrimaryButton)
            {
                mouseClicked = true;
            }
            if (e.Button == MouseButton.SecondaryButton)
            {
                normalPath.Add(new Point(mousePosition.X + offset.X, mousePosition.Y + offset.Y));
            }
        }

        public static void MouseMotion(object sender, MouseMotionEventArgs e)
        {
            mousePosition = new Point(e.X, e.Y);
        }

        public static void Update(object sender, TickEventArgs e)
        {
            foreach(Entity kills in toKill)
            {
                entities.Remove(kills);
            }
            videoScreen.Fill(Color.DarkOliveGreen);
            List<IDrawable> drawable = new List<IDrawable>();
            drawable.AddRange(bullets);
            drawable.AddRange(sprays);
            drawable.AddRange(explosives);
            drawable.AddRange(entities);
            foreach (IDrawable entity in drawable)
            {
                entity.Update(directionKeys, entities, mousePosition, mouseClicked);
                entity.Draw(videoScreen);
            }
            //Begin debug draw
            Circle c = new Circle(mousePosition, 8);
            c.Draw(videoScreen, Color.Red, true, false);
            c.Draw(videoScreen, Color.FromArgb(32, 255, 0, 0), true, true);
            mouseClicked = false;
            foreach(Point p in normalPath)
            {
                c.Center = new Point(p.X - offset.X, p.Y - offset.Y);
                c.Draw(videoScreen, Color.Blue, true, false);
                c.Draw(videoScreen, Color.FromArgb(32, 128, 128, 0), true, true);
            }
            videoScreen.Update();
        }

        public static void Quit(object sender, QuitEventArgs e)
        {
            Events.QuitApplication();
        }

        public static void KeyDown(object sender, KeyboardEventArgs e)
        {
            if(e.Key == Key.LeftShift)
            {
                explosives.Add(new Explosive(mousePosition.X, mousePosition.Y, Explosive.explosiveType.bomb));
            }
            switch(e.Key)
            {
                case Key.W:
                    directionKeys[0] = true;
                    break;
                case Key.A:
                    directionKeys[1] = true;
                    break;
                case Key.S:
                    directionKeys[2] = true;
                    break;
                case Key.D:
                    directionKeys[3] = true;
                    break;
            }
        }

        public static void KeyUp(object sender, KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    directionKeys[0] = false;
                    break;
                case Key.A:
                    directionKeys[1] = false;
                    break;
                case Key.S:
                    directionKeys[2] = false;
                    break;
                case Key.D:
                    directionKeys[3] = false;
                    break;
            }
        }

        public static void displayDebugInfo(object sender, TickEventArgs e)
        {

        }

        public enum Direction
        {
            Up,
            Down,
            Right,
            Left,
            None
        }

        public static void shotsFired(int x, int y, Entity shooter)
        {
            Point originOfShot = shooter.position;
            Point DestinationOfShot = new Point(x,y);
            foreach (Entity e in entities)
            {
                int dX = (e.position.X + 16)- x;
                int dY = (e.position.Y + 16) - y;
                if (dX * dX + dY * dY < (32 * 32) && e.type != Entity.entityType.Player)
                {
                    e.health -= new Random().Next(25, 100);
                }
            }
        }

        public static void setupTmpLevel()
        {
            entities.Add(new Entity(0, 0, Entity.entityType.Player));
            sprays.Add(new Spray(0, 0, Spray.sprayType.blood));
        }
    }
}
