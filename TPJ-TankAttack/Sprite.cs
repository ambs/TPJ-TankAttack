using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPJ_TankAttack
{
    class Sprite
    {
        private Texture2D image;
        private Vector2 position;
        private Vector2 size;
        private float rotation;

        public Sprite(ContentManager contents, String assetName)
        {
            this.rotation = 0f;
            this.size = new Vector2(1f, 1f);
            this.position = Vector2.Zero;
            this.image = contents.Load<Texture2D>(assetName);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle pos = Camera.WorldSize2PixelRectangle(this.position, this.size);
            spriteBatch.Draw(this.image, pos, Color.White);
        }
    }
}
