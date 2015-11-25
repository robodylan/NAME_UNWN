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
        public sprayType type;

        public Spray(int x, int y, sprayType type)
        {
            this.position = new Point(x,y);
            this.type = type;
            switch(type)
            {
                case sprayType.blood:
                    spriteTexture = new Surface("Resources/bloodTexture.png");
                    break;
                case sprayType.bullet:
                    break;
                case sprayType.fire:
                    break;

            }
        }

        public void Draw(Surface renderTarget)
        {
            renderTarget.Blit(spriteTexture, position);
        }

        public void Update(bool[] directionKeys, Point mousePosition, bool mouseClicked)
        {
           
        }

        public enum sprayType
        {
            bullet,
            blood,
            fire
        }
    }
}
