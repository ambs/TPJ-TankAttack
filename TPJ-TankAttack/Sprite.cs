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
        public bool HasCollisions { protected set; get; }

        protected Texture2D image;
        protected Vector2 position;
        protected float radius; // raio da "bounding box"
        protected Vector2 size;
        protected float rotation;
        protected Scene scene;
        protected Vector2 pixelsize;
        protected Rectangle? source = null;
        protected Color[] pixels;
        protected ContentManager cManager;
        public Sprite(ContentManager contents, String assetName)
        {
            this.cManager = contents;
            this.HasCollisions = false;
            this.rotation = 0f;
            this.position = Vector2.Zero;
            this.image = contents.Load<Texture2D>(assetName);
            this.pixelsize = new Vector2(image.Width, image.Height);
            this.size = new Vector2(1f, (float)image.Height / (float)image.Width);
        }

        // Se houver colisao, collisionPoint é o ponto de colisão
        // se não houver, collisionPoint deve ser ignorado!
        public bool CollidesWith(Sprite other, out Vector2 collisionPoint)
        {
            collisionPoint = position; // Calar o compilador

            if (!this.HasCollisions)  return false;
            if (!other.HasCollisions) return false;

            float distance = (this.position - other.position).Length();

            if (distance > this.radius + other.radius) return false;
            
            return this.PixelTouches(other, out collisionPoint);
        }

        public virtual void EnableCollisions()
        {
            this.HasCollisions = true;
            this.radius = (float) Math.Sqrt( Math.Pow(size.X / 2, 2) +
                                             Math.Pow(size.Y / 2, 2) );

            pixels = new Color[(int)(pixelsize.X * pixelsize.Y)];
            image.GetData<Color>(pixels);
        }

        public Color GetColorAt(int x, int y)
        {
            // Se nao houver collider, da erro!!!
            return pixels[x + y * (int)pixelsize.X];
        }

        private Vector2 ImagePixelToVirtualWorld(int i, int j)
        {
            float x = i * size.X / (float)pixelsize.X;
            float y = j * size.Y / (float)pixelsize.Y;
            return new Vector2(position.X + x - (size.X * 0.5f),
                               position.Y - y + (size.Y * 0.5f));
        }

        private Vector2 VirtualWorldPointToImagePixel(Vector2 p)
        {
            Vector2 delta = p - position;
            float i = delta.X * pixelsize.X / size.X;
            float j = delta.Y * pixelsize.Y / size.Y;

            i += pixelsize.X * 0.5f;
            j = pixelsize.Y * 0.5f - j;

            return new Vector2(i, j);
        }

        public bool PixelTouches(Sprite other, out Vector2 collisionPoint)
        {
            // Se nao houver colisao, o ponto de colisao retornado e'
            // a posicao da Sprite (podia ser outro valor qualquer)
            collisionPoint = position;

            bool touches = false;

            int i = 0;
            while (touches == false && i < pixelsize.X)
            {
                int j = 0;
                while (touches == false && j < pixelsize.Y)
                {
                    if (GetColorAt(i, j).A > 0)
                    {
                        Vector2 CollidePoint = ImagePixelToVirtualWorld(i, j);
                        Vector2 otherPixel = other.VirtualWorldPointToImagePixel(CollidePoint);

                        if (otherPixel.X >= 0 && otherPixel.Y >= 0 &&
                            otherPixel.X < other.pixelsize.X &&
                            otherPixel.Y < other.pixelsize.Y)
                        {
                            if (other.GetColorAt((int)otherPixel.X, (int)otherPixel.Y).A > 0)
                            {
                                touches = true;
                                collisionPoint = CollidePoint;
                            }
                        }

                    }
                    j++;
                }
                i++;
            }
            return touches;
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

        public void Destroy()
        {
            this.scene.RemoveSprite(this);
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
