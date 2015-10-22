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
        public Surface entityTexture { get; set; }
        public Surface weaponTexture { get; set; }
        public Point position { get; set; }
        public Entity(int x, int y, entityType entityType)
        {
            entityTexture = new Surface("Resources/entityTexture.bmp");
            position = new Point(x,y);
        }

        public void Update()
        {
            int xToMove = 0;
            int yToMove = 0;
            int speed = 5;
            switch(Program.direction)
            {
                case Program.Direction.Up:
                    yToMove -= speed;
                    break;
                case Program.Direction.Down:
                    yToMove += speed;
                    break;
                case Program.Direction.Left:
                    xToMove -= speed;
                    break;
                case Program.Direction.Right:
                    xToMove += speed;
                    break;
            }
            position = new Point(position.X + xToMove, position.Y + yToMove);
        }

        public void Draw(Surface renderTarget)
        {
            renderTarget.Blit(entityTexture, position);
        }

        public enum entityType
        {
            Student,
            Player, 
            Teacher,
            Police
        }
    }
}
