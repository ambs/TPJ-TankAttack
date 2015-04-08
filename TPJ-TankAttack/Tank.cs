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
        private float fireInterval = 0.5f;
        private float fireCounter = 0f;

        public Tank(ContentManager content) : base(content, "tank_body")
        {
            this.turret = new Sprite(content, "tank_turret");
            this.turret.SetRotation((float)Math.PI / 4);
            this.EnableCollisions();
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
            MouseState mstate = Mouse.GetState();
            Point mpos = mstate.Position;

            Vector2 tpos = Camera.WorldPoint2Pixels(position);
            float a = (float) mpos.Y - tpos.Y;
            float l = (float)mpos.X - tpos.X;
            float rot = (float)Math.Atan2(a, l);
            rot += (float)Math.PI/2f;
            turret.SetRotation(rot);

            fireCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (fireCounter >= fireInterval &&
                mstate.LeftButton == ButtonState.Pressed)
            {
                Vector2 pos = this.position 
                         + new Vector2((float)Math.Sin(rot) * size.Y/2,
                                       (float)Math.Cos(rot) * size.Y/2);
                Bullet bullet = new Bullet(cManager, pos, rot);
                scene.AddSprite(bullet);
                fireCounter = 0f;
            }


            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.W))
            {
                this.position.Y += 0.01f;
                Sprite other;
                Vector2 colPosition;
                if (scene.Collides(this, out other, out colPosition))
                    this.position.Y -= 0.01f;
            }
            if (state.IsKeyDown(Keys.S))
            {
                this.position.Y -= 0.01f;
                Sprite other;
                Vector2 colPosition;
                if (scene.Collides(this, out other, out colPosition))
                    this.position.Y += 0.01f;
            }




            turret.SetPosition(this.position);
            Camera.SetTarget(this.position);

            turret.Update(gameTime);
            base.Update(gameTime);
        }
    }
}
