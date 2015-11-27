using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAME_UNWN.Path
{
    class NormalPath
    {
        public Point begin;
        public Point end; 
        public NormalPath(Point A, Point B)
        {
            this.begin = A;
            this.end = B;
        }
    }
}
