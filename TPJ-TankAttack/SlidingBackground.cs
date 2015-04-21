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
        private float speedRatio = 1f; // sliding speed
        private Scene scene;

        public SlidingBackground(ContentManager manager, string assetName)
        {
            texture = manager.Load<Texture2D>(assetName);
            origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            lastCameraPosition = Camera.GetTarget();
            position = Camera.GetTarget();
            size = new Vector2(Camera.worldWidth,
                Camera.worldWidth * texture.Height / texture.Width);
        }

        public void SetScene(Scene scene)
        {
            this.scene = scene;
        }

        public void Draw(GameTime gameTime)
        {

        }

        public void Dispose()
        {
            texture.Dispose();
        }
    }
}
