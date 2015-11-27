using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdlDotNet.Graphics;

namespace NAME_UNWN.Drawable
{
    class Spray : IDrawable
    {
        public Surface spriteTexture { get; set; }
        public Point position { get; set; }
        public float rotation { get; set; }
        public static Random sprayRandom = new Random();
        public sprayType type;

        public Spray(int x, int y, sprayType type)
        {
            this.rotation = sprayRandom.Next(0, 360);
            this.position = new Point(x,y);
            this.type = type;
            switch(type)
            {
                case sprayType.blood:
                    spriteTexture = new Surface("Resources/images/bloodTexture.png");
                    break;
                case sprayType.bullet:
                    break;
                case sprayType.fire:
                    break;
                case sprayType.explosion:
                    spriteTexture = new Surface("Resources/images/explosionTexture.png");
                    break;

            }
        }

        public void Draw(Surface renderTarget)
        {
            renderTarget.Blit(spriteTexture.CreateRotatedSurface((int)rotation), new Point(position.X - Program.offset.X, position.Y - Program.offset.Y));
        }

        public void Update(bool[] directionKeys, List<Entity> entities, Point mousePosition, bool mouseClicked)
        {
           
        }

        public enum sprayType
        {
            bullet,
            blood,
            fire,
            explosion
        }
    }
}
