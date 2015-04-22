using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPJ_TankAttack
{
    class SlidingBackground
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 size; // World size
        private Vector2 origin; // Center of image in pixels
        private Vector2 lastCameraPosition;
        private float speedRatio = 0f; // sliding speed
        private Scene scene;

        public SlidingBackground(ContentManager manager, string assetName)
        {
            texture = manager.Load<Texture2D>(assetName);
            origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            lastCameraPosition = Camera.GetTarget();
            position = Camera.GetTarget();
            size = new Vector2(0.5f * Camera.worldWidth,
                0.5f * Camera.worldWidth * texture.Height / texture.Width);
        }

        public void SetScene(Scene scene)
        {
            this.scene = scene;
        }

        public void Draw(GameTime gameTime)
        {
            Vector2 movement = lastCameraPosition - Camera.GetTarget();
            position = position + speedRatio * movement;
            lastCameraPosition = Camera.GetTarget();

            int xMin, xMax, yMin, yMax;
            Rectangle dest = Camera.WorldSize2PixelRectangle(position, size);

            xMin = -(int)Math.Ceiling((dest.X - 0.5f * dest.Width) / dest.Width);
            yMin = -(int)Math.Ceiling((dest.Y - 0.5f * dest.Height) / dest.Height);

            xMax = (int)Math.Ceiling((Camera.gDevManager.PreferredBackBufferWidth
                - dest.X - dest.Width * .5f) / dest.Width);

            yMax = (int)Math.Ceiling((Camera.gDevManager.PreferredBackBufferHeight
                - dest.Y - dest.Height * .5f) / dest.Height);


            for (int i = xMin; i <= xMax; i++)
                for (int j = yMin; j <= yMax; j++)
                {
                    Rectangle d;
                    d = new Rectangle(dest.X + i * dest.Width,
                                      dest.Y + j * dest.Height,
                                      dest.Width, dest.Height);
                    scene.SpriteBatch.Draw(texture, d, null,
                        Color.White, 0f, origin, SpriteEffects.None, 0);
                }
        }

        public void Dispose()
        {
            texture.Dispose();
        }
    }
}
