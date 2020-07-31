using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutClone
{
    interface ICollide
    {
        event EventHandler<EventArgs> Collision;

        void CheckIfCollide(Rectangle bounds);
    }
}
