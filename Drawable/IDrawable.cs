using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdlDotNet.Graphics;
using System.Drawing;

namespace NAME_UNWN.Drawable
{
    interface IDrawable
    {
        Surface spriteTexture { get; set; }
        Point position { get; set; }
        float rotation { get; set; }
        void Update(bool[] directionKeys, List<Entity> entities, Point mousePosition, bool mouseClicked);
        void Draw(Surface renderTarget);
    }
}
