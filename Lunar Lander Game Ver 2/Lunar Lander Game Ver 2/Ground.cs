using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lunar_Lander_Game_Ver_2
{
    class Ground : GameObject
    {
        public Ground(Texture2D texture, Vector2 pos, Color color, Vector2 size) : base(texture, pos, color)
        {
            hitBox = new Rectangle(pos.ToPoint(), size.ToPoint());
            origin = Vector2.Zero;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, pos, null, color, 0f, origin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(texture, hitBox, null, color, 0f, origin, SpriteEffects.None, 0);
        }
    }
}
