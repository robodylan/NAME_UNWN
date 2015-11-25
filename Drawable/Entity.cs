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
        public static Surface playerTexture = new Surface("Resources/entityTexture.png");
        //public static Surface teacherTexture = new Surface("Resources/teacherTexture.png");
        public static Surface studentTexture = new Surface("Resources/studentTexture.png");
        public Surface spriteTexture { get; set; }
        public Surface weaponTexture { get; set; }
        public Point position { get; set; }
        public float rotation { get; set; }
        public int health { get; set; }
        public entityType type;
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
                    break;
                default:
                    spriteTexture = playerTexture;
                    break;
            }
            position = new Point(x,y);
        }

        public void Update(bool[] directionKeys, Point mousePosition, bool mouseClicked)
        {
            if(mouseClicked && type == entityType.Player)
            {
                Program.bullets.Add(new Bullet(position.X, position.Y, rotation - 90));
            }
            switch(this.type)
            {
                case entityType.Player:
                rotation = 90 + (int)(57 * (float)(Math.Atan2((Program.mousePosition.Y - position.Y - 15), (Program.mousePosition.X - position.X - 15))));
                int xToMove = 0;
                int yToMove = 0;
                int speed = 4;
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
                position = new Point(position.X + xToMove, position.Y + yToMove);
                break;
            }
            if(health <= 0)
            {
                deathHandle();
            }
        }

        public void Draw(Surface renderTarget)
        {
            renderTarget.Blit(spriteTexture.CreateRotatedZoomedSurface((int)-rotation, 1, true), position);
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
            this.position = new Point(100);
        }
    }
}
