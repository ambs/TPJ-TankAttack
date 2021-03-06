﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPJ_TankAttack
{
    class Scene
    {
        public SpriteBatch SpriteBatch {get; private set;}
        private List<Sprite> sprites;

        // Background list
        private List<SlidingBackground> backgrounds;

        public Scene(SpriteBatch sb)
        {
            this.SpriteBatch = sb;
            this.sprites = new List<Sprite>();
            this.backgrounds = new List<SlidingBackground>();
        }

        public void AddSprite(Sprite s)
        {
            this.sprites.Add(s);
            s.SetScene(this);
        }

        public void AddBackground(SlidingBackground b)
        {
            this.backgrounds.Add(b);
            b.SetScene(this);
        }

        public void RemoveSprite(Sprite s)
        {
            this.sprites.Remove(s);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var sprite in sprites.ToList())
            {
                sprite.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (sprites.Count > 0 || backgrounds.Count > 0)
            {
                this.SpriteBatch.Begin();
                // Desenhar os fundos!!!
                foreach (var background in backgrounds)
                    background.Draw(gameTime);
                
                // Desenhar as sprites!!!
                foreach (var sprite in sprites)
                    sprite.Draw(gameTime);

                this.SpriteBatch.End();
            }
        }

        public bool Collides(Sprite s, out Sprite collided,
                                       out Vector2 collisionPoint)
        {
            bool collisionExists = false;
            collided = s;  // para calar o compilador
            collisionPoint = Vector2.Zero; // para calar o compilador

            foreach (var sprite in sprites)
            {
                if (s == sprite) continue;
                if (s.CollidesWith(sprite, out collisionPoint))
                {
                    collisionExists = true;
                    collided = sprite;
                    break;
                }
            }
            return collisionExists;
        }

        public void Dispose()
        {
            foreach (var sprite in sprites)
                sprite.Dispose();

            foreach (var background in backgrounds)
                background.Dispose();
        }
    }
}
