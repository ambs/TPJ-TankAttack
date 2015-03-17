using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPJ_TankAttack
{
    class Soldier: AnimatedSprite
    {
        public Soldier(ContentManager content): base(content, "soldado",1,3)
        {
            this.Scale(0.2f);
        }
    }
}
