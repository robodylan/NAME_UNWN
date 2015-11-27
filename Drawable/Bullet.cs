using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdlDotNet.Graphics;

namespace NAME_UNWN.Drawable
{
    class Bullet : IDrawable
    {
        public Surface spriteTexture { get; set; }
        public Point position { get; set; }
        public float rotation { get; set; }
        public Entity shooter;
        public int time;

        public Bullet(int x, int y, float rotationInDegrees, Entity shooter)
        {
            float speed = 0;
            float XtoMove = speed * (float)Math.Cos(rotation * 0.0174533f);
            float YtoMove = speed * (float)Math.Sin(rotation * 0.0174533f);
            this.position = new Point(x + (int)XtoMove,y + (int)YtoMove);
            this.rotation = rotationInDegrees;
            this.shooter = shooter;
            this.spriteTexture = new Surface("Resources/images/bulletTexture.png");
        }

        public void Draw(Surface renderTarget)
        {
            renderTarget.Blit(spriteTexture.CreateRotatedZoomedSurface((int)-rotation + 45, 1, true), new Point(position.X - Program.offset.X, position.Y - Program.offset.Y));
        }

        public void Update(bool[] directionKeys, List<Entity> entities,Point mousePosition, bool mouseClicked)
        {
            time++;
            if(time > 1000)
            {
                Program.bullets.Remove(this);
            }
            foreach(Entity entity in entities)
            {
                int dX = entity.position.X - position.X;
                int dY = entity.position.Y - position.Y;
                if (dX * dX + dY * dY < (32 * 32) && !entity.Equals(shooter))
                {
                    Program.bullets.Remove(this);
                    entity.health -= new Random().Next(50, 150); //Needs revision
                }
            }
            float speed = 16;
            float XtoMove = speed * (float)Math.Cos(rotation * 0.0174533f);
            float YtoMove = speed * (float)Math.Sin(rotation * 0.0174533f);
            this.position = new Point(position.X + (int)XtoMove, position.Y + (int)YtoMove);
        }
    }
}
