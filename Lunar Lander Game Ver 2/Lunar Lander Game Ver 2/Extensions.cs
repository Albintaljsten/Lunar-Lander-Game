using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lunar_Lander_Game_Ver_2
{
    public static class Extensions
    {
        private static Texture2D tex;
        public static void Draw(this SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            if (tex == null)
            {
                tex = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                tex.SetData(new Color[1] { Color.White });
            }
            spriteBatch.Draw(tex, rect, color);
        }
    }
}
