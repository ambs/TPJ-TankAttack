#region Using statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace TPJ_TankAttack
{
    class Sprite
    {
        private Texture2D image;
        protected Vector2 position;
        protected Vector2 size;
        private float rotation;
        protected Scene scene;
        protected Vector2 pixelsize;
        protected Rectangle? source = null;
        public Sprite(ContentManager contents, String assetName)
        {
            this.rotation = 0f;
            this.position = Vector2.Zero;
            this.image = contents.Load<Texture2D>(assetName);
            this.pixelsize = new Vector2(image.Width, image.Height);
            this.size = new Vector2(1f, (float)image.Height / (float)image.Width);
        }

        public virtual void Scale(float scale)
        {
            this.size *= scale;
        }

        public virtual void SetScene(Scene s)
        {
            this.scene = s;
        }
        public Sprite Scl(float scale)
        {
            this.Scale(scale);
            return this;
        }


        public virtual void Draw(GameTime gameTime)
        {
            Rectangle pos = Camera.WorldSize2PixelRectangle(this.position, this.size);
           // scene.SpriteBatch.Draw(this.image, pos, Color.White);
            scene.SpriteBatch.Draw(this.image, pos, source, Color.White,
                this.rotation, new Vector2(pixelsize.X/ 2, pixelsize.Y / 2),
                SpriteEffects.None, 0);
        }

        public virtual void SetRotation(float r)
        {
            this.rotation = r;
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Dispose()
        {
            this.image.Dispose();
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }
        public Sprite At(Vector2 p)
        {
            this.SetPosition(p);
            return this;
        }
    }
}
