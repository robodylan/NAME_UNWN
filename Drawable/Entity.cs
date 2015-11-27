using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdlDotNet.Graphics;
using SdlDotNet.Core;
using System.Drawing;

namespace NAME_UNWN.Drawable
{
    class Entity : IDrawable
    {
        public static Surface playerTexture = new Surface("Resources/images/entityTexture.png");
        public static Surface teacherTexture = new Surface("Resources/images/teacherTexture.png");
        public static Surface studentTexture = new Surface("Resources/images/studentTexture.png");
        public Surface spriteTexture { get; set; }
        public Surface weaponTexture { get; set; }
        public Point position { get; set; }
        public float rotation { get; set; }
        public int health { get; set; }
        public entityType type;
        public int timeSinceClick;
        public Entity(int x, int y, entityType entityType)
        {
            type = entityType;
            spriteTexture = playerTexture;
            switch (entityType)
            {
                case entityType.Player:
                    this.health = 100;
                    spriteTexture = playerTexture; 
                    break;
                case entityType.Student:
                    this.health = 100;
                    spriteTexture = studentTexture;
                    break;
                case entityType.Teacher:
                    this.health = 100;
                    spriteTexture = teacherTexture;
                    break;
                default:
                    spriteTexture = playerTexture;
                    break;
            }
            position = new Point(x,y);
        }

        public void Update(bool[] directionKeys, List<Entity> entities, Point mousePosition, bool mouseClicked)
        {
            if(mouseClicked && type == entityType.Player && timeSinceClick > (144 / 3))
            {
                timeSinceClick = 0;
                Program.bullets.Add(new Bullet(position.X, position.Y, rotation - 90, this));
            }
            else
            {
                timeSinceClick++;
            }
            int xToMove = 0;
            int yToMove = 0;
            int speed = 4;
            switch (this.type)
            {
                case entityType.Player:
                    Program.offset = new Point(position.X - (Program.width / 2), position.Y - (Program.height / 2));
                    rotation = 90 + (int)(57 * (float)(Math.Atan2(((Program.mousePosition.Y + Program.offset.Y) - position.Y - 16), ((Program.mousePosition.X + Program.offset.X) - position.X - 16))));
                    if (directionKeys[0])
                    {
                        yToMove -= speed;
                    }
                    if (directionKeys[1])
                    {
                        xToMove -= speed;
                    }
                    if (directionKeys[2])
                    {
                        yToMove += speed;
                    }
                    if (directionKeys[3])
                    {
                        xToMove += speed;
                    }
                    break;
                case entityType.Student:
                    Point toFollow = new Point(123456789,0);
                    int shortestDistance = 1000000;
                    foreach(Point p in Program.normalPath)
                    {
                        int distance = (p.X - position.X * p.X - position.X) + (p.Y - position.Y * p.Y - position.Y);
                        if(distance < shortestDistance && distance < 100)
                        {
                            shortestDistance = distance;
                            toFollow = p;
                        }
                    }
                    if(toFollow.X != 123456789)
                    {
                        if (position.X < toFollow.X) xToMove += Program.r.Next(0, 4);
                        if (position.X > toFollow.X) xToMove -= Program.r.Next(0, 4); 
                        if (position.Y < toFollow.Y) yToMove += Program.r.Next(0, 4); 
                        if (position.Y > toFollow.Y) yToMove -= Program.r.Next(0, 4);
                    }
                    break;
            }
            position = new Point(position.X + xToMove, position.Y + yToMove);
            if (health <= 0)
            {
                deathHandle();
            }
        }

        public void Draw(Surface renderTarget)
        {
            renderTarget.Blit(spriteTexture.CreateRotatedSurface((int)-rotation, true), new Point(position.X - Program.offset.X, position.Y - Program.offset.Y));
        }

        public enum entityType
        {
            Student,
            Player, 
            Teacher,
            Police
        }

        public void deathHandle()
        {
            Program.sprays.Add(new Spray(position.X, position.Y, Spray.sprayType.blood));
            Program.entities.Remove(this);
        }
    }
}
