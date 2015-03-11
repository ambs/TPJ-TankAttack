using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPJ_TankAttack
{
    class Tank : Sprite
    {
        private Sprite turret;

        public Tank(ContentManager content) : base(content, "tank_body")
        {
            this.turret = new Sprite(content, "tank_turret"); 
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            turret.Draw(gameTime);
        }

        public override void SetScene(Scene s)
        {
            this.scene = s;
            turret.SetScene(s);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.W))
                this.position.Y += 0.01f;
            
            if (state.IsKeyDown(Keys.S))
                this.position.Y -= 0.01f;



            turret.SetPosition(this.position);

            turret.Update(gameTime);
            base.Update(gameTime);
        }
    }
}
