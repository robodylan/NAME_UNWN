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
        Surface entityTexture { get; set; }
        Surface weaponTexture { get; set; }
        Point position { get; set; }
        void Update();
        void Draw(Surface renderTarget);
    }
}
