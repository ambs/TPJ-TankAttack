#region Using Statements
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace TPJ_TankAttack
{
    static class Camera
    {
        public static GraphicsDeviceManager gDevManager;
        public static float worldWidth { private set; get; }
        public static float ratio      { private set; get; }
        private static Vector2 target;
        private static int lastSeenPixelWidth = 0;

        public static void SetGraphicsDeviceManager(GraphicsDeviceManager gdm)
        {
            Camera.gDevManager = gdm;
        }

        public static void SetWorldWidth(float w)
        {
            Camera.worldWidth = w;
        }

        public static void SetTarget(Vector2 target)
        {
            Camera.target = target;
        }

        private static void UpdateRatio()
        {
            if (Camera.lastSeenPixelWidth !=
                Camera.gDevManager.PreferredBackBufferWidth)
            {
                Camera.ratio = Camera.gDevManager.PreferredBackBufferWidth /
                    Camera.worldWidth;
                Camera.lastSeenPixelWidth = Camera.gDevManager.PreferredBackBufferWidth;
            }
        }

        public static Vector2 WorldPoint2Pixels(Vector2 point)
        {
            Camera.UpdateRatio();
            Vector2 pixelPoint = new Vector2();

            // Calcular pixeis em relacao ao target da camara (centro)
            pixelPoint.X = (int)((point.X - target.X) * Camera.ratio + 0.5f);
            pixelPoint.Y = (int)((point.Y - target.Y) * Camera.ratio + 0.5f);

            // protetar pixeis calculados para o canto inferior esquerdo do ecra
            pixelPoint.X += Camera.lastSeenPixelWidth / 2;
            pixelPoint.Y += Camera.gDevManager.PreferredBackBufferHeight / 2;

            // inverter coordenadas Y
            pixelPoint.Y = Camera.gDevManager.PreferredBackBufferHeight - pixelPoint.Y;

            return pixelPoint;
        }

        public static Rectangle WorldSize2PixelRectangle(Vector2 pos, Vector2 size)
        {
            Camera.UpdateRatio();

            Vector2 pixelPos = WorldPoint2Pixels(pos);
            int pixelWidth = (int)( size.X * Camera.ratio + .5f);
            int pixelHeight = (int)(size.Y * Camera.ratio + .5f);

            return new Rectangle((int)pixelPos.X, (int)pixelPos.Y, pixelWidth, pixelHeight);
        }

        public static Vector2 GetTarget()
        {
            return Camera.target;
        }
    }
}
