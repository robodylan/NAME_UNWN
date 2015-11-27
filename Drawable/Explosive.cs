using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdlDotNet.Graphics;
using SdlDotNet.Audio;

namespace NAME_UNWN.Drawable
{
    class Explosive : IDrawable
    {
        public Surface spriteTexture { get; set; }
        public Point position { get; set; }
        public float rotation { get; set; }
        public explosiveType type;
        public int time;
        public Sound sound;
        public static int numberOfSounds;

        public Explosive(int x, int y, explosiveType type)
        {
            this.time = 550;
            this.position = new Point(x + Program.offset.X,y + Program.offset.Y);
            this.type = type;
            switch(type)
            {
                case explosiveType.bomb:
                    spriteTexture = new Surface("Resources/images/bombTexture.png");
                    sound = new Sound("Resources/sound/11633_1408804876.ogg");
                    sound.Volume = 64;
                    numberOfSounds++;
                    if (!(numberOfSounds > 8))
                    {
                        sound.Play();
                    }
                    break;
            }
        }

        public void Draw(Surface renderTarget)
        {
            if (time > 0)
            {
                renderTarget.Blit(spriteTexture.CreateRotatedSurface((int)-rotation, true), new Point(position.X - Program.offset.X, position.Y - Program.offset.Y));
            }
        }

        public void Update(bool[] directionKeys, List<Entity> entities, Point mousePosition, bool mouseClicked)
        {
            time--;
            if(time == 0)
            {
                Explode();
                Program.sprays.Add(new Spray(position.X, position.Y, Spray.sprayType.explosion));
            }
            if(time < -200)
            {
                numberOfSounds--;
                sound.Dispose();
                Program.explosives.Remove(this);
            }

        }

        public enum explosiveType
        {
            bomb,
            grenade
        }

        public void Explode()
        {
            foreach(Entity entity in Program.entities)
            {
                int dX = entity.position.X - position.X;
                int dY = entity.position.Y - position.Y;
                if((dX * dX) + (dY * dY) < (256 * 256))
                {
                    Program.toKill.Add(entity);
                }
            }
        }
    }
}
